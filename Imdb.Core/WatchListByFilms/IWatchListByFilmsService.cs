using System.Collections.Generic;
using System.Threading.Tasks;

namespace Imdb.Core.WatchListByFilms
{
    public interface IWatchListByFilmsService
    {
        Task MarkAsWatched(int userId, int filmId);
        List<WatchListByFilms> GetFilmsToWatch(int watchListId);
    }
}
