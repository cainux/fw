using System.Collections.Generic;
using System.Threading.Tasks;
using Movies.Core.Entities;
using Movies.Core.Exceptions;
using Movies.Core.Repositories;

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

        public Task<MovieRating> RateMovieAsync(int movieId, int userId, int rating)
        {
            if (rating < 0 || rating > 5)
                throw new InvalidRatingException(rating);

            var movie = movieRepository.Get(movieId);

            if (movie == null)
                throw new MovieNotFoundException(movieId);

            var user = userRepository.Get(userId);

            if (user == null)
                throw new UserNotFoundException(userId);

            return movieRatingRepository.Upsert(new MovieRating
            {
                MovieId = movieId,
                UserId = userId,
                Rating = rating
            });
        }

        public Task<IList<Movie>> SearchMoviesAsync(string title, int? yearOfRelease, string[] genres)
        {
            if (string.IsNullOrWhiteSpace(title) && yearOfRelease == null && (genres == null || genres.Length == 0))
                throw new EmptySearchCriteriaException();

            return movieRepository.SearchMoviesAsync(title, yearOfRelease, genres);
        }

        public Task<IList<Movie>> TopNMoviesAsync(int? userId = null, int n = 5)
        {
            return movieRepository.TopNMoviesAsync(userId, n);
        }
    }
}