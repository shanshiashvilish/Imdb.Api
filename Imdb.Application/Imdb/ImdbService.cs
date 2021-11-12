using Imdb.Application.Options;
using Imdb.Core.Imdb;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using System.Net.Http;
using Imdb.Core.Imdb.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System;

namespace Imdb.Application.Imdb
{
    public class ImdbService : IImdbService
    {
        private readonly ImdbApiConfiguration _configration;
        public ImdbService(IOptions<ImdbApiConfiguration> configuration)
        {
            _configration = configuration.Value;
        }

        public async Task<GetFilmPostersResultModel> GetFilmPostersAsync(string externalId)
        {
            string url = string.Format(_configration.GetPostersEndPoint, externalId);

            var responseContent = await GetResponseContentAsJsonAsync(url);
            var result = JsonConvert.DeserializeObject<GetFilmPostersResultModel>(responseContent);

            return result;
        }

        public async Task<FilmSearchResultModel> SearchFilmAsync(string filmName)
        {
            string url = string.Format(_configration.SeachFilmByNameEndPoint, filmName);

            var responseContent = await GetResponseContentAsJsonAsync(url);
            var result = JsonConvert.DeserializeObject<FilmSearchResultModel>(responseContent);

            return result;
        }

        public static async Task<string> GetResponseContentAsJsonAsync(string url)
        {
            using var httpClientHandler = new HttpClientHandler();
            using var client = new HttpClient(httpClientHandler);

            return await client.GetStringAsync(url);
        }

        public async Task<GetMostRatedFilmResultModel> GetMostRatedFilm(List<string> imdbIds)
        {
            var filmsDetailedList = new List<GetMostRatedFilmResultModel>();

            foreach (var imdbId in imdbIds)
            {
                string url = string.Format(_configration.GetFilmDetailsEndPoint, imdbId);

                var responseContent = await GetResponseContentAsJsonAsync(url);
                var film = JsonConvert.DeserializeObject<GetMostRatedFilmResultModel>(responseContent);

                filmsDetailedList.Add(film);
            }

            var filmToReturn = filmsDetailedList.OrderByDescending(x => x.imDbRating).FirstOrDefault();

            return filmToReturn;
        }

        public async Task<string> GetWikipediaDescription(string imdbId)
        {
            string url = string.Format(_configration.GetWikipediaDescriptionEndPoint, imdbId);

            var responseContent = await GetResponseContentAsJsonAsync(url);
            string descriptionFromWikipedia = JsonConvert.DeserializeObject<GetWikipediaDescriptionResultModel>(responseContent).plotShort.plainText;

            return descriptionFromWikipedia;
        }

        public async Task<List<string>> GetGenresByNameArray(string[] filmList)
        {
            List<string> result = new();

            foreach (var filmName in filmList.ToList())
            {
                string url = string.Format(_configration.SeachFilmByNameEndPoint, filmName);
                var responseContent = await GetResponseContentAsJsonAsync(url);
                var filmDetails = JsonConvert.DeserializeObject<FilmSearchResultModel>(responseContent);
                string filmImdbId = filmDetails.results.FirstOrDefault().id;

                string urlForDetails = string.Format(_configration.GetFilmDetailsEndPoint, filmImdbId);
                var responseContentForDetails = await GetResponseContentAsJsonAsync(urlForDetails);
                var filmDetailsByImdbId = JsonConvert.DeserializeObject<GetGenresByNameArrayResultModel>(responseContentForDetails);

                var genres = filmDetailsByImdbId.genres.Split(new[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries);
                result.AddRange(genres);
            }
            return result;
        }
    }
}
