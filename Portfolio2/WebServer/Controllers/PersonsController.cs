using AutoMapper;
using DataLayer;
using Microsoft.AspNetCore.Mvc;
using WebServer.Models;

namespace WebServer.Controllers;

[Route("api/persons")]
[ApiController]

public class PersonsController : BaseController
{
    private readonly IDataService _dataService;
    private readonly IMapper _mapper;

    public PersonsController(IDataService dataService, LinkGenerator linkGenerator, IMapper mapper)
        : base(linkGenerator)
    {
        _dataService = dataService;
        _mapper = mapper;
    }

    [HttpGet(Name = nameof(GetPersons))]

    public IActionResult GetPersons(int page = 0, int pageSize = 10)
    {
        (var persons, var total) = _dataService.GetPersons(page, pageSize);

        var items = persons.Select(CreatePersonListModel);

        var result = Paging(items, total, page, pageSize, nameof(GetPersons));
        return Ok(result);
    }

    [HttpGet("{id}", Name = nameof(GetPerson))]

    public IActionResult GetPerson(string id)
    {
        var person = _dataService.GetPerson(id);
        if (person == null)
        {
            return NotFound();
        }

        return Ok(CreatePersonModel(person));
    }

    private PersonListModel CreatePersonListModel (Person person)
    {
        var model = _mapper.Map<PersonListModel>(person);
        model.Url = GetUrl(nameof(GetPerson), new { person.Id });
        return model;

    }

    private PersonModel CreatePersonModel(Person person)
    {
        var model = _mapper.Map<PersonModel>(person);
        return model;
    }
}