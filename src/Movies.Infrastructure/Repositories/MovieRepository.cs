using Microsoft.EntityFrameworkCore;
using Movies.Core.Entities;
using Movies.Core.Projections;
using Movies.Core.Repositories;
using Movies.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Movies.Infrastructure.Repositories
{
    public class MovieRepository : IMovieRepository
    {
        private readonly MoviesDbContext moviesDbContext;

        public MovieRepository(MoviesDbContext moviesDbContext)
        {
            this.moviesDbContext = moviesDbContext;
        }

        public Task<Movie> Get(int movieId)
        {
            throw new NotImplementedException();
        }

        public async Task<IList<Movie>> SearchMoviesAsync(string title, int? yearOfRelease, string[] genres)
        {
            return await moviesDbContext
                .Movies
                .Where(x =>
                    (string.IsNullOrWhiteSpace(title) || x.Title.Contains(title))
                    && (!yearOfRelease.HasValue || x.YearOfRelease == yearOfRelease.Value)
                    && ((genres == null || genres.Length == 0) || genres.Contains(x.Genre))
                )
                .OrderBy(x => x.Title)
                .ToListAsync();
        }

        public async Task<IList<MovieWithAverageRating>> TopNMoviesAsync(int? userId = null, int n = 5)
        {
            return await moviesDbContext
                .Movies
                .Include(x => x.Ratings)
                .Select(x => new MovieWithAverageRating
                {
                    MovieId = x.MovieId,
                    Title = x.Title,
                    YearOfRelease = x.YearOfRelease,
                    RunningTime = x.RunningTime,
                    Genre = x.Genre,
                    AverageRating = x.Ratings.Any() ? x.Ratings.Average(y => y.Rating) : 0d
                })
                .OrderByDescending(x => x.AverageRating)
                .ThenBy(x => x.Title)
                .Take(n)
                .ToListAsync();            
        }
    }
}
