namespace DataLayer;

public interface IDataService
{
    public (IList<MovieInfo> movieInfos, int count) GetMovieInfos(int page, int pageSize);
    public MovieInfo? GetMovieInfo(string Id);
    public (IList<Person> persons, int count) GetPersons(int page, int pageSize);
    public Person? GetPerson(string searchId);
}