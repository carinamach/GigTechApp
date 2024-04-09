using GigTechMvc.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using GigTech.Shared;


var builder = WebApplication.CreateBuilder(args);

// Pass configuration explicitly to ConfigureServices method
ConfigureServices(builder.Services, builder.Configuration);

// Modify ConfigureServices method to accept IConfiguration
void ConfigureServices(IServiceCollection services, IConfiguration configuration)
{
    services.AddDbContext<GigTechContext>(options =>
        options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
    services.AddDbContext<IdentityContext>(options =>
        options.UseSqlServer(configuration.GetConnectionString("IdentityConnection")));
    services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
        .AddEntityFrameworkStores<IdentityContext>();
    services.AddAuthorization();
    services.AddControllersWithViews();
}

var app = builder.Build();

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
app.MapControllerRoute(
    name: "forum",
    pattern: "{controller=Forum}/{action=ForumIndex}/{id?}");
app.MapRazorPages();

app.Run();

