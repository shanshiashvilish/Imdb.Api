using Imdb.Api.BackgroundJobs.Helpers;
using Imdb.Core.WatchLists;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Imdb.Api.BackgroundJobs
{
    public class NotifyUsersToWatchFilmsBackgroundJob : BackgroundService
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly ILogger<NotifyUsersToWatchFilmsBackgroundJob> _logger;
        private NotifyUsersToWatchFilmsWitness _witness;

        public NotifyUsersToWatchFilmsBackgroundJob(IServiceScopeFactory scopeFactory,
                                                    ILogger<NotifyUsersToWatchFilmsBackgroundJob> logger,
                                                    NotifyUsersToWatchFilmsWitness witness)
        {
            _scopeFactory = scopeFactory;
            _logger = logger;
            _witness = witness;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                using var scope = _scopeFactory.CreateScope();

                try
                {
                    bool isTimeToStartJob = NotifyUsersToWatchFilmsScheduler.IsTimeToStatJob(_witness.DateOfLastExecution);

                    if (isTimeToStartJob)
                    {
                        var watchListService = scope.ServiceProvider.GetRequiredService<IWatchListService>();
                        await watchListService.NotifyUsersToWatchFilms();

                        _witness.DateOfLastExecution = DateTime.Now;
                    }
                    await Task.Delay(TimeSpan.FromMinutes(1));
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, ex.Message);
                    await Task.Delay(TimeSpan.FromMinutes(1));
                }
            }
        }
    }
}
