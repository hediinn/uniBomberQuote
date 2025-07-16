using MudBlazor.Services;
using uniBomberQuote.Components;
using Microsoft.EntityFrameworkCore;
using uniBomberQuote.Shared.Models;
using uniBomberQuote.Client.Pages;
using Microsoft.AspNetCore.HttpLogging;

var builder = WebApplication.CreateBuilder(args);

// Add MudBlazor services
builder.Services.AddMudServices();


builder.Services.AddDbContext<DataContext>(opts=>
{  
    opts.UseSqlite(
        builder.Configuration[
        "ConnectionStrings:ProductConnection"]);
    opts.EnableSensitiveDataLogging(true);
});

 


// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents() 
    .AddInteractiveWebAssemblyComponents();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();


app.UseAntiforgery();

app.MapStaticAssets();


app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(uniBomberQuote.Client._Imports).Assembly);


var context = app.Services.CreateScope()
    .ServiceProvider
    .GetRequiredService<DataContext>();

context.Database.EnsureCreated();
SeedData.SeedDatabase(context);
app.Run();
