using api.Domain;
using Version = api.Version;

var builder = WebApplication.CreateBuilder(args);


var app = builder.Build();

app.MapGet("/v1/modpacks/", () => "Hello World!");
app.MapGet("/v1/modpack/{modpack}/", () => "Hello World!");

app.Run();