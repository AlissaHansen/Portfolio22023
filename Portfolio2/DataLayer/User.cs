namespace DataLayer;

public class User
{
    public string UserId { get; set; }
    public string Password { get; set; }
    public DateTime CreationTime { get; set; }
    
    public IList<MovieBookmark> MovieBookmarks { get; set; }
    public IList<PersonBookmark> PersonBookmarks { get; set; }
    public IList<UserRating> UserRatings { get; set; }
}