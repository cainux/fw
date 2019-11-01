using FluentAssertions;
using Movies.Core.Exceptions;
using System;
using Xunit;

namespace Movies.Tests.Services.Given_a_MovieService
{
    public class When_searching_without_any_criteria : Given_a_MovieService
    {
        private readonly Exception actual;

        public When_searching_without_any_criteria()
        {
            // Act
            actual = Record.Exception(SUT.SearchMoviesAsync(null, null, null).Wait);
        }

        [Fact]
        public void Then_EmptySearchCriteriaException_should_be_thrown()
        {
            actual.InnerException.Should().BeOfType<EmptySearchCriteriaException>();
        }
    }
}
