//pengatur route API, untuk mengatur route API dan membuat webapp serta service seperti middleware
using API.Data;
using Microsoft.EntityFrameworkCore;
using API.Interfaces;
using API.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"));
}); // untuk set connection ke database SQLite. Connection string diambil dari appsettings.json.

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddCors();

builder.Services.AddScoped<ITokenService, TokenService>(); // untuk menambahkan service TokenService ke dalam container dependency injection.

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        var tokenKey = builder.Configuration["TokenKey"] 
        ?? throw new Exception("Token key not found in configuration");
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(tokenKey)),
            ValidateIssuer = false,
            ValidateAudience = false
        };
    });

var app = builder.Build();

app.UseCors(x => x.AllowAnyHeader().AllowAnyMethod().
    WithOrigins("http://localhost:4200","https://localhost:4200")); // untuk mengatur CORS agar bisa diakses dari localhost:4200 (Angular app)

app.UseAuthentication(); // middleware untuk memvalidasi token JWT pada setiap request
app.UseAuthorization(); // middleware untuk mengecek apakah user punya akses ke endpoint

app.MapControllers();

app.Run();


