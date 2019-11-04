using System.Collections.Generic;
using System.Threading.Tasks;
using Movies.Core.Entities;

namespace Movies.Core.Repositories
{
    public interface IMovieRepository
    {
        // Commands
        Task<int> UpdateAverageRating(int movieId, double avgRating);

        // Queries
        Task<Movie> Get(int movieId);
        Task<IList<Movie>> SearchMoviesAsync(string title, int? yearOfRelease, string[] genres);
        Task<IList<Movie>> TopNMoviesAsync(int n = 5);
        Task<IList<Movie>> TopNMoviesAsync(int userId, int n = 5);
    }
}