using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Movies.Core.Entities;
using Movies.Core.Exceptions;
using Movies.Core.Repositories;
using Movies.Core.Util;

namespace Movies.Core.Services
{
    public class MoviesService : IMoviesService
    {
        private readonly IMovieRepository movieRepository;
        private readonly IUserRepository userRepository;
        private readonly IMovieRatingRepository movieRatingRepository;

        public MoviesService(IMovieRepository movieRepository, IUserRepository userRepository, IMovieRatingRepository movieRatingRepository)
        {
            this.movieRepository = movieRepository;
            this.userRepository = userRepository;
            this.movieRatingRepository = movieRatingRepository;
        }

        public async Task<int> RateMovieAsync(int movieId, int userId, int rating)
        {
            if (rating < 1 || rating > 5)
                throw new InvalidRatingException(rating);

            var movieGetter = movieRepository.Get(movieId);
            var userGetter = userRepository.Get(userId);

            if (await movieGetter == null)
                throw new MovieNotFoundException(movieId);

            if (await userGetter == null)
                throw new UserNotFoundException(userId);

            var ratingSavedResult = await movieRatingRepository.UpsertAsync(new MovieRating
            {
                MovieId = movieId,
                UserId = userId,
                Rating = rating
            });

            var ratings = await movieRatingRepository.GetByMovieId(movieId);
            var roundedAverageRating = Rounder.Round(ratings.Average(x => x.Rating));

            return await movieRepository.UpdateAverageRating(movieId, roundedAverageRating);
        }

        public async Task<IList<Movie>> SearchMoviesAsync(string title, int? yearOfRelease, string[] genres)
        {
            if (string.IsNullOrWhiteSpace(title) && yearOfRelease == null && (genres == null || genres.Length == 0))
                throw new EmptySearchCriteriaException();

            return await movieRepository.SearchMoviesAsync(title, yearOfRelease, genres);
        }

        public async Task<IList<Movie>> TopNMoviesAsync(int? userId = null, int n = 5)
        {
            if (userId.HasValue)
            {
                if (await userRepository.Get(userId.Value) == null)
                    throw new UserNotFoundException(userId.Value);
                else
                    return await movieRepository.TopNMoviesAsync(userId.Value, n);
            }
 
            return await movieRepository.TopNMoviesAsync(n);
        }
    }
}