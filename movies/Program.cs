using Autofac;
using Autofac.Extensions.DependencyInjection;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using movies;
using movies.Repositories;
using System.Reflection;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthorization();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
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
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key!)),
        ValidateIssuerSigningKey = true,
    };
});

builder.Services.AddControllers();

builder.Services.AddCors();

builder.Services.AddFluentValidation(s =>
{
    s.RegisterValidatorsFromAssemblyContaining<Program>();
    s.ValidatorOptions.CascadeMode = FluentValidation.CascadeMode.StopOnFirstFailure;
});

builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());

builder.Host.ConfigureContainer<ContainerBuilder>(builder =>
{
    var assembly = Assembly.GetAssembly(typeof(Program));

    builder.RegisterType<FilmDbContext>().SingleInstance();
    builder.RegisterType<DependencyFactory>();
    builder.RegisterAssemblyTypes(assembly).Where(t => t.Name.EndsWith("Repository")).InstancePerLifetimeScope().AsImplementedInterfaces();
    builder.RegisterAssemblyTypes(assembly).Where(t => t.Name.EndsWith("Builder")).InstancePerLifetimeScope();
    builder.RegisterAssemblyTypes(assembly).Where(t => t.Name.EndsWith("Operation")).InstancePerLifetimeScope().AsImplementedInterfaces();
    builder.RegisterAssemblyTypes(assembly).Where(t => t.Name.EndsWith("Validation")).InstancePerLifetimeScope();
    builder.RegisterAssemblyTypes(assembly).Where(t => t.Name.EndsWith("Validator")).InstancePerLifetimeScope();
    builder.RegisterAssemblyTypes(assembly).Where(t => t.Name.EndsWith("Entity")).AsImplementedInterfaces();
});

builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseHttpsRedirection();
app.UseCors(builder => builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod().SetPreflightMaxAge(TimeSpan.MaxValue));

// Configure the HTTP request pipeline.

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.UseSwagger();
app.UseSwaggerUI();

app.Run();
