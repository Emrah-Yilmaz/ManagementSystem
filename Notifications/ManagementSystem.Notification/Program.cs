using CommonLibrary.Options.Email;
using ManagementSystem.Notification.Consumers.EmailConsumers;
using ManagementSystem.Notification.Services;
using MassTransit;
using Microsoft.Extensions.Configuration;
using System.Configuration;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// RabbitMQ yapýlandýrmasý
var rabbitMqHost = builder.Configuration["RabbitMQ:Host"];
var rabbitMqPort = builder.Configuration["RabbitMQ:Port"];

// MassTransit ve RabbitMQ yapýlandýrmasý
#pragma warning disable ASP0000 // Do not call 'IServiceCollection.BuildServiceProvider' in 'ConfigureServices'
builder.Services.AddMassTransit(x =>
{
    x.AddConsumer<SendEmailConsumer>();
    x.UsingRabbitMq((context, cfg) =>
    {
        var rabbitMqConfig = builder.Configuration.GetSection("RabbitMQ");

        cfg.Host(rabbitMqConfig["Host"], h =>
        {
            h.Username(rabbitMqConfig["Username"]);
            h.Password(rabbitMqConfig["Password"]);
        });
        cfg.ReceiveEndpoint("email_queue", e =>
        {
            e.ConfigureConsumer<SendEmailConsumer>(context);
        });
    });
    builder.Services.Configure<EmailOptions>(builder.Configuration.GetSection("EmailSettings"));
    builder.Services.AddScoped<IEmailService, EmailService>();
}).BuildServiceProvider();
#pragma warning restore ASP0000 // Do not call 'IServiceCollection.BuildServiceProvider' in 'ConfigureServices'

builder.Services.AddMassTransitHostedService();
builder.Services.AddControllers();
var app = builder.Build();



if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseRouting();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.Run();
