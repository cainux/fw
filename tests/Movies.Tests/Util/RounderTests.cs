using FluentAssertions;
using Movies.Core.Util;
using Xunit;

namespace Movies.Tests.Util
{
    public class RounderTests
    {
        [Theory]
        [InlineData(2.91, 3.0)]
        [InlineData(3.249, 3.0)]
        [InlineData(3.25, 3.5)]
        [InlineData(3.6, 3.5)]
        [InlineData(3.75, 4.0)]
        public void Test_rounding(double input, double expected)
        {
            var actual = Rounder.Round(input);

            actual.Should().Be(expected);
        }
    }
}