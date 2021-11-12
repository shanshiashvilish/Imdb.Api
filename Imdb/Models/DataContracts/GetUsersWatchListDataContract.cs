using Imdb.Core.WatchLists;
using System;
using System.Collections.Generic;

namespace Imdb.Api.Models.DataContracts
{
    public class GetUsersWatchListDataContract
    {
        public int UserId { get; set; }
        public List<UsersFilm> Films { get; set; }

        public static GetUsersWatchListDataContract BuildFrom(WatchList watchList)
        {
            var filmList = new List<UsersFilm>();
            var filmsFromWatchList = watchList.Films;

            foreach (var film in filmsFromWatchList)
            {
                filmList.Add(new UsersFilm
                {
                    Title = film.FilmName,
                    ImdbId = film.ImdbId,
                });
            }

            return new GetUsersWatchListDataContract
            {
                UserId = watchList.UserId,
                Films = filmList
            };
        }
    }

    public class UsersFilm
    {
        public string Title { get; set; }
        public string ImdbId { get; set; }
        public bool IsWatched { get; set; }
        public DateTime DateOfAddToWatchList { get; set; }
    }
}
