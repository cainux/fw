using Movies.Core.Services;
using System.Linq;

namespace Movies.Tests.Services.Given_a_MovieService
{
    public abstract class Given_a_MovieService : BaseGiven
    {
        protected readonly MoviesService SUT; 

        public Given_a_MovieService()
        {
            SUT = Mocker.CreateInstance<MoviesService>();
        }
    }
}
