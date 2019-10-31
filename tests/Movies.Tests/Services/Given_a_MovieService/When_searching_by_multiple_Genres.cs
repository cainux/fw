using FluentAssertions;
using Movies.Core.Entities;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Movies.Tests.Services.Given_a_MovieService
{
    public class When_searching_by_multiple_Genres : Given_a_MovieService
    {
        private readonly IList<Movie> actual;

        public When_searching_by_multiple_Genres()
        {
            // Arrange
            var dbc = GetMoviesDbContext();

            dbc.Movies.AddRange(new[]
            {
                new Movie { Genre = "Action" },
                new Movie { Genre = "Romance" },
                new Movie { Genre = "Comedy" },
                new Movie { Genre = "Horror" }
            });

            dbc.SaveChanges();

            // Act
            actual = SUT.SearchMoviesAsync(null, null, new[] { "Action", "Horror" }).Result;
        }

        [Fact]
        public void Then_the_results_are_correct()
        {
            actual.Should().NotBeNull();
            actual.Count.Should().Be(2);
            actual.Where(x => x.Genre == "Action").Should().HaveCount(1);
            actual.Where(x => x.Genre == "Horror").Should().HaveCount(1);
        }
    }
}
