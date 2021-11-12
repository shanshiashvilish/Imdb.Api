using Imdb.Application.Films;
using Imdb.Core.Films;
using Imdb.Infrastructure.Films;
using Microsoft.Extensions.DependencyInjection;

namespace Imdb.Infrastructure.Configuration.FilmsConfiguration
{
    public static class ConfigureFilmServices 
    {
        public static IServiceCollection AddFilmServices(this IServiceCollection services)
        {
            services.AddScoped<IFilmService, FilmService>();
            services.AddScoped<IFilmRepository, FilmRepository>();

            return services;
        }

    }
}
