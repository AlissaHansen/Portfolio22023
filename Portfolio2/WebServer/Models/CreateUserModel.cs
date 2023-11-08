namespace WebServer.Models;

public class CreateUserModel
{
    public string UserId { get; set; }
    public string Password { get; set; }
    public string Salt { get; set; } = string.Empty;
    public string Role { get; set; } = "User";

}