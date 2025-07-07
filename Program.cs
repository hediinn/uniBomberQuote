
using Microsoft.EntityFrameworkCore;
using uniBomberQuote;
using uniBomberQuote.Models;


var builder = WebApplication.CreateBuilder(args);
using StreamReader reader = new StreamReader("Industrial_Society.txt");
string[] strings = reader.ReadToEnd().Split("\n\n");

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




SentencesMaker.addAlldata(context, strings);

app.MapGet("/", () => $"{context.Sentences.First().SentenceType}\n {context.Sentences.Count()}");

app.Run();
