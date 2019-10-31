﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Movies.Core.Entities;
using Movies.Core.Projections;

namespace Movies.Core.Services
{
    public interface IMoviesService
    {
        // Commands
        Task<MovieRating> RateMovieAsync(int movieId, int userId, int rating);

        // Queries
        Task<IList<Movie>> SearchMoviesAsync(string title, int? yearOfRelease, string[] genres);
        Task<IList<MovieWithAverageRating>> TopNMoviesAsync(int? userId = null, int n = 5);
    }
}