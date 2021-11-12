using Imdb.Infrastructure.DataAccess;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Imdb.Api.Initializers
{
    public class AutoMigrateInitializer : IInitializer
    {
        private readonly ImdbDbContext _imdbDbContext;

        public AutoMigrateInitializer(ImdbDbContext imdbDbContext)
        {
            _imdbDbContext = imdbDbContext;
        }

        public async Task Initialize()
        {
            using (var context = _imdbDbContext)
            {
                await context.Database.MigrateAsync();
            }
        }
    }
}
