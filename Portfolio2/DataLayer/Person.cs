namespace DataLayer;

public class Person
{
    public string Id { get; set; }
    public string? Name { get; set; }
    public string? BirthYear { get; set; }
    public string? DeathYear { get; set; }
    public IList<Profession> Professions { get; set; }
    public IList<KnownFor> KnownFors { get; set; }
}