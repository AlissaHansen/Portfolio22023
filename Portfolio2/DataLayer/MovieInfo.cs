namespace DataLayer;

public class MovieInfo
{
    public string Id { get; set; }
    public string Type { get; set; }
    public string PrimaryTitle { get; set; }
    public string? OriginalTitle { get; set; }
    public bool? IsAdult { get; set; }
    public string? StartYear { get; set; }
    public string? EndYear { get; set; }
    public int? RunTime { get; set; }
    public string? Poster { get; set; }
    public string? Plot { get; set; }
    public ICollection<Genre> Genres { get; set; }
    
}