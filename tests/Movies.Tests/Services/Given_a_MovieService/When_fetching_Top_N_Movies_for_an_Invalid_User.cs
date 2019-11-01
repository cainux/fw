using FluentAssertions;
using Movies.Core.Exceptions;
using System;
using Xunit;

namespace Movies.Tests.Services.Given_a_MovieService
{
    public class When_fetching_Top_N_Movies_for_an_Invalid_User : Given_a_MovieService
    {
        private readonly Exception actual;

        public When_fetching_Top_N_Movies_for_an_Invalid_User()
        {
            // Act
            actual = Record.Exception(SUT.TopNMoviesAsync(1, 5).Wait);
        }

        [Fact]
        public void Then_UserNotFoundException_should_be_thrown()
        {
            actual.InnerException.Should().BeOfType<UserNotFoundException>();
        }
    }
}
