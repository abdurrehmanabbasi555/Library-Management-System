using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using LibrarySystem.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Add Entity Framework
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(connectionString));
// Add Identity (Authentication & Authorization)
builder.Services.AddDefaultIdentity<IdentityUser>(options =>
{
    // Password settings (make it simple for development)
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequiredLength = 6;
})
.AddEntityFrameworkStores<AppDbContext>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

// app.UseHttpsRedirection(); // Comment this out for now
app.UseStaticFiles();
app.UseRouting();

app.UseAuthentication();  // Who are you?
app.UseAuthorization(); // What can you do?

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Books}/{action=Index}/{id?}"); // Changed to start at Books

// Add Razor Pages support (needed for Identity UI)
app.MapRazorPages();

app.Run();