using System.Collections.Generic;
using System.Threading.Tasks;
using Movies.Core.Entities;
using Movies.Core.Projections;

namespace Movies.Core.Repositories
{
    public interface IMovieRepository
    {
        Task<Movie> Get(int movieId);
        Task<IList<Movie>> SearchMoviesAsync(string title, int? yearOfRelease, string[] genres);
        Task<IList<MovieWithRating>> TopNMoviesAsync(int n = 5);
        Task<IList<MovieWithRating>> TopNMoviesAsync(int userId, int n = 5);
    }
}