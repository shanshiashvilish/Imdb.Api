using Imdb.Core.Films;
using Imdb.Core.Users;
using System;
using System.Collections.Generic;

namespace Imdb.Core.WatchLists
{
    public class WatchList
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public List<Film> Films { get; set; }
        public DateTime CreateDate { get; set; }
        public User User { get; set; }
    }
}
