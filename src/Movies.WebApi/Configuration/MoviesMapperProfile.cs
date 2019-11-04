using AutoMapper;
using Movies.Core.Entities;
using Movies.WebApi.ViewModels;

namespace Movies.WebApi.Configuration
{
    public class MoviesMapperProfile : Profile
    {
        public MoviesMapperProfile()
        {
            CreateMap<Movie, MovieViewModel>();
        }
    }
}
