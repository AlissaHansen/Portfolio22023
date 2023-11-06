using DataLayer;

namespace TestSuite;

public class UnitTest1
{
    /* MovieInfos test */
    [Fact]
    public void MovieInfo_Object_HasAllAttributes()
    {
        var movieInfo = new MovieInfo();
        Assert.Null(movieInfo.Id);
        Assert.Null(movieInfo.Type);
        Assert.Null(movieInfo.PrimaryTitle);
        Assert.Null(movieInfo.OriginalTitle);
        Assert.Null(movieInfo.IsAdult);
        Assert.Null(movieInfo.StartYear);
    }

    [Fact]
    public void GetAllMovieInfos_WithPageAndPageSize_ReturnsMovies()
    {
        var service = new DataService();
        var movies = service.GetMovieInfos(0, 20);
        Assert.Equal(55277,movies.count);
        
    }
}