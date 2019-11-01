using FluentAssertions;
using Movies.Core.Entities;
using System.Linq;
using Xunit;

namespace Movies.Tests.Services.Given_a_MovieService
{
    public class When_rating_a_Movie_with_a_valid_Rating : Given_a_MovieService
    {
        private readonly MovieRating actual;

        public When_rating_a_Movie_with_a_valid_Rating()
        {
            // Arrange
            var dbc = GetMoviesDbContext();

            dbc.Users.Add(new User { UserId = 1, Username = "User_01" });
            dbc.Movies.Add(new Movie { MovieId = 1, Title = "Movie_01" });

            dbc.SaveChanges();

            // Act
            actual = SUT.RateMovieAsync(1, 1, 5).Result;
        }

        [Fact]
        public void Then_the_rating_should_be_returned()
        {
            actual.Should().BeEquivalentTo(new
            {
                MovieId = 1,
                UserId = 1,
                Rating = 5
            });
        }

        [Fact]
        public void _and_it_should_be_saved_in_the_database()
        {
            var dbc = GetMoviesDbContext();

            var movieRating = dbc.MovieRatings.Single(x => x.MovieId == 1 && x.UserId == 1);

            movieRating.Should().BeEquivalentTo(new { Rating = 5 });
        }
    }
}
