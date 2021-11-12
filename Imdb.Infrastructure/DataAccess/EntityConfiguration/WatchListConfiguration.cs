using Imdb.Core.Users;
using Imdb.Core.WatchLists;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Imdb.Infrastructure.DataAccess.EntityConfiguration
{
    public class WatchListConfiguration : IEntityTypeConfiguration<WatchList>
    {
        public void Configure(EntityTypeBuilder<WatchList> builder)
        {
            builder.ToTable("WatchLists");

            builder.HasKey(watchList => watchList.Id);
            builder.Property(watchList => watchList.UserId).IsRequired();
            builder.Property(watchList => watchList.CreateDate).HasDefaultValueSql("GETUTCDATE()").ValueGeneratedOnAdd();

            builder
                .HasOne(p => p.User)
                   .WithOne(p => p.WatchList)
                   .HasForeignKey<User>(p => p.WatchListId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder
                .HasMany(w => w.Films)
                .WithMany(w => w.WatchList)
                .UsingEntity<Core.WatchListByFilms.WatchListByFilms>(
                    j => j
                        .HasOne(p => p.Film)
                        .WithMany()
                        .HasForeignKey(p => p.FilmId)
                        .OnDelete(DeleteBehavior.Cascade),
                    j => j
                        .HasOne(p => p.WatchList)
                        .WithMany()
                        .HasForeignKey(p => p.WatchListId)
                        .OnDelete(DeleteBehavior.Cascade),
                    j => 
                    {
                        j.HasKey(t => new { t.FilmId, t.WatchListId });
                    });
        }
    }
}
