using Imdb.Core.Users;
using Imdb.Core.WatchLists;
using System.Threading.Tasks;

namespace Imdb.Application.WatchLists
{
    public class WatchListService : IWatchListService
    {
        private readonly IWatchListRepository _watchListRepository;

        public WatchListService(IWatchListRepository watchListRepository)
        {
            _watchListRepository = watchListRepository;
        }

        public async Task AddWatchListAsync(int userId)
        {
            var watchList = new WatchList { UserId = userId };

            await _watchListRepository.AddWatchListAsync(watchList);
            await _watchListRepository.SaveChangesAsync();
        }

        public User GetUserByWatchListId(int watchListId)
        {
            return _watchListRepository.GetUserByWatchListId(watchListId);
        }

        public async Task SaveChangesAsync()
        {
            await _watchListRepository.SaveChangesAsync();
        }
    }
}
