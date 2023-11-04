namespace WebServer.Models;

public class MoviePrincipalModel
{
    public string Url { get; set; }
    public string Category { get; set; }
    public int Ordering { get; set; }

    public string? PersonName { get; set; }
}