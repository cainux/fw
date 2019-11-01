using Microsoft.EntityFrameworkCore;
using Movies.Core.Entities;
using Movies.Core.Repositories;
using Movies.Infrastructure.Data;
using System.Threading.Tasks;

namespace Movies.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly MoviesDbContext moviesDbContext;

        public UserRepository(MoviesDbContext moviesDbContext)
        {
            this.moviesDbContext = moviesDbContext;
        }

        public async Task<User> Get(int userId)
        {
            return await moviesDbContext
                .Users
                .SingleOrDefaultAsync(x => x.UserId == userId);
        }
    }

    public class MovieRatingRepository : IMovieRatingRepository
    {
        private readonly MoviesDbContext moviesDbContext;

        public MovieRatingRepository(MoviesDbContext moviesDbContext)
        {
            this.moviesDbContext = moviesDbContext;
        }

        public async Task<int> Upsert(MovieRating value)
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
                moviesDbContext.MovieRatings.Update(value);
            }

            return await moviesDbContext.SaveChangesAsync();
        }
    }
}
