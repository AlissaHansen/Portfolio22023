using AutoMapper;
using DataLayer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace WebServer.Controllers;

[Route("api/personbookmarks")]
[ApiController]
[Authorize]

public class PersonBookmarksController : BaseController
{
    private readonly IDataService _dataService;
    private readonly IMapper _mapper;

    public PersonBookmarksController(IDataService dataService, LinkGenerator linkGenerator, IMapper mapper)
        : base(linkGenerator)
    {
        _dataService = dataService;
        _mapper = mapper;
    }

    [HttpPost]

    public IActionResult CreatePersonBookmark(PersonBookmark personBookmark)
    {
        try
        {
            if (!User.Identity.IsAuthenticated)
            {
                return Unauthorized(personBookmark);
            }
            var bookmark = new PersonBookmark
            {
                PersonId = personBookmark.PersonId,
                UserId = personBookmark.UserId
            };
            if (_dataService.CreatePesonBookmark(bookmark))
            {
                return Created("success", bookmark);
            }

            return BadRequest(personBookmark);
        }
        catch
        {
            return BadRequest(personBookmark);
        }
    }

    [HttpDelete]

    public IActionResult DeletePersonBookmark(PersonBookmark personBookmark)
    {
        try
        {
            if (!User.Identity.IsAuthenticated)
            {
                return Unauthorized(personBookmark);
            }
            if (_dataService.DeletePersonBookmark(personBookmark))
            {
                return Ok("deleted");
            }

            return BadRequest(personBookmark);
        }
        catch
        {
            return BadRequest(personBookmark);
        }
    }
}