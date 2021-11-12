using Imdb.Api.BackgroundJobs;
using Imdb.Api.BackgroundJobs.Helpers;
using Imdb.Api.Initializers;
using Imdb.Application.Options;
using Imdb.Infrastructure.Configuration.EmailServiceConfiguration;
using Imdb.Infrastructure.Configuration.FilmsConfiguration;
using Imdb.Infrastructure.Configuration.Imdb;
using Imdb.Infrastructure.Configuration.UsersConfiguration;
using Imdb.Infrastructure.Configuration.WatchListByFilmsConfiguration;
using Imdb.Infrastructure.Configuration.WatchListsConfiguration;
using Imdb.Infrastructure.DataAccess;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;

namespace Imdb.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ImdbDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("Default")));
            services.AddScoped<IInitializer, AutoMigrateInitializer>();

            services.Configure<ImdbApiConfiguration>(Configuration.GetSection(nameof(ImdbApiConfiguration)));

            services.AddHostedService<NotifyUsersToWatchFilmsBackgroundJob>();
            services.AddSingleton<NotifyUsersToWatchFilmsWitness>();

            services.AddUserServices();
            services.AddImdbServices();
            services.AddFilmServices();
            services.AddWatchListServices();
            services.AddWatchListByFilmServices();

            services.AddEmailServices();
            services.AddHttpClient();

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Imdb.Api", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Imdb.Api v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
        #region CustomExtensions
        private static void InitializeData(IServiceProvider serviceProvider)
        {
            IEnumerable<IInitializer> initializers = serviceProvider.GetServices<IInitializer>();
            foreach (var initializer in initializers)
                initializer.Initialize().Wait();
        }
        #endregion
    }
}
