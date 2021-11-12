using Imdb.Application.Users;
using Imdb.Core.Users;
using Imdb.Infrastructure.Users;
using Microsoft.Extensions.DependencyInjection;

namespace Imdb.Infrastructure.Configuration.UsersConfiguration
{
    public static class ConfigureUserServices
    {
        public static IServiceCollection AddUserServices(this IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IUserRepository, UserRepository>();

            return services;
        }
    }
}
