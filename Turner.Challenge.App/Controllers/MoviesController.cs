using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Turner.Challenge.App.Models;
using Turner.Challenge.App.Repository;

namespace Turner.Challenge.App.Controllers
{
    [Route("api/[controller]")]
    public class MoviesController : Controller
    {
        private readonly IMoviesRepository _moviesRepository;
        private readonly IMovieTitleRepository _movieTitleRepository;
        public MoviesController(IMoviesRepository moviesRepository, IMovieTitleRepository movieTitleRepository)
        {
            _moviesRepository = moviesRepository;
            _movieTitleRepository = movieTitleRepository;
        }
        
        [HttpGet("titles")]
        public async Task<ActionResult<IEnumerable<MovieTitle>>> GetAllByName(string name)
        {
            if (String.IsNullOrWhiteSpace(name))
                return NoContent();

            var movieTiles = await _movieTitleRepository.GetByNameAsync(name);

            if (movieTiles != null)
            {
                return movieTiles;
            }

            return NotFound();
        }

    }
}
