using Microsoft.EntityFrameworkCore;

namespace DataLayer;

public class MoviedbContext : DbContext
{
    public DbSet<MovieInfo> MovieInfos { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
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
    }
}
