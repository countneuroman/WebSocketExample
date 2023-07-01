var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

var app = builder.Build();

var webSocketOptions = new WebSocketOptions
{
    KeepAliveInterval = TimeSpan.FromMinutes(1)
};

// webSocketOptions.AllowedOrigins.Add("https://client.com");

app.UseWebSockets(webSocketOptions);

app.UseDefaultFiles();
app.UseStaticFiles();

app.MapControllers();

app.Run();