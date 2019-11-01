using Microsoft.EntityFrameworkCore;
using Moq.AutoMock;
using Movies.Core.Repositories;
using Movies.Infrastructure.Data;
using Movies.Infrastructure.Repositories;
using System;

namespace Movies.Tests
{
    public abstract class BaseGiven : IDisposable
    {
        protected readonly AutoMocker Mocker;

        protected BaseGiven()
        {
            Mocker = new AutoMocker();
            Mocker.Use(GetMoviesDbContext());
            Mocker.Use<IMovieRepository>(Mocker.CreateInstance<MovieRepository>());
            Mocker.Use<IUserRepository>(Mocker.CreateInstance<UserRepository>());
            Mocker.Use<IMovieRatingRepository>(Mocker.CreateInstance<MovieRatingRepository>());
        }

        protected MoviesDbContext GetMoviesDbContext()
        {
            var optionsBuilder = new DbContextOptionsBuilder();

            optionsBuilder.UseInMemoryDatabase(GetType().FullName);

            var context = new MoviesDbContext(optionsBuilder.Options);

            context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;

            return context;
        }

        public void Dispose()
        {
            GetMoviesDbContext().Database.EnsureDeleted();
        }
    }
}
