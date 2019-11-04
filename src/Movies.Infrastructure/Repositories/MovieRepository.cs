using Microsoft.EntityFrameworkCore;
using Movies.Core.Entities;
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

        public async Task<int> UpdateAverageRating(int movieId, double avgRating)
        {
            var movie = await moviesDbContext
                .Movies
                .SingleAsync(x => x.Id == movieId);

            movie.AverageRating = avgRating;

            moviesDbContext.Movies.Update(movie);

            return await moviesDbContext.SaveChangesAsync();
        }

        public async Task<Movie> Get(int movieId)
        {
            return await moviesDbContext
                .Movies
                .SingleOrDefaultAsync(x => x.Id == movieId);
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

        public async Task<IList<Movie>> TopNMoviesAsync(int n = 5)
        {
            return await moviesDbContext
                .Movies
                .OrderByDescending(x => x.AverageRating)
                .ThenBy(x => x.Title)
                .Take(n)
                .ToListAsync();
        }

        public async Task<IList<Movie>> TopNMoviesAsync(int userId, int n = 5)
        {
            return await moviesDbContext
                .MovieRatings
                .Include(x => x.Movie)
                .Where(x => x.UserId == userId)
                .Select(x => new Movie
                {
                    Id = x.Movie.Id,
                    Title = x.Movie.Title,
                    YearOfRelease = x.Movie.YearOfRelease,
                    RunningTime = x.Movie.RunningTime,
                    Genre = x.Movie.Genre,
                    AverageRating = x.Rating
                })
                .OrderByDescending(x => x.AverageRating)
                .ThenBy(x => x.Title)
                .Take(n)
                .ToListAsync();
        }
    }
}
