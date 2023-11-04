using AutoMapper;
using DataLayer;
using Microsoft.AspNetCore.Mvc;
using WebServer.Models;

namespace WebServer.Controllers;

[Route("api/rate")]
[ApiController]

public class RateController : BaseController
{
    private readonly IDataService _dataService;
    private readonly IMapper _mapper;

    public RateController(IDataService dataService, LinkGenerator linkGenerator, IMapper mapper)
        : base(linkGenerator)
    {
        _dataService = dataService;
        _mapper = mapper;
    }

    [HttpPost]

    public IActionResult CreateRating(CreateRatingModel model)
    {
        var userRating = new UserRating
        {
            UserId = model.UserId,
            MovieInfoId = model.MovieId,
            Rating = model.Rating
        };
        if (_dataService.CreateRating(userRating))
        {
            return Ok(userRating);
        }

        return BadRequest();
    }

    private CreateRatingModel CreateRatingModel(UserRating userRating)
    {
        var model = _mapper.Map<CreateRatingModel>(userRating);
        return model;
    }
}