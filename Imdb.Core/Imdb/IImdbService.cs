using Imdb.Core.Imdb.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Imdb.Core.Imdb
{
    public interface IImdbService
    {
        Task<FilmSearchResultModel> SearchFilmAsync(string filmName);
        Task<GetFilmPostersResultModel> GetFilmPostersAsync(string externalId);
        Task<GetMostRatedFilmResultModel> GetMostRatedFilm(List<string> imdbIds);
        Task<string> GetWikipediaDescription(string imdbId);
        Task<List<string>> GetGenresByNameArray(string[] filmList);
    }
}
