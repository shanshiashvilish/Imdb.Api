
namespace Imdb.Core.Imdb.Models
{
    public class GetMostRatedFilmResultModel
    {
        public string id { get; set; }
        public string title { get; set; }
        public string originalTitle { get; set; }
        public string fullTitle { get; set; }
        public string image { get; set; }
        public string genres { get; set; }
        public string contentRating { get; set; }
        public string imDbRating { get; set; }
        public string imDbRatingVotes { get; set; }
        public string metacriticRating { get; set; }
        public object wikipedia { get; set; }
        public string posters { get; set; }
        public object images { get; set; }
        public string keywords { get; set; }
        public string errorMessage { get; set; }
    }
}
