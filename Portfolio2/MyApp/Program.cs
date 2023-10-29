using DataLayer;

var ds = new DataService();

var movieInfo = ds.GetMovieInfo("tt0078672");

Console.WriteLine(movieInfo.Genres.Count());

