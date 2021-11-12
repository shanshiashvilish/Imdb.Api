using Imdb.Core.EmailService;
using Imdb.Core.Imdb;
using Imdb.Core.Users;
using Imdb.Core.WatchListByFilms;
using Imdb.Core.WatchLists;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Imdb.Application.WatchLists
{
    public class WatchListService : IWatchListService
    {
        private readonly IWatchListRepository _watchListRepository;
        private readonly IUserRepository _userRepository;
        private readonly IWatchListByFilmsService _watchListByFilmsService;
        private readonly IImdbService _imdbService;
        private readonly IEmailService _emailService;

        public WatchListService(IWatchListRepository watchListRepository, 
                                IUserRepository userRepository, 
                                IWatchListByFilmsService watchListByFilmsService, 
                                IImdbService imdbService, 
                                IEmailService emailService)
        {
            _watchListRepository = watchListRepository;
            _userRepository = userRepository;
            _watchListByFilmsService = watchListByFilmsService;
            _imdbService = imdbService;
            _emailService = emailService;
        }

        public async Task AddWatchListAsync(int userId)
        {
            var watchList = new WatchList { UserId = userId };

            await _watchListRepository.AddWatchListAsync(watchList);
            await _watchListRepository.SaveChangesAsync();
        }

        public User GetUserByWatchListId(int watchListId)
        {
            return _watchListRepository.GetUserByWatchListId(watchListId);
        }

        public async Task NotifyUsersToWatchFilms()
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

                    var userMailAddress = GetUserByWatchListId(watchListId.Value).Email;
                    var mailSubject = "You have not watched one of the top rated film yet!";
                    _emailService.Send(userMailAddress, mailSubject, message);
                }
            }
        }

        public async Task SaveChangesAsync()
        {
            await _watchListRepository.SaveChangesAsync();
        }
    }
}
