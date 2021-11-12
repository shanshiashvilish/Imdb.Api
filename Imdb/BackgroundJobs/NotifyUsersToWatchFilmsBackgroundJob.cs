using Imdb.Core.EmailService;
using Imdb.Core.Imdb;
using Imdb.Core.Users;
using Imdb.Core.WatchListByFilms;
using Imdb.Core.WatchLists;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Imdb.Api.BackgroundJobs
{
    public class NotifyUsersToWatchFilmsBackgroundJob : BackgroundService
    {
        private readonly IUserRepository _userRepository;
        private readonly IWatchListByFilmsService _watchListByFilmsService;
        private readonly IWatchListService _watchListService;
        private readonly IImdbService _imdbService;
        private readonly IEmailService _emailService;
        private readonly ILogger<NotifyUsersToWatchFilmsBackgroundJob> _logger;
        private DateTime _witnessOfLastExecution;

        public NotifyUsersToWatchFilmsBackgroundJob(ILogger<NotifyUsersToWatchFilmsBackgroundJob> logger,
                                                    DateTime witnessOfLastExecution,
                                                    IUserRepository userRepository,
                                                    IWatchListByFilmsService watchListByFilmsService,
                                                    IWatchListService watchListService,
                                                    IImdbService imdbService,
                                                    IEmailService emailService)
        {
            _logger = logger;
            _witnessOfLastExecution = witnessOfLastExecution;
            _userRepository = userRepository;
            _watchListByFilmsService = watchListByFilmsService;
            _watchListService = watchListService;
            _imdbService = imdbService;
            _emailService = emailService;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    var dateNow = DateTime.Now;

                    bool isTimeToStartJob = DateTime.UtcNow.Subtract(_witnessOfLastExecution).Days >= 14
                                            && dateNow.Hour >= 19
                                            && dateNow.Minute >= 30
                                            || _witnessOfLastExecution == DateTime.MinValue;

                    if (isTimeToStartJob)
                    {
                        var usersWatchListsIds = _userRepository.Queryable.Select(u => u.WatchListId).ToList();

                        foreach (var watchListId in usersWatchListsIds)
                        {
                            var filmsToWatch = _watchListByFilmsService.GetFilmsToWatch(watchListId.Value);

                            if (filmsToWatch.Count > 3)
                            {
                                var ImdbIds = new List<string>();
                                filmsToWatch.ForEach(u => ImdbIds.Add(u.Film.ImdbId));

                                var mostPopularOne = await _imdbService.GetMostRatedFilm(ImdbIds);
                                var wikipediaDescription = await _imdbService.GetWikipediaDescription(mostPopularOne.id);

                                string message = "You have not watched the most popular film in your watchlist.\n" +
                                                 $"Title: {mostPopularOne.title}\n" +
                                                 $"Imdb Rating: {mostPopularOne.imDbRating}\n" +
                                                 $"Poster: {mostPopularOne.posters}\n" +
                                                 $"Description: {wikipediaDescription}";

                                var userMailAddress = _watchListService.GetUserByWatchListId(watchListId.Value).Email;
                                var mailSubject = "You have not watched one of the top rated film yet!";
                                _emailService.Send(userMailAddress, mailSubject, message);
                            }
                        }
                        _witnessOfLastExecution = DateTime.Now;
                    }
                    await Task.Delay(TimeSpan.FromMinutes(5));
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, ex.Message);
                    await Task.Delay(TimeSpan.FromMinutes(5));
                }
            }
        }
    }
}
