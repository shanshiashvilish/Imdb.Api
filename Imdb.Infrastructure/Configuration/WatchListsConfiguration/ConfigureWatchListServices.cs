using Imdb.Application.WatchLists;
using Imdb.Core.WatchLists;
using Imdb.Infrastructure.WatchLists;
using Microsoft.Extensions.DependencyInjection;

namespace Imdb.Infrastructure.Configuration.WatchListsConfiguration
{
    public static class ConfigureWatchListServices
    {
        public static IServiceCollection AddWatchListServices(this IServiceCollection services)
        {
            services.AddScoped<IWatchListService, WatchListService>();
            services.AddScoped<IWatchListRepository, WatchListRepository>();

            return services;
        }
    }
}
