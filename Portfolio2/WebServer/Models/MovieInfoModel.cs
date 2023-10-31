using DataLayer;

namespace WebServer.Models;

public class MovieInfoModel
{
    public string Type { get; set; } = string.Empty;
    public string PrimaryTitle { get; set; } = string.Empty;
    public string OriginalTitle { get; set;} = string.Empty;
    public string IsAdult { get; set; } = string.Empty;
    public string StartYear { get; set; } = string.Empty;
    public string EndYear { get; set; } = string.Empty;
    public int RunTime { get; set; }
    public string Poster { get; set; } = string.Empty;
    public string Plot { get; set; } = string.Empty;
    public float AverageRating { get; set; }
    public int NumVotes { get; set; }
    public IList<GenreModel> Genres { get; set; }
    
    

}