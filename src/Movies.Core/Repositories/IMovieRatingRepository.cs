using System.Threading.Tasks;
using Movies.Core.Entities;

namespace Movies.Core.Repositories
{
    public interface IMovieRatingRepository
    {
        Task<MovieRating> Get(int movieId, int userId);
        Task<MovieRating> Upsert(MovieRating value);
    }
}