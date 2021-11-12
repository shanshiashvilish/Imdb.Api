using System.Collections.Generic;

namespace Imdb.Core.Imdb.Models
{
    public class GetGenresByNameArrayResultModel
    {
        public string id { get; set; }
        public string title { get; set; }
        public string originalTitle { get; set; }
        public string fullTitle { get; set; }
        public string genres { get; set; }
        public string errorMessage { get; set; }
    }
}
