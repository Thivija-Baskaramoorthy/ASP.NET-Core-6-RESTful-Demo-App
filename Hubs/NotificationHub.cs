using Microsoft.AspNetCore.SignalR;
using System.Collections.Concurrent;
using System.Runtime.CompilerServices;
using WebAppDemo.Services.ClientConnectionService;
using WebAppDemo.Services.HubService;

public class NotificationHub : Hub
{
    private INotificationService _notificationService;
    private IClientConnectionService _clientConnectionService;

    public NotificationHub(INotificationService notificationService, IClientConnectionService clientConnectionService)
    {
        _notificationService = notificationService;
        _clientConnectionService = clientConnectionService;
    }

    public ConcurrentDictionary<string, string> MyClients = new ConcurrentDictionary<string, string>();


    public async override Task OnConnectedAsync()
    {
        string clientId = Context.GetHttpContext().Request.Query["clientId"];
        string connectionId = Context.ConnectionId;

        Console.WriteLine("ClientID: "+clientId);
        Console.WriteLine("ConnectionID: " + connectionId);
        await _clientConnectionService.AddOrUpdateConnectionId(clientId, connectionId);
        await _clientConnectionService.GetConnectionIdByClientId(clientId);

        await _notificationService.SendOneClient("thivija");
        
        await base.OnConnectedAsync();

        
    }

    public override async Task OnDisconnectedAsync(Exception exception)
    {
        string clientId = Context.GetHttpContext().Request.Query["clientId"];

        await _clientConnectionService.RemoveConnection(clientId);

        await base.OnDisconnectedAsync(exception);
    }
}