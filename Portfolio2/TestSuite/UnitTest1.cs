using DataLayer;
using Xunit;

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
        Assert.NotNull(movies.movieInfos.First().PrimaryTitle);
    }

    [Fact]
    public void GetMovie_WithInvalidId_ReturnsNoMovie()
    {
        var service = new DataService();
        var movie = service.GetMovieInfo("notARealTconst");
        Assert.Null(movie);
    }

    [Fact]
    public void GetMovie_WithValidId_ReturnsMovie()
    {
        var service = new DataService();
        var movie = service.GetMovieInfo("tt0137204");
        Assert.Equal("Joe Finds Grace", movie.PrimaryTitle);
    }

    /* Search Test */
    [Fact]
    public void GetMoviesBySearch_ValidSearch_RetunsMatchingMovies()
    {
        var service = new DataService();
        var movies = service.GetMoviesBySearchNoUser("Avatar");
        Assert.NotNull(movies);
        Assert.Equal("Avatar", movies.First().Title);
    }

    /* Users tests */

    [Fact]
    public void GetUser_WithValidUserId_RetunsUser()
    {
        var service = new DataService();
        var user = service.GetUser("Alissa");
        Assert.NotNull(user);
    }

    [Fact]
    public void CreateUser_WithValidData_MakesUser()
    {
        var service = new DataService();
        var user = new User
        {
            UserId = "Test",
            Password = "mypswd",
            Salt = "peber",
            Role = "User"
        };
        var result = (service.CreateUser(user));
        Assert.True(result);

        // Cleanup
        service.DeleteUser("Test"); 
    }

    [Fact]
    public void DeleteUser_WithValidUserId_DeletesUser()
    {
        var service = new DataService();
        var user = new User
        {
            UserId = "Test",
            Password = "mypswd",
            Salt = "peber",
            Role = "User"
        };
        service.CreateUser(user);
        var result = service.DeleteUser("Test");
        Assert.True(result);
    }

    [Fact]
    public void DeleteUser_WithInvalidId_ReturnsFalse()
    {
        var service = new DataService();
        var result = service.DeleteUser("");
        Assert.False(result);
    }

    [Fact]
    public void UpdateUser_NewUserPassword_UpdateUserPassword ()
    {
        var service = new DataService();
        var newUser = new User
        {
            UserId = "UpdateTest",
            Password = "passwordToUpdate",
            Salt = "someSalt",
            Role = "User"
        };
        service.CreateUser(newUser);

        var userUpdate = new User
        {
            UserId = "UpdateTest",
            Password = "updatedPassword",
            Salt = "someSalt",
            Role = "User"
        };
        var result = service.UpdateUser(userUpdate);
        Assert.True(result);

        var user = service.GetUser("UpdateTest");
        Assert.Equal("updatedPassword", user.Password);

        //cleanup
        service.DeleteUser("UpdateTest");
    }

}