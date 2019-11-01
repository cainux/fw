using System.Threading.Tasks;
using Movies.Core.Entities;

namespace Movies.Core.Repositories
{
    public interface IMovieRatingRepository
    {
        Task<int> Upsert(MovieRating value);
    }
}