using API_Ecommerce.Data;
using API_Ecommerce.Repositories;
using API_Ecommerce.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
ConfigureMvc(builder);
ConfigureServices(builder);

builder.Services.AddEndpointsApiExplorer();
var app = builder.Build();

app.MapControllers();

app.Run();

void ConfigureMvc(WebApplicationBuilder builder)
{
    builder.Services.AddControllers()
    .ConfigureApiBehaviorOptions(options =>
    {
        options.SuppressModelStateInvalidFilter = true;
    });
}

void ConfigureServices(WebApplicationBuilder builder)
{
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
    builder.Services.AddDbContext<DataContext>(
        options =>
            options.UseSqlServer(connectionString));

    builder.Services.AddScoped<UserRepositories>();
    builder.Services.AddScoped<UserServices>();

}