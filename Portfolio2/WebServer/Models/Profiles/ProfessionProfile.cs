using AutoMapper;
using DataLayer;

namespace WebServer.Models.Profiles;

public class ProfessionProfile : Profile
{
    public ProfessionProfile()
    {
        CreateMap<Profession, ProfessionModel>();
    }
}