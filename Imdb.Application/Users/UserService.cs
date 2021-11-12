using Imdb.Core.Users;
using Imdb.Core.WatchLists;
using System.Threading.Tasks;

namespace Imdb.Application.Users
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task AddUserAsync(string userName, string email)
        {
            WatchList watchList = new WatchList()
            {
                Films = new System.Collections.Generic.List<Core.Films.Film>(),
            };

            User userToAdd = new()
            {
                UserName = userName,
                Email = email,
                WatchList = watchList
            };

            await _userRepository.AddUserAsync(userToAdd);
            await _userRepository.SaveChangesAsync();
            watchList.UserId = userToAdd.Id;
            await _userRepository.SaveChangesAsync();
        }

        public async Task<WatchList> GetUsersWatchListAsync(int userId)
        {
            var user = await _userRepository.GetUserAsync(userId);
            var watchList = user.WatchList;

            return watchList;
        }
    }
}
