using System.Collections.Generic;
using System.Threading.Tasks;
using Movies.Core.Entities;

namespace Movies.Core.Repositories
{
    public interface IMovieRepository
    {
        Task<Movie> Get(int movieId);
        Task<IList<Movie>> SearchMoviesAsync(string title, int? yearOfRelease, string[] genres);
        Task<IList<Movie>> TopNMoviesAsync(int? userId = null, int n = 5);
    }
}