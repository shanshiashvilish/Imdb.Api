using System.Collections.Generic;
using System.Threading.Tasks;

namespace Imdb.Core.WatchListByFilms
{
    public interface IWatchListByFilmsRepository
    {
        WatchListByFilms GetUsersWatchListByFilm(int filmId, int watchListId);
        List<WatchListByFilms> GetUsersFilmsToWatch(int watchListId);
        Task SaveChangesAsync();
    }
}
