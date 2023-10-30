using Core.Repositories.EntityFramework.Bases;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using Movie.Business.Services;
using Movie.DataAccess.Contexts;
using Movie.DataAccess.Repositories;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);

//regional adjustment
List<CultureInfo> cultures = new List<CultureInfo>()
{
    new CultureInfo("en-US") 
};

builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    options.DefaultRequestCulture = new RequestCulture(cultures.FirstOrDefault().Name);
    options.SupportedCultures = cultures;
    options.SupportedUICultures = cultures;
});

builder.Services.AddControllersWithViews();


builder.Services
    .AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)//authentication

    .AddCookie(config =>

    {
        config.LoginPath = "/User/Login";

        config.AccessDeniedPath = "/User/AccessDenied";

        config.ExpireTimeSpan = TimeSpan.FromMinutes(30);

        config.SlidingExpiration = true;
    });

builder.Services.AddSession(config => //add session
{
    config.IdleTimeout = TimeSpan.FromMinutes(30); 
    config.IOTimeout = Timeout.InfiniteTimeSpan; 
});

// Add services to the container.

string connectionString = builder.Configuration.GetConnectionString("Context");
builder.Services.AddDbContext<ShopAppContext>(options => options.UseSqlServer(connectionString));

builder.Services.AddScoped(typeof(RepoBase<>), typeof(Repo<>));
builder.Services.AddScoped<ISilverScreenService, SilverScreenService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IAccountService, AccountService>();

var app = builder.Build();

app.UseRequestLocalization(new RequestLocalizationOptions()
{
    DefaultRequestCulture = new RequestCulture(cultures.FirstOrDefault().Name),
    SupportedCultures = cultures,
    SupportedUICultures = cultures,
});

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
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

