using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Movies.Core.Entities;
using Movies.Infrastructure.Data;
using System.Linq;

namespace Movies.WebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            InitialiseDatabase(host);

            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });

        private static void InitialiseDatabase(IHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;

                var dbc = services.GetRequiredService<MoviesDbContext>();
                dbc.Database.EnsureCreated();

                // Database already has data - get out!
                if (dbc.Movies.Any())
                    return;

                dbc.Movies.AddRange(new[]
                {
                    // Grabbed these from a CSV I found online
                    new Movie { Title = "Mindhorn", Genre = "Comedy", YearOfRelease = 2016, RunningTime = 89 },
                    new Movie { Title = "Manchester by the Sea", Genre = "Drama", YearOfRelease = 2016, RunningTime = 137 },
                    new Movie { Title = "Bad Moms", Genre = "Comedy", YearOfRelease = 2016, RunningTime = 100 },
                    new Movie { Title = "Why Him", Genre = "Comedy", YearOfRelease = 2016, RunningTime = 111 },
                    new Movie { Title = "Moonlight", Genre = "Drama", YearOfRelease = 2016, RunningTime = 111 },
                    new Movie { Title = "Lowriders", Genre = "Drama", YearOfRelease = 2016, RunningTime = 99 },
                    new Movie { Title = "The Last Face", Genre = "Drama", YearOfRelease = 2016, RunningTime = 130 },
                    new Movie { Title = "Wakefield", Genre = "Drama", YearOfRelease = 2016, RunningTime = 106 },
                    new Movie { Title = "The Help", Genre = "Drama", YearOfRelease = 2011, RunningTime = 146 },
                    new Movie { Title = "The Comedian", Genre = "Comedy", YearOfRelease = 2016, RunningTime = 120 },
                    new Movie { Title = "All We Had", Genre = "Drama", YearOfRelease = 2016, RunningTime = 105 },
                    new Movie { Title = "Office Christmas Party", Genre = "Comedy", YearOfRelease = 2016, RunningTime = 105 },
                    new Movie { Title = "Boyka: Undisputed IV", Genre = "Action", YearOfRelease = 2016, RunningTime = 86 },
                    new Movie { Title = "Fences", Genre = "Drama", YearOfRelease = 2016, RunningTime = 139 },
                    new Movie { Title = "Room", Genre = "Drama", YearOfRelease = 2015, RunningTime = 118 },
                    new Movie { Title = "Superbad", Genre = "Comedy", YearOfRelease = 2007, RunningTime = 113 }
                });

                dbc.Users.AddRange(new[]
                {
                    new User { Username = "Cain" },
                    new User { Username = "Yin" },
                    new User { Username = "Tilly" },
                    new User { Username = "Ally" },
                });

                dbc.SaveChanges();
            }
        }
    }
}
