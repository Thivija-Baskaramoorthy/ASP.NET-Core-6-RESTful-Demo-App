using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using System.Collections.Concurrent;
using System.Collections.Generic;
using WebAppDemo.DTOs.Resposes;

namespace WebAppDemo.Services.HubService.ClientConnectionService
{
    public class ClientConnectionService : IClientConnectionService
    {
        private readonly IHubContext<NotificationHub> hubContext;

        public ClientConnectionService(IHubContext<NotificationHub> hubNotificationContext)
        {
            hubContext = hubNotificationContext;
        }

        private readonly ConcurrentDictionary<string, string> MyClients = new ConcurrentDictionary<string, string>();

        public async Task<bool> AddOrUpdateConnectionId(string clientId, string connectionId)
        {
            if (MyClients.ContainsKey(clientId))
            {
                MyClients[clientId] = connectionId;
                return true;
            }
            else
            {
                MyClients.TryAdd(clientId, connectionId);
                return false;
            }
        }

        public async Task RemoveConnection(string clientId)
        {
            if (!string.IsNullOrEmpty(clientId))
            {
                MyClients.TryRemove(clientId, out _);
            }
        }

        public async Task<string> GetConnectionIdByClientId(string clientId)
        {
            if (MyClients.TryGetValue(clientId, out string connectionId))
            {
                return connectionId;
            }
            else
            {
                return null;
            }
        }
    }
}
