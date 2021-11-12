using Imdb.Core.WatchLists;

namespace Imdb.Core.Users
{
    public class User
    {
        public int Id { get; set; }
        public int? WatchListId { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public WatchList WatchList { get; set; }
    }
}
