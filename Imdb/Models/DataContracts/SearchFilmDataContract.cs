using Imdb.Core.Imdb.Models;
using System.Collections.Generic;

namespace Imdb.Api.Models.DataContracts
{
    public class SearchFilmResult
    {
        public List<FilmsResponse> Films { get; set; }

        public static SearchFilmResult BuildFrom(FilmSearchResultModel searchResult)
        {
            var FilmList = new List<FilmsResponse>();
            var foundFilms = searchResult.results;

            foreach (var film in foundFilms)
            {
                FilmList.Add(new FilmsResponse
                {
                    Title = film.title,
                    Description = film.description,
                    PosterUrl = film.image
                });
            }

            return new SearchFilmResult
            {
                Films = FilmList
            };
        }
    }

    public class FilmsResponse
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string PosterUrl { get; set; }
    }
}
