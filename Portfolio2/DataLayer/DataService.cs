using Microsoft.EntityFrameworkCore;

namespace DataLayer;

public class DataService : IDataService
{
    public (IList<MovieInfo> movieInfos, int count) GetMovieInfos(int page, int pageSize)
    {
        var db = new MoviedbContext();
        var movieInfos = db.MovieInfos
            .Include(x=> x.Rating)
            .Skip(page * pageSize)
            .Take(pageSize)
            .ToList();
        return (movieInfos, db.MovieInfos.Count());
    }

    public MovieInfo? GetMovieInfo(string searchId)
    {
        var db = new MoviedbContext();

        foreach (var item in db.MovieInfos
                     .Include(r => r.Rating)
                     .Include(g => g.Genres)
                     .Where(x => x.Id.Equals(searchId)))
        {
            return item;
        }
        return null;
    }
    
    public (IList<Person> persons, int count) GetPersons(int page, int pageSize)
    {
        var db = new MoviedbContext();
        var persons = db.Persons
            .Skip(page * pageSize)
            .Take(pageSize)
            .ToList();
        return (persons, db.Persons.Count());
    }
    
    public Person? GetPerson(string searchId)
    {
        var db = new MoviedbContext();

        foreach (var item in db.Persons
                     .Where(x => x.Id.Equals(searchId)))
        {
            return item;
        }
        return null;
    }
}