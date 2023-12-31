namespace WebServer.Models;

public class PersonModel
{
    public string Name { get; set; } = string.Empty;
    public string BirthYear { get; set; } = string.Empty;
    public string DeathYear { get; set; } = string.Empty;
    public IList<ProfessionModel> Professions { get; set; }
    public IList<KnownForModel> KnownForTitles { get; set; }
}