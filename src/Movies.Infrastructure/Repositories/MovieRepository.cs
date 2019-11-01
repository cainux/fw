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

        public async Task<Movie> Get(int movieId)
        {
            return await moviesDbContext
                .Movies
                .SingleOrDefaultAsync(x => x.MovieId == movieId);
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

        public async Task<IList<MovieWithRating>> TopNMoviesAsync(int n = 5)
        {
            return await moviesDbContext
                .Movies
                .Include(x => x.Ratings)
                .Select(x => new MovieWithRating
                {
                    MovieId = x.MovieId,
                    Title = x.Title,
                    YearOfRelease = x.YearOfRelease,
                    RunningTime = x.RunningTime,
                    Genre = x.Genre,
                    Rating = x.Ratings.Any() ? x.Ratings.Average(y => y.Rating) : 0d
                })
                .OrderByDescending(x => x.Rating)
                .ThenBy(x => x.Title)
                .Take(n)
                .ToListAsync();
        }

        public async Task<IList<MovieWithRating>> TopNMoviesAsync(int userId, int n = 5)
        {
            return await moviesDbContext
                .MovieRatings
                .Include(x => x.Movie)
                .Where(x => x.UserId == userId)
                .Select(x => new MovieWithRating
                {
                    MovieId = x.Movie.MovieId,
                    Title = x.Movie.Title,
                    YearOfRelease = x.Movie.YearOfRelease,
                    RunningTime = x.Movie.RunningTime,
                    Genre = x.Movie.Genre,
                    Rating = x.Rating
                })
                .OrderByDescending(x => x.Rating)
                .ThenBy(x => x.Title)
                .Take(n)
                .ToListAsync();
        }
    }
}
