using AutoMapper;
using DataLayer;
using Microsoft.AspNetCore.Mvc;
using WebServer.Models;

namespace WebServer.Controllers;

[Route("api/searchmovie")]
[ApiController]

public class MovieSearchController : BaseController
{
    private readonly IDataService _dataService;
    private readonly IMapper _mapper;

    public MovieSearchController(IDataService dataService, LinkGenerator linkGenerator, IMapper mapper)
        : base(linkGenerator)
    {
        _dataService = dataService;
        _mapper = mapper;
    }

    [HttpGet]

    public IActionResult GetMovieSearchResults(string searchTerm, string username = "")
    {
        var movies = string.IsNullOrEmpty(username)
            ? _dataService.GetMoviesBySearchNoUser(searchTerm)
            : _dataService.GetMoviesBySearch(searchTerm, username);
        var items = movies.Select(CreateMovieSearchModel);
        return Ok(items);
    }
    
    private MovieSearchModel CreateMovieSearchModel(MovieSearchResult searchResult)
    {
        var model = _mapper.Map<MovieSearchModel>(searchResult);
        model.Url = Url.Action("GetMovieInfo", "MovieInfos", new { id = searchResult.MovieInfoId });
        return model;
    }
}