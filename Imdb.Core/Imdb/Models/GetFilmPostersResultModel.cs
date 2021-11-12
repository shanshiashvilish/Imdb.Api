using System.Collections.Generic;

namespace Imdb.Core.Imdb.Models
{
    public class GetFilmPostersResultModel
    {
        public string imDbId { get; set; }
        public string title { get; set; }
        public string fullTitle { get; set; }
        public string type { get; set; }
        public string year { get; set; }
        public List<Poster> posters { get; set; }
    }

    public class Poster
    {
        public string id { get; set; }
        public string link { get; set; }
        public double aspectRatio { get; set; }
        public string language { get; set; }
        public int width { get; set; }
        public int height { get; set; }
    }
}
