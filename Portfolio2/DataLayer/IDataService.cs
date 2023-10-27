namespace DataLayer;

public interface IDataService
{
    public IList<MovieInfo> GetMovieInfos(int page, int pageSize);
    public MovieInfo? GetMovieInfo(string Id);
}