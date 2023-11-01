using AutoMapper;
using DataLayer;

namespace WebServer.Models.Profiles;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<User, UserModel>();
        CreateMap<User, UserListModel>();
    }
}