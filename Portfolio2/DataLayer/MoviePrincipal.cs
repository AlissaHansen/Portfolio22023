namespace DataLayer;

public class MoviePrincipal
{
    public string MovieInfoId { get; set; }
    public string PersonId { get; set; }
    public Person? Person { get; set; }
    public string? Category { get; set; }
    public int Ordering { get; set; }
    
    
}