using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Imdb.Infrastructure.DataAccess.EntityConfiguration
{
    public class WatchListByFilmsConfiguration : IEntityTypeConfiguration<Core.WatchListByFilms.WatchListByFilms>
    {
        public void Configure(EntityTypeBuilder<Core.WatchListByFilms.WatchListByFilms> builder)
        {
            builder.ToTable("WatchListByFilms");

            builder.HasKey(wf => wf.Id);
            builder.Property(wf => wf.FilmId).IsRequired();
            builder.Property(wf => wf.IsWatched).HasDefaultValue(false).ValueGeneratedOnAdd();
            builder.Property(wf => wf.CreateDate).HasDefaultValueSql("GETUTCDATE()").ValueGeneratedOnAdd();
        }
    }
}
