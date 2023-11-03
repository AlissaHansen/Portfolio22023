using AutoMapper;
using DataLayer;

namespace WebServer.Models.Profiles;

public class MovieBookmarkProfile : Profile
{
    public MovieBookmarkProfile()
    {
        CreateMap<MovieBookmark, MovieBookmarkModel>();
    }
}