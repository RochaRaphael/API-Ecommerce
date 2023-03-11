using API_Ecommerce;
using API_Ecommerce.Data;
using API_Ecommerce.Repositories;
using API_Ecommerce.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);
ConfigureMvc(builder);
ConfigureServices(builder);

builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();
LoadConfiguration(app);

app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();

void LoadConfiguration(WebApplication app)
{
    Configuration.JwtKey = app.Configuration.GetValue<string>("JwtKey");
}

void ConfigureAuthentication(WebApplicationBuilder builder)
{
    var key = Encoding.ASCII.GetBytes(Configuration.JwtKey);
    builder.Services.AddAuthentication(x =>
    {
        x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    }).AddJwtBearer(x =>
    {
        x.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key),
            ValidateIssuer = false,
            ValidateAudience = false
        };
    });
}

void ConfigureMvc(WebApplicationBuilder builder)
{
    builder.Services.AddControllers()
    .ConfigureApiBehaviorOptions(options =>
    {
        options.SuppressModelStateInvalidFilter = true;
    })
    .AddJsonOptions(x =>
    {
        x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
        x.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingDefault;
    }); ;
}

void ConfigureServices(WebApplicationBuilder builder)
{
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
    builder.Services.AddDbContext<DataContext>(
        options =>
            options.UseSqlServer(connectionString));
    builder.Services.AddTransient<TokenService>();

    builder.Services.AddScoped<UserRepositories>();
    builder.Services.AddScoped<UserServices>();
    builder.Services.AddScoped<CategoryRepositories>();
    builder.Services.AddScoped<CategoryServices>();
    builder.Services.AddScoped<ProductRepositories>();
    builder.Services.AddScoped<ProductServices>();
    builder.Services.AddScoped<RoleRepositories>();
    builder.Services.AddScoped<RoleServices>();

}