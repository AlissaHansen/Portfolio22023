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
        modelBuilder.Entity<MovieInfo>().Property(x => x.).HasColumnName("tconst");
    }
}
