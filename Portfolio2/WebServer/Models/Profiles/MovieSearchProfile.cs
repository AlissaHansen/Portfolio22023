using AutoMapper;
using DataLayer;

namespace WebServer.Models.Profiles;

public class MovieSearchProfile :Profile
{
    public MovieSearchProfile()
    {
        CreateMap<MovieSearchResult, MovieSearchModel>();
    }
}