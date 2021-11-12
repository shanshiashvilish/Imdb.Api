using Imdb.Core.WatchLists;
using System.Threading.Tasks;

namespace Imdb.Core.Users
{
    public interface IUserService
    {
        Task AddUserAsync(string userName, string email);
        Task<WatchList> GetUsersWatchListAsync(int userId);
    }
}
