using AutoMapper;
using DataLayer;
namespace WebServer.Models.Profiles;


public class MovieInfoProfile : Profile
{
    public MovieInfoProfile()
    {
        CreateMap<MovieInfo, MovieInfoModel>();
        CreateMap<MovieInfo, MovieInfoListModel>();
    }
}