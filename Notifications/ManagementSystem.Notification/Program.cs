using ManagementSystem.Notification.Consumers.EmailConsumers;
using MassTransit;

var builder = WebApplication.CreateBuilder(args);

// RabbitMQ yapýlandýrmasý
var rabbitMqHost = builder.Configuration["RabbitMQ:Host"];
var rabbitMqPort = builder.Configuration["RabbitMQ:Port"];

// MassTransit ve RabbitMQ yapýlandýrmasý
builder.Services.AddMassTransit(x =>
{
    x.AddConsumer<SendEmailConsumer>();

    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host(rabbitMqHost, rabbitMqPort, "/", h =>
        {
            h.Username("guest");
            h.Password("guest");
        });

        cfg.ReceiveEndpoint("sample_event_queue", e =>
        {
            e.ConfigureConsumer<SendEmailConsumer>(context);
        });
    });
}).BuildServiceProvider();

builder.Services.AddMassTransitHostedService();
builder.Services.AddControllers();
var app = builder.Build();

app.UseRouting();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.Run();
