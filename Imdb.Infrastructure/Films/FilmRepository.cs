using Imdb.Core.Films;
using Imdb.Infrastructure.DataAccess;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Imdb.Infrastructure.Films
{
    public class FilmRepository : IFilmRepository
    {
        private readonly ImdbDbContext _imdbDbContext;

        public FilmRepository(ImdbDbContext imdbDbContext)
        {
            _imdbDbContext = imdbDbContext;
        }

        public async Task SaveChangesAsync()
        {
            await _imdbDbContext.SaveChangesAsync();
        }

        public async Task AddFilmAsync(Film film)
        {
            await _imdbDbContext.AddAsync(film);
        }
    }
}
