﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Movies.Core.Exceptions;
using Movies.Core.Services;
using Movies.WebApi.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Movies.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MoviesController : ControllerBase
    {
        private readonly IMoviesService moviesService;
        private readonly IMapper mapper;

        public MoviesController(IMoviesService moviesService, IMapper mapper)
        {
            this.moviesService = moviesService;
            this.mapper = mapper;
        }

        [HttpGet("search")]
        public async Task<IActionResult> Search([FromQuery] string title, [FromQuery] int? yearOfRelease, [FromQuery] string[] genres)
        {
            try
            {
                var result = await moviesService.SearchMoviesAsync(title, yearOfRelease, genres);

                if (result.Count > 0)
                {
                    return Ok(mapper.Map<IList<MovieViewModel>>(result));
                }
                else
                {
                    return NotFound();
                }
            }
            catch (EmptySearchCriteriaException e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("top5")]
        public async Task<IActionResult> Top5([FromQuery] int? userId)
        {
            try
            {
                var result = await moviesService.TopNMoviesAsync(userId, 5);

                return Ok(mapper.Map<IList<MovieViewModel>>(result));
            }
            catch (UserNotFoundException e)
            {
                return NotFound(e.Message);
            }
        }

        [HttpPost("ratemovie")]
        public async Task<IActionResult> RateMovie([FromBody] MovieRatingViewModel value)
        {
            try
            {
                var results = await moviesService.RateMovieAsync(value.MovieId, value.UserId, value.Rating);

                return Ok(results);
            }
            catch (InvalidRatingException e)
            {
                return BadRequest(e.Message);
            }
            catch (Exception e) when (e is MovieNotFoundException || e is UserNotFoundException)
            {
                return NotFound(e.Message);
            }
        }
    }
}
