using Microsoft.Extensions.DependencyInjection;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace TestSuite;

public class WebServiceTests
{
    private const string MoviesApi = "http://localhost:5001/api/movieinfos";
    private const string UsersApi = "http://localhost:5001/api/users";

    /*  Api movieinfos  */

    [Fact]
    public async Task ApiMovies_GetMoviesNoArgument_OkAndAllMovies()
    {
        var (data, statusCode) = await GetObject(MoviesApi);
        Assert.Equal(HttpStatusCode.OK, statusCode);
    }

    [Fact]
    public async Task ApiMovies_GetMovieWithValidTconst_OkAndMovie()
    {
        var (movie, statusCode) = await GetObject($"{MoviesApi}/tt0499549");
        Assert.Equal(HttpStatusCode.OK, statusCode);
        Assert.Equal("Avatar", movie?.Value("primaryTitle"));
    }

    [Fact]
    public async Task ApiMovie_GetMovieWithInvalidTconst_NotFound()
    {
        var (_, statusCode) = await GetObject($"{MoviesApi}/notATconst");
        Assert.Equal(HttpStatusCode.NotFound, statusCode);
    }

    /*  User tests  */

    [Fact]
    public async Task ApiUser_CreateUserValid_Created()
    {
        var newUser = new
        {
            UserId = "UserTest",
            Password = "password"
        };
        var (_, statusCode) = await PostData(UsersApi, newUser);

        Assert.Equal(HttpStatusCode.Created, statusCode);
        await DeleteData($"{UsersApi}?userid={newUser.UserId}");
    }

    [Fact]
    public async Task ApiUser_DeleteUser_Ok()
    {
        var newUser = new
        {
            UserId = "UserTest",
            Password = "password"
        };
        await PostData(UsersApi, newUser);
        var statusCode = await DeleteData($"{UsersApi}?userid={newUser.UserId}");
        
        Assert.Equal(HttpStatusCode.OK, statusCode);
    }
    

    // Helpers taget fra Henriks test

    async Task<(JsonObject?, HttpStatusCode)> GetObject(string url)
    {
        var client = new HttpClient();
        var response = client.GetAsync(url).Result;
        var data = await response.Content.ReadAsStringAsync();
        return (JsonSerializer.Deserialize<JsonObject>(data), response.StatusCode);
    }
    async Task<(JsonObject?, HttpStatusCode)> PostData(string url, object content)
    {
        var client = new HttpClient();
        var requestContent = new StringContent(
            JsonSerializer.Serialize(content),
            Encoding.UTF8,
            "application/json");
        var response = await client.PostAsync(url, requestContent);
        var data = await response.Content.ReadAsStringAsync();
        return (JsonSerializer.Deserialize<JsonObject>(data), response.StatusCode);
    }
    async Task<HttpStatusCode> DeleteData(string url)
    {
        var client = new HttpClient();
        var response = await client.DeleteAsync(url);
        return response.StatusCode;
    }

}

static class HelperExt
{
    public static string? Value(this JsonNode node, string name)
    {
        var value = node[name];
        return value?.ToString();
    }
}