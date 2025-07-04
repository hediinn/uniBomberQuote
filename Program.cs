
using Microsoft.EntityFrameworkCore;
using uniBomberQuote.Models;
using System.IO;


//List<String> test =

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddDbContext<DataContext>(opts =>{
    opts.UseSqlite(builder.Configuration[
        "ConnectionStrings:ProductConnection"]);
    opts.EnableSensitiveDataLogging(true);
});
builder.Services.AddControllersWithViews();

var app = builder.Build();

app.UseStaticFiles();
app.MapControllers();
app.UseRouting();

var context = app.Services.CreateScope()
    .ServiceProvider
    .GetRequiredService<DataContext>();

SeedData.SeedDatabase(context);

app.MapGet("/", () => $"{context.Sentences.First().SentenceType}");

app.Run();
