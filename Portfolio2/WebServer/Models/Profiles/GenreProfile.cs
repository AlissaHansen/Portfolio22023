using AutoMapper;
using DataLayer;
namespace WebServer.Models.Profiles;


public class GenreProfile : Profile
{
    public GenreProfile()
    {
        CreateMap<Genre, GenreModel>();
    }
}