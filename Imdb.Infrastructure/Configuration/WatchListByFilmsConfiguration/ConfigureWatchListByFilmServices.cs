using Imdb.Core.WatchListByFilms;
using Microsoft.Extensions.DependencyInjection;
using Imdb.Application.WatchListByFilms;
using Imdb.Infrastructure.WatchListByFilms;

namespace Imdb.Infrastructure.Configuration.WatchListByFilmsConfiguration
{
    public static class ConfigureWatchListByFilmServices
    {
        public static IServiceCollection AddWatchListByFilmServices(this IServiceCollection services)
        {
            services.AddScoped<IWatchListByFilmsService, WatchListByFilmsService>();
            services.AddScoped<IWatchListByFilmsRepository, WatchListByFilmsRepository>();

            return services;
        }
    }
}
