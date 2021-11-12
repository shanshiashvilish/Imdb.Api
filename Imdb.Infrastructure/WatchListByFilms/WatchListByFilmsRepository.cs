using Imdb.Core.WatchListByFilms;
using Imdb.Infrastructure.DataAccess;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Imdb.Infrastructure.WatchListByFilms
{
    public class WatchListByFilmsRepository : IWatchListByFilmsRepository
    {
        private readonly ImdbDbContext _imdbDbContext;

        public WatchListByFilmsRepository(ImdbDbContext imdbDbContext)
        {
            _imdbDbContext = imdbDbContext;
        }

        public List<Core.WatchListByFilms.WatchListByFilms> GetUsersFilmsToWatch(int watchListId)
        {
            return _imdbDbContext.WatchListByFilms.Where(u => u.WatchListId == watchListId && u.IsWatched == false).ToList();
        }

        public Core.WatchListByFilms.WatchListByFilms GetUsersWatchListByFilm(int filmId, int watchListId)
        {
            return _imdbDbContext.WatchListByFilms.Where(wbf => wbf.WatchListId == watchListId && wbf.FilmId == filmId).FirstOrDefault();
        }

        public async Task SaveChangesAsync()
        {
            await _imdbDbContext.SaveChangesAsync();
        }
    }
}
