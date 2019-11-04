using FluentAssertions;
using Movies.Core.Entities;
using System.Collections.Generic;
using Xunit;

namespace Movies.Tests.Services.Given_a_MovieService
{
    public class When_fetching_Top_N_Movies_for_a_User : Given_a_MovieService
    {
        private readonly IList<Movie> actual;

        public When_fetching_Top_N_Movies_for_a_User()
        {
            // Arrange
            var dbc = GetMoviesDbContext();

            dbc.Users.AddRange(new[]
            {
                new User { UserId = 1, Username = "User_01"},
                new User { UserId = 2, Username = "User_02"},
                new User { UserId = 3, Username = "User_03"},
                new User { UserId = 4, Username = "User_04"},
                new User { UserId = 5, Username = "User_05"},
                new User { UserId = 6, Username = "User_06"},
                new User { UserId = 7, Username = "User_07"}
            });

            dbc.Movies.AddRange(new[]
            {
                new Movie { Id = 1, Title = "Movie_01" },
                new Movie { Id = 2, Title = "Movie_02" },
                new Movie { Id = 3, Title = "Movie_03" },
                new Movie { Id = 4, Title = "Movie_04" },
                new Movie { Id = 5, Title = "Movie_05" },
                new Movie { Id = 6, Title = "Movie_06" },
                new Movie { Id = 7, Title = "Movie_07" }
            });

            dbc.MovieRatings.AddRange(new[]
            {
                // Movie 1 - 5
                new MovieRating { MovieId = 1, UserId = 1, Rating = 5 },
                new MovieRating { MovieId = 1, UserId = 2, Rating = 5 },
                new MovieRating { MovieId = 1, UserId = 3, Rating = 5 },
                new MovieRating { MovieId = 1, UserId = 4, Rating = 5 },

                // Movie 2 - 4.5
                new MovieRating { MovieId = 2, UserId = 1, Rating = 5 },
                new MovieRating { MovieId = 2, UserId = 2, Rating = 4 },

                // Movie 4 - 2.5
                new MovieRating { MovieId = 4, UserId = 1, Rating = 4 },
                new MovieRating { MovieId = 4, UserId = 2, Rating = 3 },
                new MovieRating { MovieId = 4, UserId = 3, Rating = 2 },
                new MovieRating { MovieId = 4, UserId = 4, Rating = 1 },

                // Movie 6 - 2
                new MovieRating { MovieId = 6, UserId = 1, Rating = 2 },

                // Movie 7 - 1
                new MovieRating { MovieId = 7, UserId = 1, Rating = 1 }
            });

            dbc.SaveChanges();

            var userId = 1;

            // Act
            actual = SUT.TopNMoviesAsync(userId, 5).Result;
        }

        [Fact]
        public void Then_the_results_are_correct()
        {
            actual.Should().HaveCount(5);
            actual.Should().BeEquivalentTo(
                new { Title = "Movie_01", Rating = 5.0d },
                new { Title = "Movie_02", Rating = 5.0d },
                new { Title = "Movie_04", Rating = 4.0d },
                new { Title = "Movie_06", Rating = 2.0d },
                new { Title = "Movie_07", Rating = 1.0d }
            );
        }
    }
}
