using DataLayer;
namespace WebServer.Models;

public class MovieInfoListModel
{
    public string Url { get; set; } = string.Empty;
    public string Id { get; set; }
    public string Primarytitle { get; set; } = string.Empty;
    public string Poster { get; set; } = string.Empty;
    public string StartYear { get; set; } = string.Empty;
    public int RunTime { get; set; }
    public float AverageRating { get; set; }
    public int NumVotes { get; set; }
}