using FluentAssertions;
using Movies.Core.Entities;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Movies.Tests.Services.Given_a_MovieService
{
    public class When_searching_by_single_Genre : Given_a_MovieService
    {
        private readonly IList<Movie> actual;

        public When_searching_by_single_Genre()
        {
            // Arrange
            var dbc = GetMoviesDbContext();

            dbc.Movies.AddRange(new[]
            {
                new Movie { Genre = "Action" },
                new Movie { Genre = "Romance" }
            });

            dbc.SaveChanges();

            // Act
            actual = SUT.SearchMoviesAsync(null, null, new[] { "Romance" }).Result;
        }

        [Fact]
        public void Then_the_results_are_correct()
        {
            actual.Should().NotBeNull();
            actual.Count.Should().Be(1);
            actual.First().Genre.Should().Be("Romance");
        }
    }
}
