namespace WebServer.Models;

public class PersonListModel
{
    public string Url { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string BirthYear { get; set; } = string.Empty;
    public string DeathYear { get; set; } = string.Empty;
    public IList<ProfessionModel> Professions { get; set; }
}