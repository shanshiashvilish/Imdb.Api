using Imdb.Core.Users;
using Imdb.Infrastructure.DataAccess;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Imdb.Infrastructure.Users
{
    public class UserRepository : IUserRepository
    {
        private readonly ImdbDbContext _imdbDbContext;
        
        public UserRepository(ImdbDbContext imdbDbContext)
        {
            _imdbDbContext = imdbDbContext;
        }

        public IQueryable<User> Queryable { get { return GetAllWatchLists(); } }

        private IQueryable<User> GetAllWatchLists()
        {
            return _imdbDbContext.Users.Include(x => x.WatchList);
        }

        public async Task AddUserAsync(User user)
        {
            await _imdbDbContext.AddAsync(user);
        }

        public async Task SaveChangesAsync()
        {
            await _imdbDbContext.SaveChangesAsync();
        }

        public async Task<User> GetUserAsync(int userId)
        {
            return await _imdbDbContext.Users.Include(u => u.WatchList).Include(u => u.WatchList.Films).Where(u => u.Id == userId).FirstOrDefaultAsync();
        }
    }
}
