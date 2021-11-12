using System.Linq;
using System.Threading.Tasks;

namespace Imdb.Core.Users
{
    public interface IUserRepository
    {
        Task SaveChangesAsync();
        Task AddUserAsync(User user);
        Task<User> GetUserAsync(int userId);
        IQueryable<User> Queryable { get; }
    }
}
