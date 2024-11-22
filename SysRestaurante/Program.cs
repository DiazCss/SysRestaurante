using Microsoft.EntityFrameworkCore;
using SysRestaurante.DAL;
using Microsoft.AspNetCore.Authentication.Cookies;
using SysRestaurante.IoC;
using SysRestaurante.Models;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); 
    options.Cookie.HttpOnly = true;  
    options.Cookie.IsEssential = true; 
});
builder.Services.AddScoped<Credencial>();

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;

});
builder.Services.AddIoCDependecies(builder.Configuration);
// Configuraci�n para la conexion a la bd 
var conString = builder.Configuration.GetConnectionString("Conn");
builder.Services.AddDbContext<SysRestauranteDbContext>(
    options => options.UseMySql(conString, ServerVersion.AutoDetect(conString))
);


// Configuraci�n de la autenticaci�n de cookies
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie((o) =>
{
    o.LoginPath = new PathString("/Usuario/login");
    o.AccessDeniedPath = new PathString("/Usuario/login");
    o.ExpireTimeSpan = TimeSpan.FromHours(8);
    o.SlidingExpiration = true;
    o.Cookie.HttpOnly = true;
});

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();


if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();
app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
