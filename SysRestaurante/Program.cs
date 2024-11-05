using Microsoft.EntityFrameworkCore;
using SysRestaurante.DAL;
using Microsoft.AspNetCore.Authentication.Cookies;
using SysRestaurante.IoC;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddControllers();
builder.Services.AddIoCDependecies(builder.Configuration);
// Configuraci�n para la conexion a la bd 
var conString = builder.Configuration.GetConnectionString("Conn");
builder.Services.AddDbContext<SysRestauranteDbContext>(
    options => options.UseMySql(conString, ServerVersion.AutoDetect(conString))
);

// Configuraci�n de la autenticaci�n de cookies
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Usuario/Login";           // Ruta para el login
        options.AccessDeniedPath = "/Usuario/Login";     // Ruta en caso de acceso denegado
        options.ExpireTimeSpan = TimeSpan.FromHours(8);  
        options.SlidingExpiration = true;                
        options.Cookie.HttpOnly = true;                
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

// Configuraci�n del middleware de autenticaci�n y autorizaci�n
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
