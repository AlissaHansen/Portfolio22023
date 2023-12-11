namespace DataLayer;

public interface IDataService
{
    // Movie Methods
    public (IList<MovieInfo> movieInfos, int count) GetMovieInfos(int page, int pageSize);
    public (IList<MovieInfo> movieInfos, int count) GetRankedMovieInfos(int page, int pageSize);
    public (IList<MovieInfo> movieInfos, int count) GetMovieInfosByRelease(int page, int pageSize);
    public MovieInfo? GetMovieInfo(string Id);

    //Person Methods
    public (IList<Person> persons, int count) GetPersons(int page, int pageSize);
    public (IList<Person> persons, int count) GetRankedPersons(int page, int pageSize);
    public Person? GetPerson(string searchId);

    //User methods
    public (IList<User> users, int count) GetUsers(int page, int pageSize);
    public User? GetUser(string username);
    public bool CreateUser(User userToCreate);
    public bool DeleteUser(string userId);
    public bool UpdateUser(User userToUpdate);

    //Bookmark methods
    public bool CreateMovieBookmark(MovieBookmark movieBookmark);
    public bool DeleteMovieBookmark(MovieBookmark movieBookmark);
    public bool CreatePesonBookmark(PersonBookmark personBookmark);
    public bool DeletePersonBookmark(PersonBookmark personBookmark);

    // Rating methods
    public bool CreateRating(UserRating userRating);
    public bool DeleteRating(string username, string movieId);

    //Search Methods
    public IList<MovieSearchResult> GetMoviesBySearch(string username, string searchTerm);
    public IList<MovieSearchResult> GetMoviesBySearchNoUser(string searchTerm);
    public IList<PersonSearchResult> GetPersonsBySearch(string searchTerm, string username);
    public IList<PersonSearchResult> GetPersonsBySearchNoUser(string searchTerm);

}