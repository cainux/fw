using FluentAssertions;
using Movies.Core.Entities;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Movies.Tests.Services.Given_a_MovieService
{
    public class When_searching_by_Title : Given_a_MovieService
    {
        private readonly IList<Movie> actual;

        public When_searching_by_Title()
        {
            // Arrange
            var dbc = GetMoviesDbContext();

            dbc.Movies.AddRange(new[]
            {
                new Movie { Title = "FooMovie" },
                new Movie { Title = "BarMovie" }
            });

            dbc.SaveChanges();

            // Act
            actual = SUT.SearchMoviesAsync("FooMovie", null, null).Result;
        }

        [Fact]
        public void Then_the_results_are_correct()
        {
            actual.Should().NotBeNull();
            actual.Count.Should().Be(1);
            actual.First().Title.Should().Be("FooMovie");
        }
    }
}
