using System.Collections.Generic;
using System.Threading.Tasks;
using Movies.Core.Entities;

namespace Movies.Core.Repositories
{
    public interface IMovieRatingRepository
    {
        // Command
        Task<int> UpsertAsync(MovieRating value);

        // Queries
        Task<IList<MovieRating>> GetByMovieId(int movieId);
    }
}