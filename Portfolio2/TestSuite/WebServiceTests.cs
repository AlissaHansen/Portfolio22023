using System.Net;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace TestSuite;

public class WebServiceTests
{
    private const string MoviesApi = "http://localhost:5001/api/movieinfos";

    [Fact]
    public async Task ApiMovies_GetMoviesNoArgument_OkAndAllMovies()
    {
        var (data, statusCode) = await GetObject(MoviesApi);
        Assert.Equal(HttpStatusCode.OK, statusCode);
    }


    // Helpers

    async Task<(JsonArray?, HttpStatusCode)> GetArray(string url)
    {
        var client = new HttpClient();
        var response = client.GetAsync(url).Result;
        var data = await response.Content.ReadAsStringAsync();
        return (JsonSerializer.Deserialize<JsonArray>(data), response.StatusCode);
    }

    async Task<(JsonObject?, HttpStatusCode)> GetObject(string url)
    {
        var client = new HttpClient();
        var response = client.GetAsync(url).Result;
        var data = await response.Content.ReadAsStringAsync();
        return (JsonSerializer.Deserialize<JsonObject>(data), response.StatusCode);
    }

}