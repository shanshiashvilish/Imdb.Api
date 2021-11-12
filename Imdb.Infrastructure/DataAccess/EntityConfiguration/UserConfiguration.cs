using Imdb.Core.Users;
using Imdb.Core.WatchLists;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Imdb.Infrastructure.DataAccess.EntityConfiguration
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users");

            builder.HasKey(user => user.Id);
            builder.Property(user => user.Email).IsRequired();
            builder.Property(user => user.UserName).IsRequired();

            builder
                .HasOne(p => p.WatchList)
                   .WithOne(p => p.User)
                   .HasForeignKey<WatchList>(p => p.UserId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
