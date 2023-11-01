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

    [HttpGet("{id}", Name = nameof(GetUser))]
    public IActionResult GetUser(string id)
    {
        var user = _dataService.GetUser(id);
        if (user == null)
        {
            return NotFound();
        }

        return Ok(CreateUserModel(user));
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
        return model;
    }
}
