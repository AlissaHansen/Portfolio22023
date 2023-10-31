using AutoMapper;
using DataLayer;
namespace WebServer.Models.Profiles;


public class PersonProfile : Profile
{
    public PersonProfile()
    {
        CreateMap<Person, PersonModel>();
        CreateMap<Person, PersonListModel>();
    }
}