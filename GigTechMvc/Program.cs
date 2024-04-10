using GigTechMvc.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using GigTech.Common.DataContext.SqlServer;
using GigTech.Shared;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
var connectionString1 = builder.Configuration.GetConnectionString("IdentityConnection") ?? throw new InvalidOperationException("Connection string 'IdentityConnection' not found.");
builder.Services.AddDbContext<GigTechContext>(options => options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();
builder.Services.AddDbContext<IdentityContext>(options => options.UseSqlServer(connectionString1));
builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<IdentityContext>();
builder.Services.AddAuthorization();

builder.Services.AddControllersWithViews();
var app = builder.Build();

ConfigureServices(builder.Services);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();

void ConfigureServices(IServiceCollection services)
{

}