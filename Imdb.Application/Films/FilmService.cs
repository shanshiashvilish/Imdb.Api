using Imdb.Core.Films;
using Imdb.Core.Users;
using Imdb.Core.WatchLists;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Imdb.Application.Films
{
    public class FilmService : IFilmService
    {
        private readonly IFilmRepository _filmRepository;
        private readonly IUserRepository _userRepository;

        public FilmService(IFilmRepository filmRepository, IUserRepository userRepository)
        {
            _filmRepository = filmRepository;
            _userRepository = userRepository;
        }

        public async Task AddFilmAsync(int userId, string filmName)
        {
            var watchList = _userRepository.Queryable
                                           .Where(p => p.Id == userId)
                                           .FirstOrDefault()
                                           .WatchList;

            if (watchList == null)
            {
                //createnewwatchlist
            }


            var listOfWatchLists = new List<WatchList>
            {
                watchList
            };

            await _filmRepository.AddFilmAsync(new Film
            {
                FilmName = filmName,
                WatchList = listOfWatchLists,
            });

            await _filmRepository.SaveChangesAsync();
        }

        public async Task MarkAsWatched(int userId, int filmId)
        {
            //var film = await _filmRepository.GetFilm(userId, filmId);

            //film.MarkAsWatched();

            await _filmRepository.SaveChangesAsync();
        }

    }
}
