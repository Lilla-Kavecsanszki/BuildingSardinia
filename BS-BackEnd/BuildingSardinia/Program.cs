using BuildingSardinia.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Configure DbContext to use PostgreSQL
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql("Host=localhost;Database=building_sardinia;Username=lilla;Password=sardinia"));

// Add services to the container
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

// app.UseHttpsRedirection();
app.UseDefaultFiles(); // Enable serving of default files like index.html
app.UseStaticFiles();  // Serve static files from wwwroot

app.UseRouting();

app.UseAuthorization();

// Map controllers for your APIs and MVC views
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// Remove the call to MapRazorPages since we're not using Razor Pages
// app.MapRazorPages();

app.Run("http://localhost:5014"); // Specify the URL if needed
