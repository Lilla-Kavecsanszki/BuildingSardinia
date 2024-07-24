using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using BuildingSardinia.Models; // Adjust if your namespace is different
using Umbraco.Cms.Core.DependencyInjection;
using System.Security.Claims; // Ensure this namespace is included

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllersWithViews();

// Configure DbContext with SQLite
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

// Configure Umbraco
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
app.UseUmbraco();

// Map controllers for your APIs and MVC views
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

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
