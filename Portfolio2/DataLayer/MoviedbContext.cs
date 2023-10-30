using Microsoft.EntityFrameworkCore;

namespace DataLayer;

public class MoviedbContext : DbContext
{
    public DbSet<MovieInfo> MovieInfos { get; set; }
    public DbSet<Genre> Genres { get; set; }
    public DbSet<Rating> Ratings { get; set; }

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
        

    }
    
    
}
