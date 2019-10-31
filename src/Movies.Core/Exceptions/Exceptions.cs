using System;

namespace Movies.Core.Exceptions
{
    public class EmptySearchCriteriaException : Exception
    {
        public EmptySearchCriteriaException() : base("No search criteria provided") { }
    }

    public class MovieNotFoundException : Exception
    {
        public MovieNotFoundException(int movieId) : base($"MovieId: {movieId} not found.") { }
    }

    public class UserNotFoundException : Exception
    {
        public UserNotFoundException(int userId) : base($"UserId: {userId} not found.") { }
    }

    public class InvalidRatingException : Exception
    {
        public InvalidRatingException(int rating) : base($"Invalid Rating: {rating} (valid ratings are 1 to 5)") { }
    }
}