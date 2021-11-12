using Imdb.Core.EmailService;
using Imdb.Infrastructure.Services.Email;
using Microsoft.Extensions.DependencyInjection;

namespace Imdb.Infrastructure.Configuration.EmailServiceConfiguration
{
    public static class ConfigureEmailServices
    {
        public static IServiceCollection AddEmailServices(this IServiceCollection services)
        {
            services.AddScoped<IEmailService, EmailService>();
            
            return services;
        }
    }
}
