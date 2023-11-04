using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using Npgsql;

namespace DataLayer;

public class DataService : IDataService
{
    public (IList<MovieInfo> movieInfos, int count) GetMovieInfos(int page, int pageSize)
    {
        var db = new MoviedbContext();
        var movieInfos = db.MovieInfos
            .Include(x=> x.Rating)
            .Skip(page * pageSize)
            .Take(pageSize)
            .ToList();
        return (movieInfos, db.MovieInfos.Count());
    }
    public (IList<MovieInfo> movieInfos, int count) GetRankedMovieInfos(int page, int pageSize)
    {
        var db = new MoviedbContext();
        var movieInfos = db.MovieInfos
            .Include(x => x.Rating)
            .Where(x => x.Rating.NumVotes > 10000)
            .OrderByDescending(x => x.Rating != null ? x.Rating.AverageRating : 0) // Handle null ratings
            .Skip(page * pageSize)
            .Take(pageSize)
            .ToList();
        return (movieInfos, db.MovieInfos.Count());
    }

    public MovieInfo? GetMovieInfo(string searchId)
    {
        var db = new MoviedbContext();

        foreach (var item in db.MovieInfos
                     .Include(r => r.Rating)
                     .Include(g => g.Genres)
                     .Include(m => m.MoviePrincipals)
                     .ThenInclude(p=> p.Person)
                     .Where(x => x.Id.Equals(searchId)))
        {
            return item;
        }
        return null;
    }
    
    public (IList<Person> persons, int count) GetPersons(int page, int pageSize)
    {
        var db = new MoviedbContext();
        var persons = db.Persons
            .Include(p=> p.Professions)
            .Skip(page * pageSize)
            .Take(pageSize)
            .ToList();
        return (persons, db.Persons.Count());
    }
    public (IList<Person> persons, int count) GetRankedPersons(int page, int pageSize)
    {
        var db = new MoviedbContext();
        var persons = db.Persons
            .Include(p=> p.PersonRating)
            .Include(p => p.Professions)
            .OrderByDescending(x => x.PersonRating != null ? x.PersonRating.PersonScore : 0) // Handle null ratings
            .Skip(page * pageSize)
            .Take(pageSize)
            .ToList();
        return (persons, db.Persons.Count());
    }

    public Person? GetPerson(string searchId)
    {
        var db = new MoviedbContext();

        foreach (var item in db.Persons
                     .Include(p=> p.Professions)
                     .Include(k => k.KnownFors)
                     .Where(x => x.Id.Equals(searchId)))
        {
            return item;
        }
        return null;
    }

    public (IList<User> users, int count) GetUsers(int page, int pageSize)
    {
        var db = new MoviedbContext();
        var users = db.Users
            .Skip(page * pageSize)
            .Take(pageSize)
            .ToList();
        return (users, db.Users.Count());
    }

    public User? GetUser(string username)
    {
        var db = new MoviedbContext();

        foreach (var item  in db.Users
                     .Include(b=> b.MovieBookmarks)
                     .Include(p=> p.PersonBookmarks)
                     .Include(u => u.UserRatings)
                     .Where(u=>u.UserId.Equals(username)))
        {
            return item;
        }
        return null;
    }

    public bool CreateUser(User userToCreate)
    {
        var db = new MoviedbContext();
        var user = new User
        {
            UserId = userToCreate.UserId,
            Password = userToCreate.Password,
        };
        db.Add(user);
        return db.SaveChanges() > 0;
    }

    public bool DeleteUser(string userId)
    {
        var db = new MoviedbContext();
        var user = db.Users.FirstOrDefault(x => x.UserId.Equals(userId));
        if (user != null)
        {
            db.Users.Remove(user);
        }
        return db.SaveChanges() > 0;
    }

    public bool UpdateUser(User userToUpdate)
    {
        var db = new MoviedbContext();
        var user = db.Users.FirstOrDefault(x => x.UserId.Equals(userToUpdate.UserId));
        if (user != null)
        {
            user.Password = userToUpdate.Password;
        }

        return db.SaveChanges() > 0;

    }

    public bool CreateMovieBookmark(MovieBookmark movieBookmark)
    {
        var db = new MoviedbContext();
        var bookmark = new MovieBookmark
        {
            MovieInfoId = movieBookmark.MovieInfoId,
            UserId = movieBookmark.UserId
        };
        db.Add(bookmark);
        return db.SaveChanges() > 0;
    }

    public bool DeleteMovieBookmark(MovieBookmark movieBookmark)
    {
        var db = new MoviedbContext();
        var bookmark = db.MovieBookmarks.FirstOrDefault(x =>
            x.UserId.Equals(movieBookmark.UserId) && x.MovieInfoId.Equals(movieBookmark.MovieInfoId));
        if (bookmark != null)
        {
            db.MovieBookmarks.Remove(bookmark);
        }

        return db.SaveChanges()>0;
    }
    public bool CreatePesonBookmark(PersonBookmark personBookmark)
    {
        var db = new MoviedbContext();
        var bookmark = new PersonBookmark
        {
            PersonId = personBookmark.PersonId,
            UserId = personBookmark.UserId
        };
        db.Add(bookmark);
        return db.SaveChanges() > 0;
    }
    public bool DeletePersonBookmark(PersonBookmark personBookmark)
    {
        var db = new MoviedbContext();
        var bookmark = db.PersonBookmarks.FirstOrDefault(x =>
            x.UserId.Equals(personBookmark.UserId) && x.PersonId.Equals(personBookmark.PersonId));
        if (bookmark != null)
        {
            db.PersonBookmarks.Remove(bookmark);
        }

        return db.SaveChanges()>0;
    }

    public bool CreateRating(UserRating userRating)
    {
        using var db = new MoviedbContext();
        try
        {
            db.Database.ExecuteSqlInterpolated
                ($"select rate({userRating.UserId}, {userRating.MovieInfoId}, {userRating.Rating})");

            return true;
        }
        catch (Exception ex)
        {
            return false;
        }
    }

    public bool DeleteRating(string username, string movieId)
    {
        using var db = new MoviedbContext();
        try
        {
            db.Database.ExecuteSqlInterpolated($"select delete_rating({username}, {movieId})");

            return true;
        }
        catch (Exception ex)
        {
            return false;
        }
    }
}