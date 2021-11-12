using Imdb.Core.Films;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Imdb.Infrastructure.DataAccess.EntityConfiguration
{
    public class FilmConfiguration : IEntityTypeConfiguration<Film>
    {
        public void Configure(EntityTypeBuilder<Film> builder)
        {
            builder.ToTable("Films");

            builder.HasKey(f => f.Id);
            builder.Property(f => f.FilmName).IsRequired();
            builder.Property(f => f.DateOfCreate).HasDefaultValueSql("GETUTCDATE()").ValueGeneratedOnAdd();
        }
    }
}
