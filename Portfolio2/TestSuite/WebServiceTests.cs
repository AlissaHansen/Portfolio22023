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


    // Helpers taget fra Henriks test

    async Task<(JsonObject?, HttpStatusCode)> GetObject(string url)
    {
        var client = new HttpClient();
        var response = client.GetAsync(url).Result;
        var data = await response.Content.ReadAsStringAsync();
        return (JsonSerializer.Deserialize<JsonObject>(data), response.StatusCode);
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