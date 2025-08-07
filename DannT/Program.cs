using DannT.Models.Context;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddSwaggerGen();

var stringConnection = builder.Configuration.GetConnectionString("MyStringConnection");
//Inyect the DB
builder.Services.AddDbContext<MyContext>(options =>
{
    options.UseSqlServer(stringConnection);
});
//Authentication
builder.Services.AddAuthentication(options =>
{
    //cookies por defecto
    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;

    //Google cuando redirigimos a un sitio externo
    options.DefaultChallengeScheme = GoogleDefaults.AuthenticationScheme;
})//Configuración de cookies
    .AddCookie(options =>
    {
        options.AccessDeniedPath = "/Auth/Forbidden";
        options.ExpireTimeSpan = TimeSpan.FromDays(3);
    })
    //Configuración de Api de Google
    .AddGoogle(options =>
    {
        options.ClientId = builder.Configuration["client_id"]!;
        options.ClientSecret= builder.Configuration["project_id"]!;

        // Mapear mas claism si lo necesito
        //options.ClaimActions.MapJsonKey(ClaimTypes.GivenName, "given_name");
        //options.ClaimActions.MapJsonKey(ClaimTypes.Surname, "family_name");
    });


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthentication();    
app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Auth}/{action=Login}/{id?}")
    .WithStaticAssets();


app.Run();
