namespace DataLayer;

public class DataService
{
    public IList<MovieInfo> GetMovieInfos()
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