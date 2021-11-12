using Imdb.Core.Users;
using Imdb.Core.WatchLists;
using Imdb.Core.Films;
using Imdb.Infrastructure.DataAccess.EntityConfiguration;
using Microsoft.EntityFrameworkCore;

namespace Imdb.Infrastructure.DataAccess
{
    public class ImdbDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<WatchList> WatchLists { get; set; }
        public DbSet<Film> Films { get; set; }
        public DbSet<Core.WatchListByFilms.WatchListByFilms> WatchListByFilms { get; set; }

        public ImdbDbContext(DbContextOptions<ImdbDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new WatchListConfiguration());
            modelBuilder.ApplyConfiguration(new FilmConfiguration());
            modelBuilder.ApplyConfiguration(new WatchListByFilmsConfiguration());
        }
    }
}
