using AutoMapper;
using DataLayer;
using Microsoft.AspNetCore.Mvc;
using WebServer.Models;

namespace WebServer.Controllers;

[Route("api/users")]
[ApiController]

public class UsersController : BaseController
{
    private readonly IDataService _dataService;
    private readonly IMapper _mapper;

    public UsersController(IDataService dataService, LinkGenerator linkGenerator, IMapper mapper)
        : base(linkGenerator)
    {
        _dataService = dataService;
        _mapper = mapper;
    }

    [HttpGet(Name = nameof(GetUsers))]
    public IActionResult GetUsers(int page = 0, int pageSize = 10)
    {
        (var users, var total) = _dataService.GetUsers(page, pageSize);

        var items = users.Select(CreateUserListModel);

        var result = Paging(items, total, page, pageSize, nameof(GetUsers));
        return Ok(result);

    }

    [HttpGet("{userId}", Name = nameof(GetUser))]
    public IActionResult GetUser(string userId)
    {
        var user = _dataService.GetUser(userId);
        if (user == null)
        {
            return NotFound();
        }

        return Ok(CreateUserModel(user));
    }

    [HttpPost]
    public IActionResult CreateUser(CreateUserModel model)
    {
        var user = new User
        {
            UserId = model.UserId,
            Password = model.Password
        };
        if (_dataService.CreateUser(user))
        {
            return Created("success", user);
        }

        return BadRequest();
    }

    [HttpDelete]
    public IActionResult DeleteUser(string userId)
    {
        if (_dataService.DeleteUser(userId))
        {
            return Ok("Deleted");
        }

        return BadRequest();
    }

    [HttpPut]
    public IActionResult UpdateUser(CreateUserModel model)
    {
        var user = new User
        {
            UserId = model.UserId,
            Password = model.Password
        };
        if (_dataService.UpdateUser(user))
        {
            return Ok(user);
        }

        return BadRequest();
    }

    
    private UserListModel CreateUserListModel(User user)
    {
        var model = _mapper.Map<UserListModel>(user);
        model.Url = GetUrl(nameof(GetUser), new { user.UserId });
        return model;
    }

    private UserModel CreateUserModel(User user)
    {
        var model = _mapper.Map<UserModel>(user);
        if (user.MovieBookmarks.Any())
        {
            model.MovieBookmarkModels = user.MovieBookmarks
                .Select(moviebookmark =>
                {
                    var url = Url.Action("GetMovieInfo", "MovieInfos", new { id = moviebookmark.MovieInfoId });
                    return new MovieBookmarkModel
                    {
                        MovieInfoId = moviebookmark.MovieInfoId,
                        Url = url != null 
                            ? url 
                            : "Not specified"
                    };
                })
                .ToList();
        }

        if (user.PersonBookmarks.Any())
        {
            model.PersonBookmarkModels = user.PersonBookmarks.Select(personbookmark => new PersonBookmarkModel
            {
                PersonId = personbookmark.PersonId
            }).ToList();
        }

        if (user.UserRatings.Any())
        {
            model.UserRatingModels = user.UserRatings.Select(userrating => new UserRatingModel
            {
                MovieInfoId = userrating.MovieInfoId,
                Rating = userrating.Rating
            }).ToList();
        }
        return model;
    }
}
