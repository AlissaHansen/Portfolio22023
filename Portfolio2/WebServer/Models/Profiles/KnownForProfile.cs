using AutoMapper;
using DataLayer;

namespace WebServer.Models.Profiles;

public class KnownForProfile : Profile
{
    public KnownForProfile()
    {
        CreateMap<KnownFor, KnownForModel>();
    }
}