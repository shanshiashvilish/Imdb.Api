using System.Threading.Tasks;

namespace Imdb.Core.Films
{
    public interface IFilmRepository
    {
        Task SaveChangesAsync();
        Task AddFilmAsync(Film film);
    }
}
