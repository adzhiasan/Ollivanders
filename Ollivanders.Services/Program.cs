using Microsoft.EntityFrameworkCore;
using Ollivanders.Services.Database;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<DatabaseContext>(options =>
{
    var connectionString = builder.Configuration.GetRequiredSection("SqlConnection:ConnectionString").Value;
    options.UseSqlServer(connectionString);
});

builder.Services.AddScoped<MagicWandRepository>();

var app = builder.Build();

app.Run();