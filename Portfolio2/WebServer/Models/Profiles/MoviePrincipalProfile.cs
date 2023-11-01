using AutoMapper;
using DataLayer;

namespace WebServer.Models.Profiles;

public class MoviePrincipalProfile : Profile
{
    public MoviePrincipalProfile()
    {
        CreateMap<MoviePrincipal, MoviePrincipalModel>();
    }
}