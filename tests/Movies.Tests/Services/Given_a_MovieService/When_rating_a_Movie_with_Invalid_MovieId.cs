﻿using FluentAssertions;
using Movies.Core.Entities;
using Movies.Core.Exceptions;
using System;
using Xunit;

namespace Movies.Tests.Services.Given_a_MovieService
{
    public class When_rating_a_Movie_with_Invalid_MovieId : Given_a_MovieService
    {
        private readonly Exception actual;

        public When_rating_a_Movie_with_Invalid_MovieId()
        {
            // Arrange
            var dbc = GetMoviesDbContext();

            dbc.Users.Add(new User { UserId = 1, Username = "User_01" });
            dbc.Movies.Add(new Movie { Id = 1, Title = "Movie_01" });

            dbc.SaveChanges();

            // Act
            actual = Record.Exception(SUT.RateMovieAsync(0, 1, 5).Wait);
        }

        [Fact]
        public void Then_MovieNotFoundException_should_be_thrown()
        {
            actual.InnerException.Should().BeOfType<MovieNotFoundException>();
        }
    }
}
