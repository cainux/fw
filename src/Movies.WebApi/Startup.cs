using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Movies.Core.Repositories;
using Movies.Core.Services;
using Movies.Infrastructure.Data;
using Movies.Infrastructure.Repositories;

namespace Movies.WebApi
{
    public class Startup
    {
        protected virtual void DbContextOptionsBuilder(DbContextOptionsBuilder options)
            => options.UseInMemoryDatabase(GetType().FullName);
        

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddDbContext<MoviesDbContext>(DbContextOptionsBuilder);
            services.AddTransient<IMoviesService, MoviesService>();
            services.AddTransient<IMovieRepository, MovieRepository>();
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IMovieRatingRepository, MovieRatingRepository>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
