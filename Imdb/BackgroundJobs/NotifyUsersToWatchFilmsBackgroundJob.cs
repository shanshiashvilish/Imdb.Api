using Imdb.Api.BackgroundJobs.Helpers;
using Imdb.Application.Options;
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
        private readonly NotifyUserToWatchFilmsConfiguration _configuration;

        public NotifyUsersToWatchFilmsBackgroundJob(IServiceScopeFactory scopeFactory, ILogger<NotifyUsersToWatchFilmsBackgroundJob> logger, NotifyUsersToWatchFilmsWitness witness, NotifyUserToWatchFilmsConfiguration configuration)
        {
            _scopeFactory = scopeFactory;
            _logger = logger;
            _witness = witness;
            _configuration = configuration;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    bool isTimeToStartJob = NotifyUsersToWatchFilmsScheduler.IsTimeToStatJob(_witness.DateOfLastExecution, _configuration);

                    if (isTimeToStartJob)
                    {
                        using var scope = _scopeFactory.CreateScope();
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
