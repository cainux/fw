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
}
