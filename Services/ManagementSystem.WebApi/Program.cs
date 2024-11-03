using CommonLibrary.Options;
using FluentValidation.AspNetCore;
using ManagementSystem.Application.Extensions;
using ManagementSystem.Domain.Extensions;
using ManagementSystem.Infrastructure.Extensions;
using ManagementSystem.WebApi.Configurations.Swagger;
using ManagementSystem.WebApi.Extensions;
using MassTransit;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Packages.Extensions;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new System.Text.Json.Serialization.JsonStringEnumConverter());
    })
    .AddFluentValidation();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Services.Configure<LocationOptions>(builder.Configuration.GetSection("LocationOptions"));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddApplicationRegistration();
builder.Services.AddWebApiRegistration();
builder.Services.AddDomainRegistration();
builder.Services.AddInfrastructureRegistration(builder.Configuration);
builder.Services.AddStackExchangeRedisCache(opt => opt.Configuration = "localhost:6379");

var rabbitMqHost = builder.Configuration["RabbitMQ:Host"];
var rabbitMqPort = builder.Configuration["RabbitMQ:Port"];


builder.Services.AddMassTransit(x =>
{
    x.UsingRabbitMq((context, cfg) =>
    {
        var rabbitMqConfig = context.GetRequiredService<IConfiguration>().GetSection("RabbitMQ");
        cfg.Host(rabbitMqConfig["Host"], h =>
        {
            h.Username(rabbitMqConfig["Username"]);
            h.Password(rabbitMqConfig["Password"]);
        });
    });
});
builder.Services.AddMassTransitHostedService();

builder.Services.AddSwaggerGen(opt =>
{ 
    opt.SwaggerDoc("v1", new OpenApiInfo { Title = "ManagementSystem", Version = "v1" });
    opt.UseInlineDefinitionsForEnums();
    opt.SchemaFilter<EnumSchemaFilter>();
    opt.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "bearer"
    });
    opt.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            new string[]{}
        }
    });
});

var key = Encoding.ASCII.GetBytes(builder.Configuration["AuthConfig:Secret"]);
builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;


}).AddJwtBearer(x =>
{

    x.Audience = "ManagementSystem";
    x.RequireHttpsMetadata = false;
    x.SaveToken = true;
    x.ClaimsIssuer = "MS.Issuer.Development";
    x.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
    {
        RequireExpirationTime = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidateIssuer = false,
        ValidateAudience = false
    };

});

builder.Services.AddCors(o => o.AddPolicy("MyPolicy", builder =>
{
    builder.AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader();
}));
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.ConfigureCustomerExceptionMiddleware();
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();
app.UseCors("MyPolicy");

app.MapControllers();

app.Run();
