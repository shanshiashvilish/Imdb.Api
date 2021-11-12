using Imdb.Api.Models.DataContracts;
using Imdb.Core.Imdb;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Imdb.Api.Controllers
{
    [Route("api/[controller]")]
    public class ImdbController : Controller
    {
        private readonly IImdbService _imdbService;
        
        public ImdbController(IImdbService imdbService)
        {
            _imdbService = imdbService;
        }

        [HttpGet("SearchFilmByName")]
        public async Task<ActionResult> SearchFilmByName(string filmName)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            try
            {
                var result = await _imdbService.SearchFilmAsync(filmName);

                return Ok(SearchFilmResult.BuildFrom(result));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetGenresByFilmNames")]
        public async Task<ActionResult> GetGenresByFilmNames(string[] filmNames)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            try
            {
                var result = await _imdbService.GetGenresByNameArray(filmNames);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
