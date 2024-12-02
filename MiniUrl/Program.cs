using Microsoft.EntityFrameworkCore;
using MiniUrl.Database;
using Microsoft.Data.Sqlite;

var builder = WebApplication.CreateBuilder(args);

// Load configurations from appsettings.json and serviceDependencies.json
builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
builder.Configuration.AddJsonFile("serviceDependencies.json", optional: true, reloadOnChange: true);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Read the connection string from the configuration
string connectionString = builder.Configuration.GetConnectionString("MiniUrlDatabase");

// Ensure the Data directory and database file exist
string dataDirectory = Path.Combine(Directory.GetCurrentDirectory(), "Data");
if (!Directory.Exists(dataDirectory))
{
    Directory.CreateDirectory(dataDirectory);
}

string dbFilePath = Path.Combine(dataDirectory, "SQLiteDatabase.db");
if (!File.Exists(dbFilePath))
{
    using (var connection = new SqliteConnection($"Data Source={dbFilePath}"))
    {
        connection.Open();
        using (var command = connection.CreateCommand())
        {
            command.CommandText = "CREATE TABLE IF NOT EXISTS UrlMappings (Id INTEGER PRIMARY KEY, HashedUrl TEXT, OriginalUrl TEXT)";
            command.ExecuteNonQuery();
        }
    }
}

// Add the DbContext with the connection string
builder.Services.AddDbContext<MiniUrlDbContext>(options => options.UseSqlite(connectionString));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
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
    name: "redirect",
    pattern: "{hashedUrl}",
    defaults: new { controller = "Home", action = "RedirectToOriginal" });

app.Run();
