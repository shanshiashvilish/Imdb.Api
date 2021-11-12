using Imdb.Core.Users;
using Imdb.Core.WatchListByFilms;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Imdb.Application.WatchListByFilms
{
    public class WatchListByFilmsService : IWatchListByFilmsService
    {
        private readonly IWatchListByFilmsRepository _watchListByFilmsRepository;
        private readonly IUserRepository _userRepository;

        public WatchListByFilmsService(IWatchListByFilmsRepository watchListByFilmsRepository, IUserRepository userRepository)
        {
            _watchListByFilmsRepository = watchListByFilmsRepository;
            _userRepository = userRepository;
        }

        public List<Core.WatchListByFilms.WatchListByFilms> GetFilmsToWatch(int watchListId)
        {
            return _watchListByFilmsRepository.GetUsersFilmsToWatch(watchListId).ToList();
        }

        public async Task MarkAsWatched(int userId, int filmId)
        {
            var user = await _userRepository.GetUserAsync(userId);
            var watchListId = user.WatchListId;
            var usersFilmToMarkAsWatched = _watchListByFilmsRepository.GetUsersWatchListByFilm(filmId, watchListId.Value);

            usersFilmToMarkAsWatched.IsWatched = true;

            await _watchListByFilmsRepository.SaveChangesAsync();
        }
    }
}
