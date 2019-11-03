using Microsoft.AspNetCore.Mvc;
using Movies.Core.Entities;
using Movies.Core.Projections;
using Movies.Core.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Movies.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MoviesController : ControllerBase
    {
        private readonly IMoviesService moviesService;

        public MoviesController(IMoviesService moviesService)
        {
            this.moviesService = moviesService;
        }

        [HttpGet("search")]
        public async Task<IEnumerable<Movie>> Search([FromQuery] string title, [FromQuery] int? yearOfRelease, [FromQuery] string[] genres)
        {
            // TODO: Add try/catch for error codes
            // TODO: This should return the movies with average rating!
            return await moviesService.SearchMoviesAsync(title, yearOfRelease, genres);
        }

        [HttpGet("top5")]
        public async Task<IEnumerable<MovieWithRating>> Top5([FromQuery] int? userId)
        {
            // TODO: try/catches for error codes
            return await moviesService.TopNMoviesAsync(userId, 5);
        }

        [HttpPost]
        public async Task<int> RateMovie(int movieId, int userId, int rating)
        {
            // TODO: Add try/catch for error codes
            return await moviesService.RateMovieAsync(movieId, userId, rating);
        }
    }
}
