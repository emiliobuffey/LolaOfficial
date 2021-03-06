using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using LolaOfficial.Server.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using LolaOfficial.Client.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();


//#################################################################
var db = builder.Configuration.GetConnectionString("Default");
builder.Services.AddDbContext<UserContext>(options => options.UseSqlite(db));   // connection to db

//enabling serverside auth
builder.Services.AddAuthentication(options => 
options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(); 

//#################################################################


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication(); // enables authentication capabilities


app.MapRazorPages();
app.MapControllers();
app.MapFallbackToFile("index.html");

app.Run();
