using Imdb.Application.Imdb;
using Imdb.Core.Imdb;
using Microsoft.Extensions.DependencyInjection;

namespace Imdb.Infrastructure.Configuration.Imdb
{
    public static class ConfigureImdbServices
    {
        public static IServiceCollection AddImdbServices(this IServiceCollection services)
        {
            services.AddScoped<IImdbService, ImdbService>();

            return services;
        }
    }
}
