using Imdb.Core.WatchLists;
using System;
using System.Collections.Generic;

namespace Imdb.Core.Films
{
    public class Film
    {
        public int Id { get; set; }
        public string ImdbId { get; set; }
        public string FilmName { get; set; }
        public List<WatchList> WatchList { get; set; }
        public DateTime DateOfCreate { get; set; }
    }
}
