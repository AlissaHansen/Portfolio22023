namespace DataLayer;

public class DataService : IDataService
{
    public IList<MovieInfo> GetMovieInfos(int page, int pageSize)
    {
        var db = new MoviedbContext();
        return db.MovieInfos.ToList();
    }

    public MovieInfo? GetMovieInfo(string Id)
    {
        var db = new MoviedbContext();
        return db.MovieInfos.FirstOrDefault(x => x.Id.Equals(Id));
    }
}