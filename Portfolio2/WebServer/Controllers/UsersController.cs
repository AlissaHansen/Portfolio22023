using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using DataLayer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using WebServer.Models;
using WebServiceToken.Services;

namespace WebServer.Controllers;

[Route("api/users")]
[ApiController]

public class UsersController : BaseController
{
    private readonly IDataService _dataService;
    private readonly IMapper _mapper;
    private readonly Hashing _hashing;
    private readonly IConfiguration _configuration;

    public UsersController(IDataService dataService, LinkGenerator linkGenerator,
        IMapper mapper, Hashing hashing, IConfiguration configuration)
        : base(linkGenerator)
    {
        _dataService = dataService;
        _mapper = mapper;
        _hashing = hashing;
        _configuration = configuration;
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
        if (_dataService.GetUser(model.UserId) != null)
        {
            return BadRequest();
        }

        if (string.IsNullOrEmpty(model.Password))
        {
            return BadRequest();
        }

        (var hashedPwd, var salt) = _hashing.Hash(model.Password);

        var user = new User
        {
            UserId = model.UserId,
            Password = hashedPwd,
            Salt = salt,
            Role = model.Role
        };
        _dataService.CreateUser(user);
        return CreatedAtRoute(nameof(GetUser), new { userId = user.UserId }, user);
    }

    [HttpPost("login")]
    public IActionResult Login(UserLoginModel model)
    {
        var user = _dataService.GetUser(model.UserId);

        if (user == null)
        {
            return BadRequest();
        }

        if (!_hashing.Verify(model.Password, user.Password, user.Salt))
        {
            return BadRequest();
        }

        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, user.UserId),
            new Claim(ClaimTypes.Role, user.Role)
        };
        var secret = _configuration.GetSection("Auth:Secret").Value;
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));

        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

        var token = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.Now.AddDays(3),
            signingCredentials: creds
        );

        var jwt = new JwtSecurityTokenHandler().WriteToken(token);
        return Ok(new { user.UserId, token = jwt });
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
        (var hashedPwd, var salt) = _hashing.Hash(model.Password);
        var user = new User
        {
            UserId = model.UserId,
            Password = hashedPwd,
            Salt = salt
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
            model.PersonBookmarkModels = user.PersonBookmarks
                .Select(personbookmark =>
                {
                    var url = Url.Action("GetPerson", "Persons", new { id = personbookmark.PersonId });
                    return new PersonBookmarkModel
                    {
                        PersonId = personbookmark.PersonId,
                        Url = url != null 
                            ? url 
                            : "Not specified"
                    };
                })
                .ToList();
        }

        if (user.UserRatings.Any())
        {
            model.UserRatingModels = user.UserRatings
                .Select(userrating =>
                {
                    var url = Url.Action("GetMovieInfo", "MovieInfos", new { id = userrating.MovieInfoId });
                    return new UserRatingModel
                    {
                        MovieInfoId = userrating.MovieInfoId,
                        Rating = userrating.Rating,
                        Url = url != null 
                            ? url 
                            : "Not specified"
                    };
                })
                .ToList();
        }
        return model;
    }
}
