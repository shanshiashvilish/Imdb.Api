
namespace Imdb.Core.Imdb.Models
{

    public class PlotShort
    {
        public string plainText { get; set; }
        public string html { get; set; }
    }

    public class PlotFull
    {
        public string plainText { get; set; }
        public string html { get; set; }
    }

    public class GetWikipediaDescriptionResultModel
    {
        public string imDbId { get; set; }
        public string title { get; set; }
        public string fullTitle { get; set; }
        public string type { get; set; }
        public string year { get; set; }
        public string language { get; set; }
        public string titleInLanguage { get; set; }
        public string url { get; set; }
        public PlotShort plotShort { get; set; }
        public PlotFull plotFull { get; set; }
        public string errorMessage { get; set; }
    }

}
