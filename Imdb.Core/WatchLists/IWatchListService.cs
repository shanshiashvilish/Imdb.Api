using Imdb.Core.Users;
using System.Threading.Tasks;

namespace Imdb.Core.WatchLists
{
    public interface IWatchListService
    {
        Task SaveChangesAsync();
        User GetUserByWatchListId(int watchListId);
        Task AddWatchListAsync(int userId);
    }
}
