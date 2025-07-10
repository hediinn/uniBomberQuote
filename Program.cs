
using Microsoft.EntityFrameworkCore;
using uniBomberQuote;
using uniBomberQuote.Components;
using uniBomberQuote.Models;
using Microsoft.AspNetCore.HttpLogging;


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
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

var app = builder.Build();
app.UseHttpsRedirection();
app.UseAntiforgery();
app.UseHttpLogging();
app.MapStaticAssets();


app.MapRazorComponents<App>().AddInteractiveServerRenderMode();
//app.MapRazorComponents<App1>();


var context = app.Services.CreateScope()
    .ServiceProvider
    .GetRequiredService<DataContext>();


context.Database.EnsureCreated();
SentencesMaker.addAlldata(context, strings);

//app.MapGet("/", () => $"{context.DataSentences.First().MySentences}");
//app.MapGet("/test/{t}", (int t) => new RazorComponentResult<Test>());
//app.MapGet("/app", () => new RazorComponentResult<App>());
app.Run();
