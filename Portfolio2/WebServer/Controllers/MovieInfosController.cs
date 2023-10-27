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

    private MovieInfoListModel CreateMovieInfoListModel(MovieInfo movieInfo)
    {
        var model = _mapper.Map<MovieInfoListModel>(movieInfo);
        model.Url = GetUrl(nameof(GetMovieInfos), new { movieInfo.Id });
        return model;

    }
}