using AutoMapper;
using DataLayer;

namespace WebServer.Models.Profiles;

public class UserRatingProfile : Profile
{
    public UserRatingProfile()
    {
        CreateMap<UserRating, UserListModel>();
    }
}
