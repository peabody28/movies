using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using movies.Entities;
using movies.Interfaces.Entities;
using movies.Interfaces.Operations;
using movies.Interfaces.Repositories;
using movies.Operations;
using movies.Repositories;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthorization();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        var issuer = builder.Configuration.GetSection("AuthOptions:ISSUER").Value;
        var audience = builder.Configuration.GetSection("AuthOptions:AUDIENCE").Value;
        var key = builder.Configuration.GetSection("AuthOptions:KEY").Value;

        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = issuer,
            ValidateAudience = true,
            ValidAudience = audience,
            ValidateLifetime = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key)),
            ValidateIssuerSigningKey = true,
        };
    });

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddDbContext<FilmDbContext>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IDirectorRepository, DirectorRepository>();
builder.Services.AddScoped<IRatingRepository, RatingRepository>();
builder.Services.AddScoped<IRatingTypeRepository, RatingTypeRepository>();
builder.Services.AddScoped<IFilmRepository, FilmRepository>();
builder.Services.AddScoped<ICountryRepository, CountryRepository>();

builder.Services.AddTransient<IUser, UserEntity>();
builder.Services.AddTransient<ICountry, CountryEntity>();
builder.Services.AddTransient<IDirector, DirectorEntity>();
builder.Services.AddTransient<IFilm, FilmEntity>();
builder.Services.AddTransient<IRating, RatingEntity>();
builder.Services.AddTransient<IRatingType, RatingTypeEntity>();

builder.Services.AddScoped<IIdentityOperation, IdentityOperation>();
builder.Services.AddScoped<IAuthorizationOperation, AuthorizationOperation>();

builder.Services.AddCors();

var app = builder.Build();

app.UseCors(builder => builder.AllowAnyOrigin());

// Configure the HTTP request pipeline.

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
