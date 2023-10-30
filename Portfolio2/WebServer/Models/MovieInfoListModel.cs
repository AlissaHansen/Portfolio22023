using DataLayer;
namespace WebServer.Models;

public class MovieInfoListModel
{
    public string Url { get; set; } = string.Empty;
    public string Primarytitle { get; set; } = string.Empty;
    public string Poster { get; set; } = string.Empty;
    public float AverageRating { get; set; }
    public int NumVotes { get; set; }
}