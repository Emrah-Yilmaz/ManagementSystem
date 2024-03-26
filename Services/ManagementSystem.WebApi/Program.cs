using ManagementSystem.Application.Features.Queries.WorkTask;
using ManagementSystem.Domain.Services.Abstract;
using ManagementSystem.Domain.Services.Concrete;
using ManagementSystem.Infrastructure.Extensions;
using MediatR;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(typeof(GetWorkTasksQueryHandler).GetTypeInfo().Assembly));

builder.Services.AddScoped<IWorkTaskService, WorkTaskService>();

builder.Services.AddInfrastructureRegistration(builder.Configuration);

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

app.UseHttpsRedirection();

app.UseAuthorization();
app.UseCors("MyPolicy");

app.MapControllers();

app.Run();
