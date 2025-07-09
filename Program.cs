
using Microsoft.EntityFrameworkCore;
using uniBomberQuote;
using uniBomberQuote.Components;
using uniBomberQuote.Models;
using Microsoft.AspNetCore.HttpLogging;
using Microsoft.AspNetCore.Http.HttpResults;

var builder = WebApplication.CreateBuilder(args);
using StreamReader reader = new StreamReader("Industrial_Society.txt");
string[] strings = reader.ReadToEnd().Split("\n\n");

builder.Services.AddDbContext<DataContext>(opts =>
{
    opts.UseSqlite(builder.Configuration[
        "ConnectionStrings:ProductConnection"]);
    opts.EnableSensitiveDataLogging(true);
});
builder.Services.AddHttpLogging(logging =>
{ 
});
builder.Services.AddControllersWithViews();
builder.Services.AddRazorComponents();

var app = builder.Build();
app.UseHttpLogging();
//app.UseStaticFiles();
app.MapControllers();
app.MapRazorComponents<App>();
//app.MapRazorComponents<App1>();
app.UseRouting();
//app.UseHttpsRedirection();


app.UseAntiforgery();
var context = app.Services.CreateScope()
    .ServiceProvider
    .GetRequiredService<DataContext>();


context.Database.EnsureCreated();
SentencesMaker.addAlldata(context, strings);

//app.MapGet("/", () => $"{context.DataSentences.First().MySentences}");
//app.MapGet("/test/{t}", (int t) => new RazorComponentResult<Test>());
//app.MapGet("/app", () => new RazorComponentResult<App>());
app.Run();
