using Imdb.Core.Users;
using System.Linq;
using System.Threading.Tasks;

namespace Imdb.Core.WatchLists
{
    public interface IWatchListRepository
    {
        Task<WatchList> GetUsersWatchList(int userId);
        User GetUserByWatchListId(int watchListId);
        Task SaveChangesAsync();
        IQueryable<WatchList> Queryable { get; }
        Task AddWatchListAsync(WatchList watchList);
    }
}
