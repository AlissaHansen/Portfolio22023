using AutoMapper;
using DataLayer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace WebServer.Controllers;

[Route("api/moviebookmarks")]
[ApiController]
[Authorize]

public class MovieBookmarksController : BaseController
{
    private readonly IDataService _dataService;
    private readonly IMapper _mapper;

    public MovieBookmarksController(IDataService dataService, LinkGenerator linkGenerator, IMapper mapper)
        : base(linkGenerator)
    {
        _dataService = dataService;
        _mapper = mapper;
    }

    [HttpPost]

    public IActionResult CreateMovieBookmark(MovieBookmark movieBookmark)
    {
        try
        {
            if (!User.Identity.IsAuthenticated)
            {
                return Unauthorized();
            }
            var bookmark = new MovieBookmark
            {
                MovieInfoId = movieBookmark.MovieInfoId,
                UserId = movieBookmark.UserId
            };
            if (_dataService.CreateMovieBookmark(bookmark))
            {
                return Created("success", bookmark);
            }

            return BadRequest(movieBookmark);
        }
        catch
        {
            return BadRequest(movieBookmark);
        }
    }

    [HttpDelete]

    public IActionResult DeleteMovieBookmark(MovieBookmark movieBookmark)
    {
        try
        {
            if (!User.Identity.IsAuthenticated)
            {
                return Unauthorized();
            }
            if (_dataService.DeleteMovieBookmark(movieBookmark))
            {
                return Ok("deleted");
            }

            return BadRequest();
        }
        catch
        {
            return BadRequest();
        }
    }
}