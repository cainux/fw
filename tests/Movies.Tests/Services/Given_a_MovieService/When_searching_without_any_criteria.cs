using FluentAssertions;
using Movies.Core.Exceptions;
using Xunit;

namespace Movies.Tests.Services.Given_a_MovieService
{
    public class When_searching_without_any_criteria : Given_a_MovieService
    {
        private readonly EmptySearchCriteriaException actual = null;

        public When_searching_without_any_criteria()
        {
            // Act
            try
            {
                SUT.SearchMoviesAsync(null, null, null);
            }
            catch (EmptySearchCriteriaException e)
            {
                actual = e;
            }
        }

        [Fact]
        public void Then_EmptySearchCriteriaException_should_be_thrown()
        {
            actual.Should().NotBeNull();
        }
    }
}
