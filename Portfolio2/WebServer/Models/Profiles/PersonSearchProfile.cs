using AutoMapper;
using DataLayer;

namespace WebServer.Models.Profiles;

public class PersonSearchProfile : Profile
{
    public PersonSearchProfile()
    {
        CreateMap<PersonSearchResult, PersonSearchModel>();
    }
}