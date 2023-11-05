using AutoMapper;
using DataLayer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

    public IActionResult GetMovieSearchResults(SearchModel searchModel)
    {
        var movies = _dataService.GetMoviesBySearch(searchModel.Username, searchModel.SearchTerm);
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