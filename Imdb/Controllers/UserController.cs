using Imdb.Api.Models.DataContracts;
using Imdb.Api.Models.RequestModels;
using Imdb.Core.Films;
using Imdb.Core.Users;
using Imdb.Core.WatchListByFilms;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Imdb.Api.Controllers
{
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private readonly IFilmService _filmService;
        private readonly IWatchListByFilmsService _watchListByFilmsService;

        public UserController(IUserService userService, 
                              IFilmService filmService, 
                              IWatchListByFilmsService watchListByFilmsService)
        {
            _userService = userService;
            _filmService = filmService;
            _watchListByFilmsService = watchListByFilmsService;
        }

        [HttpGet("GetUsersWatchList")]
        public async Task<ActionResult> GetUsersWatchList(int userId)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            try
            {
                var result = await _userService.GetUsersWatchListAsync(userId);

                return Ok(GetUsersWatchListDataContract.BuildFrom(result));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("AddToWatchList")]
        public async Task<ActionResult> AddToWatchList(AddToWatchListRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            try
            {
                await _filmService.AddFilmAsync(request.UserId, request.FilmName);

                return Ok($"Film {request.FilmName} was added to your watchlist");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("MarkAsWatched")]
        public async Task<ActionResult> MarkFilmWatched(int userId, int filmId)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            try
            {
                await _watchListByFilmsService.MarkAsWatched(userId, filmId);

                return Ok("Marked as watched");
            }

            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("AddNewUser")]
        public async Task<ActionResult> AddNewUser(string userName, string email)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            try
            {
                await _userService.AddUserAsync(userName, email);

                return Ok($"New user was created. Username: {userName}");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
