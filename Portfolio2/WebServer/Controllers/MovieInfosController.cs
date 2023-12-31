using AutoMapper;
using DataLayer;
using Microsoft.AspNetCore.Mvc;
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

    public IActionResult GetMovieInfos(int page = 0, int pageSize = 10, string ranked = "false", string newest = "false")
    {

        var (movieInfos, total) = ranked.Equals("true")
            ? newest.Equals("true")
                ? _dataService.GetMovieInfos(page, pageSize)
                : _dataService.GetRankedMovieInfos(page, pageSize)
            : newest.Equals("true")
                ? _dataService.GetMovieInfosByRelease(page, pageSize)
                : _dataService.GetMovieInfos(page, pageSize);

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
        model.Id = movieInfo.Id;
        model.AverageRating = movieInfo.Rating.AverageRating;
        model.NumVotes = movieInfo.Rating.NumVotes;
        return model;

    }

    private MovieInfoModel CreateMovieInfoModel(MovieInfo movieInfo)
    {
        var model = _mapper.Map<MovieInfoModel>(movieInfo);
        model.AverageRating = movieInfo.Rating.AverageRating;
        model.NumVotes = movieInfo.Rating.NumVotes;
        
        // Extract GenreName values from the list of genres
        if (movieInfo.Genres != null && movieInfo.Genres.Any())
        {
            model.Genres = movieInfo.Genres.Select(genre => new GenreModel
            {
                GenreName = genre.GenreName
            }).ToList();
        }

        if (movieInfo.MoviePrincipals.Any())
        {
            model.MoviePrincipals = movieInfo.MoviePrincipals
                .Select(movieprincipals =>
                {
                    var url = Url.Action("GetPerson", "Persons", new { id = movieprincipals.PersonId });
                    return new MoviePrincipalModel
                    {
                        Category = movieprincipals.Category,
                        Ordering = movieprincipals.Ordering,
                        PersonName = movieprincipals.Person.Name,
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