using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TechWave.Models;
using TechWave.Models.DomainModel;
using TechWave.Models.SeedData;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<TechWaveDBContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("TechWaveContext")));

builder.Services.AddIdentity<User, IdentityRole>(options => {
    options.Password.RequiredLength = 6;
    options.Password.RequireNonAlphanumeric = true;
    options.Password.RequireDigit = true;
    options.User.RequireUniqueEmail = true;
}).AddRoles<IdentityRole>()

.AddEntityFrameworkStores<TechWaveDBContext>()
.AddDefaultTokenProviders();

// Add Identity services
/*builder.Services.AddDefaultIdentity<User>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<TechWaveDBContext>();*/

var app = builder.Build();

// Apply any pending migrations and update the database
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var dbContext = services.GetRequiredService<TechWaveDBContext>();

    try
    {
        // Apply migrations
        dbContext.Database.Migrate();

        // Seed the database
        SeedTechWaveData.Initialize(services);
    }
    catch (Exception ex)
    {
        // Log the exception
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred seeding the DB.");
    }
}

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

var scopeFactory = app.Services.GetRequiredService<IServiceScopeFactory>();
using (var scope = scopeFactory.CreateScope())
{
    await ConfigureIdentity.CreateAdminUserAsync(scope.ServiceProvider);
}

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
