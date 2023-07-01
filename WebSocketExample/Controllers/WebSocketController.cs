using System.Globalization;
using System.Net.WebSockets;
using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using WebSocketExample.Models;

namespace WebSocketExample.Controllers;

[ApiController]
[Route("[controller]")]
public class WebSocketController : ControllerBase
{
    private readonly ILogger<WebSocketController> _logger;

    public WebSocketController(ILogger<WebSocketController> logger)
    {
        _logger = logger;
    }

    [Route("/ws")]
    public async Task Get()
    {
        if (HttpContext.WebSockets.IsWebSocketRequest)
        {
            using var webSocket = await HttpContext.WebSockets.AcceptWebSocketAsync();
            await GetData(webSocket);
        }
        else
            HttpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
    }

    private async Task GetData(WebSocket webSocket)
    {
        var buffer = new byte[1024 * 4];
        //TODO: При закрытии сокета со стороны клиента по факту продолжается выполнение логики.
        var receiveResult = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
        while (!receiveResult.CloseStatus.HasValue)
        {
            var time = await GetTime();
            
            var strContet = time.ToString(CultureInfo.InvariantCulture);
            var bytes = Encoding.UTF8.GetBytes(strContet);

            await webSocket.SendAsync(
                new ArraySegment<byte>(bytes),
                WebSocketMessageType.Text,
                true,
                CancellationToken.None);

            await Task.Delay(TimeSpan.FromSeconds(5));
        }

        await webSocket.CloseAsync(
            receiveResult.CloseStatus.Value, 
            receiveResult.CloseStatusDescription,
            CancellationToken.None);
    }

    private async Task<DateTime> GetTime()
    {
        using var httpClient = new HttpClient();
        httpClient.BaseAddress = new Uri("https://worldtimeapi.org/api/");
        var result = await httpClient.GetAsync(new Uri("timezone/Europe/Moscow", UriKind.Relative));
        Console.WriteLine(result.StatusCode);
        var strContent = await result.Content.ReadAsStringAsync();
        var worldTime = JsonSerializer.Deserialize<WorldTime>(strContent);
        
        return worldTime.UtcDatetime;
    }
}