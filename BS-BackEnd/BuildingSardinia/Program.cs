using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using BuildingSardinia.Models;
using BuildingSardinia.Services; // Ensure this namespace is included
using Umbraco.Cms.Core.DependencyInjection;
using Umbraco.Cms.Web.Common.Extensions;
using Umbraco.Cms.Web.Common;
using System.Security.Claims; // Ensure this namespace is included

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllersWithViews();

builder.Services.AddScoped<IPropertyService, PropertyService>();

// Configure DbContext with SQLite
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

// Configure Umbraco with required parameters
builder.Services.AddUmbraco(builder.Environment, builder.Configuration)
    .AddBackOffice()
    .AddWebsite()
    .AddComposers()
    .Build();

builder.WebHost.ConfigureKestrel(serverOptions =>
{
    serverOptions.Limits.MaxRequestBodySize = 50 * 1024 * 1024; // 50 MB
});

var app = builder.Build();

// Boot Umbraco
await app.BootUmbracoAsync();

// Configure the HTTP request pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

// Uncomment the following line if you want HTTPS redirection
// app.UseHttpsRedirection();

app.UseStaticFiles();  // Serve static files from wwwroot

app.UseRouting();

app.UseAuthentication(); // Ensure authentication is used
app.UseAuthorization();

// Map Umbraco routes
app.UseUmbraco()
    .WithMiddleware(u =>
    {
        u.UseBackOffice();
        u.UseWebsite();
    })
    .WithEndpoints(u =>
    {
        u.UseBackOfficeEndpoints();
        u.UseWebsiteEndpoints();
    });

// Map controllers for your APIs and MVC views
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "properties",
    pattern: "Properties/{action=PropertyListing}/{id?}",
    defaults: new { controller = "Properties" });

app.MapControllerRoute(
    name: "construction",
    pattern: "Construction/{action=Index}/{id?}",
    defaults: new { controller = "Construction" });

app.MapControllerRoute(
    name: "rent",
    pattern: "Rent/{action=Index}/{id?}",
    defaults: new { controller = "Rent" });

app.MapControllerRoute(
    name: "about",
    pattern: "About/{action=Index}/{id?}",
    defaults: new { controller = "About" });

app.MapControllerRoute(
    name: "contact",
    pattern: "Contact/{action=Contact}/{id?}",
    defaults: new { controller = "Contact" });


// Restrict access to Umbraco back office
app.MapWhen(
    context => context.Request.Path.StartsWithSegments("/umbraco"),
    appBuilder =>
    {
        appBuilder.Use(async (context, next) =>
        {
            // Check if the user is authenticated and authorized
            if (context.User?.Identity?.IsAuthenticated == true && UserIsAdmin(context.User))
            {
                await next();
            }
            else
            {
                context.Response.Redirect("/home"); // Redirect unauthorized users
            }
        });
    });

app.Run();

// Static method to check if the user is an admin
static bool UserIsAdmin(ClaimsPrincipal user)
{
    // Check if the user is in the "Administrators" role
    return user.IsInRole("Administrators");
}
