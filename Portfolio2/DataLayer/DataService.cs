using Microsoft.EntityFrameworkCore;

namespace DataLayer;

public class DataService : IDataService
{
    public (IList<MovieInfo> movieInfos, int count) GetMovieInfos(int page, int pageSize)
    {
        var db = new MoviedbContext();
        var movieInfos = db.MovieInfos
            .Include(x => x.Genres)
            .Skip(page * pageSize)
            .Take(pageSize)
            .ToList();
        return (movieInfos, db.MovieInfos.Count());
    }

    public MovieInfo? GetMovieInfo(string searchId)
    {
        var db = new MoviedbContext();

        foreach (var item in db.MovieInfos
                     .Include(g => g.Genres)
                     .Where(x => x.Id.Equals(searchId)))
        {
            return item;
        }
        return null;
    }
}