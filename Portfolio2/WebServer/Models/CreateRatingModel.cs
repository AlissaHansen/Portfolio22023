namespace WebServer.Models;

public class CreateRatingModel
{
    public string UserId { get; set; }
    public string MovieId { get; set; }
    public int Rating { get; set; }
}