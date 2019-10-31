using FluentAssertions;
using Movies.Core.Entities;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Movies.Tests.Services.Given_a_MovieService
{
    public class When_searching_by_Genres_and_Year : Given_a_MovieService
    {
        private readonly IList<Movie> actual;

        public When_searching_by_Genres_and_Year()
        {
            // Arrange
            var dbc = GetMoviesDbContext();

            dbc.Movies.AddRange(new[]
            {
                new Movie { Genre = "Action", YearOfRelease = 1999 },
                new Movie { Genre = "Romance", YearOfRelease = 1986 },
                new Movie { Genre = "Comedy", YearOfRelease = 1980 },
                new Movie { Genre = "Horror", YearOfRelease = 1979 }
            });

            dbc.SaveChanges();

            // Act
            actual = SUT.SearchMoviesAsync(null, 1999, new[] { "Action", "Horror" }).Result;
        }

        [Fact]
        public void Then_the_results_are_correct()
        {
            actual.Should().NotBeNull();
            actual.Count.Should().Be(1);
            actual.First().Genre.Should().Be("Action");
            actual.First().YearOfRelease.Should().Be(1999);
        }
    }
}
