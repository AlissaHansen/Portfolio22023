﻿using DataLayer; 
using Microsoft.EntityFrameworkCore;

var ds = new DataService();

//var db = new MoviedbContext();

// foreach (var x in db.MovieInfos.Include(x => x.Genres)
//              .Take(10))
// {
//     Console.WriteLine($"{x.Id}, {string.Join(",", x.Genres.Select(x => x.GenreName))}");
// }

var movie = ds.GetMovieInfo("tt0052520");

foreach (var entity in movie.Genres)
{
    Console.WriteLine(entity.GenreName);
}


