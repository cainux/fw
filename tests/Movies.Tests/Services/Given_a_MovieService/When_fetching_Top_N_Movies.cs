using FluentAssertions;
using Movies.Core.Entities;
using System.Collections.Generic;
using Xunit;

namespace Movies.Tests.Services.Given_a_MovieService
{
    public class When_fetching_Top_N_Movies : Given_a_MovieService
    {
        private readonly IList<Movie> actual;

        public When_fetching_Top_N_Movies()
        {
            // Arrange
            var dbc = GetMoviesDbContext();

            dbc.Movies.AddRange(new[]
            {
                new Movie { Id = 1, Title = "Movie_01", AverageRating = 5 },
                new Movie { Id = 2, Title = "Movie_02", AverageRating = 4.5 },
                new Movie { Id = 3, Title = "Movie_03" },
                new Movie { Id = 4, Title = "Movie_04", AverageRating = 2.5 },
                new Movie { Id = 5, Title = "Movie_05" },
                new Movie { Id = 6, Title = "Movie_06", AverageRating = 2 },
                new Movie { Id = 7, Title = "Movie_07", AverageRating = 1 }
            });

            dbc.SaveChanges();

            // Act
            actual = SUT.TopNMoviesAsync(n: 5).Result;
        }

        [Fact]
        public void Then_the_results_are_correct()
        {
            actual.Should().HaveCount(5);
            actual.Should().BeEquivalentTo(
                new { Title = "Movie_01", AverageRating = 5.0d },
                new { Title = "Movie_02", AverageRating = 4.5d },
                new { Title = "Movie_04", AverageRating = 2.5d },
                new { Title = "Movie_06", AverageRating = 2.0d },
                new { Title = "Movie_07", AverageRating = 1.0d }
            );
        }
    }
}
