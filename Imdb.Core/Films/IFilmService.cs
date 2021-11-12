using System.Threading.Tasks;

namespace Imdb.Core.Films
{
    public interface IFilmService
    {
        Task AddFilmAsync(int userId, string filmName);
        Task MarkAsWatched(int userId, int filmId);
    }
}
