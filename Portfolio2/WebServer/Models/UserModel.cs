namespace WebServer.Models;

public class UserModel
{
    public string UserId { get; set; }
    public DateTime CreationTime { get; set; }
    public IList<MovieBookmarkModel> MovieBookmarkModels { get; set; }
}