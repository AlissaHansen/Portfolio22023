namespace DataLayer;

public interface IDataService
{
    public (IList<MovieInfo> movieInfos, int count) GetMovieInfos(int page, int pageSize);
    public (IList<MovieInfo> movieInfos, int count) GetRankedMovieInfos(int page, int pageSize);
    public MovieInfo? GetMovieInfo(string Id);
    public (IList<Person> persons, int count) GetPersons(int page, int pageSize);
    public Person? GetPerson(string searchId);
    public (IList<User> users, int count) GetUsers(int page, int pageSize);
    public User? GetUser(string username);
    public bool CreateUser(User userToCreate);
    public bool DeleteUser(string userId);
    public bool UpdateUser(User userToUpdate);


}