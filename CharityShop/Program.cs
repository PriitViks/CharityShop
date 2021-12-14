using Application;
using Infrastructure;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

ConfigureServices(builder.Services);

void ConfigureServices(IServiceCollection services)
{
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

    services.AddControllersWithViews();

    services.AddDbContext<ApplicationDbContext>(options =>
      options.UseNpgsql(connectionString));
    services.AddApplication();
    services.AddInfrastructure(builder.Configuration);

    services.AddDatabaseDeveloperPageExceptionFilter();
    services.AddSwaggerGen();
}
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    try
    {
        var context = services.GetRequiredService<ApplicationDbContext>();
        if (context.Database.IsNpgsql())
        {
            context.Database.Migrate();
        }
        await ApplicationDbContextSeed.SeedDefaultDataAsync(context);
    }
    catch (Exception ex)
    {
        var logger = app.Services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred while migrating or seeding the database.");
    }
}

app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}");

app.MapFallbackToFile("index.html"); ;

app.Run();
