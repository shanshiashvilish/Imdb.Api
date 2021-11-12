
namespace Imdb.Api.Models.RequestModels
{
    public class AddToWatchListRequest
    {
        public int UserId { get; set; }
        public string FilmName { get; set; }
    }
}
