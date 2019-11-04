using Microsoft.EntityFrameworkCore;
using Movies.Core.Entities;
using Movies.Core.Repositories;
using Movies.Infrastructure.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Movies.Infrastructure.Repositories
{
    public class MovieRatingRepository : IMovieRatingRepository
    {
        private readonly MoviesDbContext moviesDbContext;

        public MovieRatingRepository(MoviesDbContext moviesDbContext)
        {
            this.moviesDbContext = moviesDbContext;
        }

        public async Task<int> UpsertAsync(MovieRating value)
        {
            var existingMovieRating = await moviesDbContext
                .MovieRatings
                .SingleOrDefaultAsync(x => x.MovieId == value.MovieId
                                           && x.UserId == value.UserId);

            if (existingMovieRating == null)
            {
                moviesDbContext.MovieRatings.Add(value);   
            }
            else
            {
                existingMovieRating.Rating = value.Rating;
                moviesDbContext.MovieRatings.Update(existingMovieRating);
            }

            return await moviesDbContext.SaveChangesAsync();
        }

        public async Task<IList<MovieRating>> GetByMovieId(int movieId)
        {
            return await moviesDbContext
                .MovieRatings
                .Where(x => x.MovieId == movieId)
                .ToListAsync();
        }
    }
}
