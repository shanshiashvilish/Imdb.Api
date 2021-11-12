using Imdb.Core.Films;
using Imdb.Core.WatchLists;
using System;

namespace Imdb.Core.WatchListByFilms
{
    public class WatchListByFilms
    {
        public int Id { get; set; }
        public int FilmId { get; set; }
        public int WatchListId { get; set; }
        public bool IsWatched { get; set; }
        public WatchList WatchList { get; set; }
        public Film Film { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
