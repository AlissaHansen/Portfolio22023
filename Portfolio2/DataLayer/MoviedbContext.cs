using Microsoft.EntityFrameworkCore;

namespace DataLayer;

public class MoviedbContext : DbContext
{
    public DbSet<MovieInfo> MovieInfos { get; set; }
    public DbSet<Genre> Genres { get; set; }
    public DbSet<Rating> Ratings { get; set; }
    public DbSet<Person> Persons { get; set; }
    public DbSet<Profession> Professions { get; set; }
    public DbSet<KnownFor> KnownFors { get; set; }
    public DbSet<MoviePrincipal> MoviePrincipals { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<PersonRating> PersonRatings { get; set; }
    public DbSet<MovieBookmark> MovieBookmarks { get; set; }
    public DbSet<PersonBookmark> PersonBookmarks { get; set; }
    public DbSet<UserRating> UserRatings{ get; set; }
    public DbSet<MovieSearchResult> MovieSearchResults { get; set; }
    public DbSet<PersonSearchResult> PersonSearchResults { get; set; }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.EnableSensitiveDataLogging();
        optionsBuilder.LogTo(Console.Out.WriteLine, Microsoft.Extensions.Logging.LogLevel.Information);
        optionsBuilder.UseNpgsql("host=localhost; db=moviedb; uid=postgres; pwd=chili321");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<MovieInfo>().ToTable("movie_info");
        modelBuilder.Entity<MovieInfo>().Property(x => x.Id).HasColumnName("tconst");
        modelBuilder.Entity<MovieInfo>().Property(x => x.Type).HasColumnName("titletype");
        modelBuilder.Entity<MovieInfo>().Property(x => x.PrimaryTitle).HasColumnName("primarytitle");
        modelBuilder.Entity<MovieInfo>().Property(x => x.OriginalTitle).HasColumnName("originaltitle");
        modelBuilder.Entity<MovieInfo>().Property(x => x.IsAdult).HasColumnName("isadult");
        modelBuilder.Entity<MovieInfo>().Property(x => x.StartYear).HasColumnName("startyear");
        modelBuilder.Entity<MovieInfo>().Property(x => x.EndYear).HasColumnName("endyear");
        modelBuilder.Entity<MovieInfo>().Property(x => x.RunTime).HasColumnName("runtimeminutes");
        modelBuilder.Entity<MovieInfo>().Property(x => x.Poster).HasColumnName("poster");
        modelBuilder.Entity<MovieInfo>().Property(x => x.Plot).HasColumnName("plot");
        
        modelBuilder.Entity<Genre>().ToTable("genre");
        modelBuilder.Entity<Genre>().Property(x => x.MovieInfoId).HasColumnName("tconst");
        modelBuilder.Entity<Genre>().Property(x => x.GenreName).HasColumnName("genre");
        modelBuilder.Entity<Genre>().HasKey(x => new { x.MovieInfoId, x.GenreName });
        
        modelBuilder.Entity<Rating>().ToTable("movie_ratings");
        modelBuilder.Entity<Rating>().Property(x => x.MovieInfoId).HasColumnName("tconst");
        modelBuilder.Entity<Rating>().Property(x => x.AverageRating).HasColumnName("averagerating");
        modelBuilder.Entity<Rating>().Property(x => x.NumVotes).HasColumnName("numvotes");
        modelBuilder.Entity<Rating>().HasKey(x => new { x.MovieInfoId});
        
        modelBuilder.Entity<Person>().ToTable("person");
        modelBuilder.Entity<Person>().Property(x => x.Id).HasColumnName("nconst");
        modelBuilder.Entity<Person>().Property(x => x.Name).HasColumnName("primaryname");
        modelBuilder.Entity<Person>().Property(x => x.BirthYear).HasColumnName("birthyear");
        modelBuilder.Entity<Person>().Property(x => x.DeathYear).HasColumnName("deathyear");
        
        modelBuilder.Entity<Profession>().ToTable("profession");
        modelBuilder.Entity<Profession>().Property(x => x.PersonId).HasColumnName("nconst");
        modelBuilder.Entity<Profession>().Property(x => x.ProfessionTitle).HasColumnName("profession");
        modelBuilder.Entity<Profession>().HasKey(x => new { x.PersonId, x.ProfessionTitle });

        modelBuilder.Entity<KnownFor>().ToTable("known_for");
        modelBuilder.Entity<KnownFor>().Property(x => x.PersonId).HasColumnName("nconst");
        modelBuilder.Entity<KnownFor>().Property(x => x.MovieInfoId).HasColumnName("knownfortitle");
        modelBuilder.Entity<KnownFor>().HasKey(x => new { x.PersonId, x.MovieInfoId });
        
        modelBuilder.Entity<MoviePrincipal>().ToTable("movie_principals");
        modelBuilder.Entity<MoviePrincipal>().Property(x => x.MovieInfoId).HasColumnName("tconst");
        modelBuilder.Entity<MoviePrincipal>().Property(x => x.PersonId).HasColumnName("nconst");
        modelBuilder.Entity<MoviePrincipal>().Property(x => x.Category).HasColumnName("category");
        modelBuilder.Entity<MoviePrincipal>().Property(x => x.Ordering).HasColumnName("ordering");
        modelBuilder.Entity<MoviePrincipal>().HasKey(x => new { x.MovieInfoId, x.PersonId });
        
        modelBuilder.Entity<User>().ToTable("users");
        modelBuilder.Entity<User>().Property(x => x.UserId).HasColumnName("username");
        modelBuilder.Entity<User>().Property(x => x.Password).HasColumnName("password");
        modelBuilder.Entity<User>().Property(x => x.Salt).HasColumnName("salt");
        modelBuilder.Entity<User>().Property(x => x.Role).HasColumnName("role");
        modelBuilder.Entity<User>().Property(x => x.CreationTime).HasColumnName("creationtime");
        modelBuilder.Entity<User>().Property(x => x.CreationTime).HasDefaultValue();

        modelBuilder.Entity<PersonRating>().ToTable("person_ratings");
        modelBuilder.Entity<PersonRating>().Property(x => x.PersonId).HasColumnName("nconst");
        modelBuilder.Entity<PersonRating>().Property(x => x.PrimaryName).HasColumnName("primaryname");
        modelBuilder.Entity<PersonRating>().Property(x => x.PersonScore).HasColumnName("personscore");
        modelBuilder.Entity<PersonRating>().HasKey(x => new { x.PersonId });
        
        modelBuilder.Entity<MovieBookmark>().ToTable("movie_bookmarks");
        modelBuilder.Entity<MovieBookmark>().Property(x => x.UserId).HasColumnName("username");
        modelBuilder.Entity<MovieBookmark>().Property(x => x.MovieInfoId).HasColumnName("tconst");
        modelBuilder.Entity<MovieBookmark>().HasKey(x => new { x.UserId, x.MovieInfoId });
        
        modelBuilder.Entity<PersonBookmark>().ToTable("name_bookmarks");
        modelBuilder.Entity<PersonBookmark>().Property(x => x.PersonId).HasColumnName("nconst");
        modelBuilder.Entity<PersonBookmark>().Property(x => x.UserId).HasColumnName("username");
        modelBuilder.Entity<PersonBookmark>().HasKey(x => new { x.PersonId, x.UserId });

        modelBuilder.Entity<UserRating>().ToTable("user_ratings");
        modelBuilder.Entity<UserRating>().Property(x => x.UserId).HasColumnName("username");
        modelBuilder.Entity<UserRating>().Property(x => x.MovieInfoId).HasColumnName("tconst");
        modelBuilder.Entity<UserRating>().Property(x => x.Rating).HasColumnName("rating");
        modelBuilder.Entity<UserRating>().HasKey(x => new { x.UserId, x.MovieInfoId });

        modelBuilder.Entity<MovieSearchResult>().HasNoKey();
        modelBuilder.Entity<MovieSearchResult>().Property(x => x.MovieInfoId).HasColumnName("movieid");
        modelBuilder.Entity<MovieSearchResult>().Property(x => x.Title).HasColumnName("title");
        modelBuilder.Entity<MovieSearchResult>().Property(x => x.Poster).HasColumnName("movieposter");
        
        modelBuilder.Entity<PersonSearchResult>().HasNoKey();
        modelBuilder.Entity<PersonSearchResult>().Property(x => x.PersonId).HasColumnName("nconst");
        modelBuilder.Entity<PersonSearchResult>().Property(x => x.Name).HasColumnName("name");
    }
}
