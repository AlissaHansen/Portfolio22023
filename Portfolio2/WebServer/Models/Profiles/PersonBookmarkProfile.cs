using AutoMapper;
using DataLayer;

namespace WebServer.Models.Profiles;

public class PersonBookmarkProfile : Profile
{
    public PersonBookmarkProfile()
    {
        CreateMap<PersonBookmark, PersonBookmarkModel>();
    }
}