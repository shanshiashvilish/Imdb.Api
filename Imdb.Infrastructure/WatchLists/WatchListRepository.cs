using Imdb.Core.Users;
using Imdb.Core.WatchLists;
using Imdb.Infrastructure.DataAccess;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Imdb.Infrastructure.WatchLists
{
    public class WatchListRepository : IWatchListRepository
    {
        private readonly ImdbDbContext _imdbDbContext;
        public IQueryable<WatchList> Queryable { get { return GetAllWatchLists(); } }

        public WatchListRepository(ImdbDbContext imdbDbContext)
        {
            _imdbDbContext = imdbDbContext;
        }

        private IQueryable<WatchList> GetAllWatchLists()
        {
            return _imdbDbContext.WatchLists.Include(w => w.Films).Include(x => x.User);
        }

        public async Task<WatchList> GetUsersWatchList(int userId)
        {
            return await _imdbDbContext.WatchLists.Where(watchlist => watchlist.UserId == userId).FirstOrDefaultAsync();
        }
        
        public async Task SaveChangesAsync()
        {
            await _imdbDbContext.SaveChangesAsync();
        }

        public User GetUserByWatchListId(int watchListId)
        {
            return _imdbDbContext.WatchLists.Where(wl => wl.Id == watchListId).Select(watchList => watchList.User).FirstOrDefault();
        }

        public async Task AddWatchListAsync(WatchList watchList)
        {
            await _imdbDbContext.AddAsync(watchList);
        }
    }
}
