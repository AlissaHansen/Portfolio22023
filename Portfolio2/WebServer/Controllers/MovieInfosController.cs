using AutoMapper;
using DataLayer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using WebServer.Models;

namespace WebServer.Controllers;

[Route("api/movieinfos")]
[ApiController]

public class MovieInfosController : BaseController
{
    private readonly IDataService _dataService;
    private readonly IMapper _mapper;

    public MovieInfosController(IDataService dataService, LinkGenerator linkGenerator, IMapper mapper)
        : base(linkGenerator)
    {
        _dataService = dataService;
        _mapper = mapper;
    }

    [HttpGet(Name = nameof(GetMovieInfos))]

    public IActionResult GetMovieInfos(int page = 0, int pageSize = 10)
    {
        (var movieInfos, var total) = _dataService.GetMovieInfos(page, pageSize);

        var items = movieInfos.Select(CreateMovieInfoListModel);

        var result = Paging(items, total, page, pageSize, nameof(GetMovieInfos));
        return Ok(result);
    }

    [HttpGet("{id}", Name = nameof(GetMovieInfo))]

    public IActionResult GetMovieInfo(string id)
    {
        var movie = _dataService.GetMovieInfo(id);
        if (movie == null)
        {
            return NotFound();
        }

        return Ok(CreateMovieInfoModel(movie));
    }

    private MovieInfoListModel CreateMovieInfoListModel(MovieInfo movieInfo)
    {
        var model = _mapper.Map<MovieInfoListModel>(movieInfo);
        model.Url = GetUrl(nameof(GetMovieInfo), new { movieInfo.Id }); 
        return model;

    }

    private MovieInfoModel CreateMovieInfoModel(MovieInfo movieInfo)
    {
        var model = _mapper.Map<MovieInfoModel>(movieInfo);
        model.Url = GetUrl(nameof(GetMovieInfo), new { movieInfo.Id });
        return model;
    }
}