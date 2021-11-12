using System.Collections.Generic;

namespace Imdb.Core.Imdb.Models
{
    public class FilmSearchResultModel
    {
        public string searchType { get; set; }
        public string expression { get; set; }
        public List<FilmResult> results { get; set; }
        public string errorMessage { get; set; }
    }

    public class FilmResult
    {
        public string id { get; set; }
        public string resultType { get; set; }
        public string image { get; set; }
        public string title { get; set; }
        public string description { get; set; }
    }
}
