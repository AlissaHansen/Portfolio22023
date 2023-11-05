using AutoMapper;
using DataLayer;
using Microsoft.AspNetCore.Mvc;
using WebServer.Models;

namespace WebServer.Controllers;

[Route("api/searchperson")]
[ApiController]

public class PersonSearchController : BaseController
{
    private readonly IDataService _dataService;
    private readonly IMapper _mapper;

    public PersonSearchController(IDataService dataService, LinkGenerator linkGenerator, IMapper mapper)
        : base(linkGenerator)
    {
        _dataService = dataService;
        _mapper = mapper;
    }

    [HttpGet]

    public IActionResult GetPersonSearchResults(string searchTerm, string username = "")
    {
        var movies = string.IsNullOrEmpty(username)
            ? _dataService.GetPersonsBySearchNoUser(searchTerm)
            : _dataService.GetPersonsBySearch(searchTerm, username);
        var items = movies.Select(CreatePersonSearchModel);
        return Ok(items);
    }
    
    private PersonSearchModel CreatePersonSearchModel(PersonSearchResult searchResult)
    {
        var model = _mapper.Map<PersonSearchModel>(searchResult);
        model.Url = Url.Action("GetPerson", "Persons", new { id = searchResult.PersonId});
        return model;
    }
}