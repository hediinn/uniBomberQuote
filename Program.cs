
using Microsoft.EntityFrameworkCore;
using uniBomberQuote;
using uniBomberQuote.Components;
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
builder.Services.AddRazorComponents();
var app = builder.Build();

app.UseStaticFiles();
app.MapControllers();
app.MapRazorComponents<App>();
app.UseRouting();
app.UseHttpsRedirection();


app.UseAntiforgery();
var context = app.Services.CreateScope()
    .ServiceProvider
    .GetRequiredService<DataContext>();


context.Database.EnsureCreated();
SentencesMaker.addAlldata(context, strings);


app.MapGet("/", () => $"{context.DataSentences.First().MySentences}");
app.Run();
