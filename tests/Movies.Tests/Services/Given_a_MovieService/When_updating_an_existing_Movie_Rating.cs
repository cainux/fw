using FluentAssertions;
using Movies.Core.Entities;
using System.Linq;
using Xunit;

namespace Movies.Tests.Services.Given_a_MovieService
{
    public class When_updating_an_existing_Movie_Rating : Given_a_MovieService
    {
        private readonly int actual;

        public When_updating_an_existing_Movie_Rating()
        {
            // Arrange
            var dbc = GetMoviesDbContext();

            dbc.Users.Add(new User { UserId = 1, Username = "User_01" });
            dbc.Movies.Add(new Movie { MovieId = 1, Title = "Movie_01" });
            dbc.MovieRatings.Add(new MovieRating { MovieId = 1, UserId = 1, Rating = 5 });

            dbc.SaveChanges();

            // Act
            actual = SUT.RateMovieAsync(1, 1, 1).Result;
        }

        [Fact]
        public void Then_1_row_should_have_been_saved()
        {
            actual.Should().Be(1);
        }

        [Fact]
        public void _and_it_should_be_saved_in_the_database()
        {
            var dbc = GetMoviesDbContext();

            var movieRating = dbc.MovieRatings.Single(x => x.MovieId == 1 && x.UserId == 1);

            movieRating.Should().BeEquivalentTo(new { Rating = 1 });
        }
    }
}
