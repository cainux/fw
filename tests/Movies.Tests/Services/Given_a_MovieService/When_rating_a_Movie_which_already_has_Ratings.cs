using FluentAssertions;
using Movies.Core.Entities;
using System.Linq;
using Xunit;

namespace Movies.Tests.Services.Given_a_MovieService
{
    public class When_rating_a_Movie_which_already_has_Ratings : Given_a_MovieService
    {
        private readonly Movie actual;

        public When_rating_a_Movie_which_already_has_Ratings()
        {
            // Arrange
            var dbc = GetMoviesDbContext();

            dbc.Users.AddRange(new[]
            {
                new User { UserId = 1, Username = "User_01" },
                new User { UserId = 2, Username = "User_02" },
                new User { UserId = 3, Username = "User_03" },
                new User { UserId = 4, Username = "User_04" }
            });

            dbc.Movies.Add(new Movie { Id = 1, Title = "Movie_01" });

            dbc.MovieRatings.AddRange(new[]
            {
                new MovieRating { MovieId = 1, UserId = 1, Rating = 5 },
                new MovieRating { MovieId = 1, UserId = 2, Rating = 4 },
                new MovieRating { MovieId = 1, UserId = 3, Rating = 3 }
            });

            dbc.SaveChanges();

            // Act
            _ = SUT.RateMovieAsync(1, 4, 2).Result;

            actual = dbc.Movies.First();
        }

        [Fact]
        public void Then_the_average_rating_should_be_updated()
        {
            actual.AverageRating.Should().Be(3.5d);
        }
    }
}
