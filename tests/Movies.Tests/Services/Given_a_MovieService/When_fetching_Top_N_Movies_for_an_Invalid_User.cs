using FluentAssertions;
using Movies.Core.Exceptions;
using System;
using Xunit;

namespace Movies.Tests.Services.Given_a_MovieService
{
    public class When_fetching_Top_N_Movies_for_an_Invalid_User : Given_a_MovieService
    {
        private readonly UserNotFoundException actual = null;

        public When_fetching_Top_N_Movies_for_an_Invalid_User()
        {
            // Arrange
            var userId = 1;

            // Act
            try
            {
                _ = SUT.TopNMoviesAsync(userId, 5).Result;
            }
            catch (AggregateException e) // This is a bit fishy
            {
                actual = (UserNotFoundException) e.InnerException;
            }
        }

        [Fact]
        public void Then_UserNotFoundException_should_be_thrown()
        {
            actual.Should().NotBeNull();
        }
    }
}
