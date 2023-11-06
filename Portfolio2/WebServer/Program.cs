using DataLayer;
using WebServiceToken.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddSingleton<Hashing>();

builder.Services.AddSingleton<IDataService, DataService>();

var app = builder.Build();

app.MapControllers();

app.Run();