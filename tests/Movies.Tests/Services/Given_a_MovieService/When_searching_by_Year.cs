using FluentAssertions;
using Movies.Core.Entities;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Movies.Tests.Services.Given_a_MovieService
{
    public class When_searching_by_Year : Given_a_MovieService
    {
        private readonly IList<Movie> actual;

        public When_searching_by_Year()
        {
            // Arrange
            var dbc = GetMoviesDbContext();

            dbc.Movies.AddRange(new[]
            {
                new Movie { YearOfRelease = 2019 },
                new Movie { YearOfRelease = 2000 }
            });

            dbc.SaveChanges();

            // Act
            actual = SUT.SearchMoviesAsync(null, 2000, null).Result;
        }

        [Fact]
        public void Then_the_results_are_correct()
        {
            actual.Should().NotBeNull();
            actual.Count.Should().Be(1);
            actual.First().YearOfRelease.Should().Be(2000);
        }
    }
}
