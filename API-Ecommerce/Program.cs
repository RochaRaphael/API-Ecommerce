using API_Ecommerce.Data;
using API_Ecommerce.Repositories;
using API_Ecommerce.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.Run();

void ConfigureServices(WebApplicationBuilder builder)
{
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
    builder.Services.AddDbContext<DataContext>(
        options =>
            options.UseSqlServer(connectionString));

    builder.Services.AddScoped<UserRepositories>();
    builder.Services.AddScoped<UserServices>();

}