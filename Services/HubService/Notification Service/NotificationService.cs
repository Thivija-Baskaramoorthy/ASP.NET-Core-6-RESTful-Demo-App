using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using WebAppDemo.DTOs.Resposes;
using WebAppDemo.Services.HubService.ClientConnectionService;
// using WebAppDemo.Hubs;

namespace WebAppDemo.Services.HubService
{
    public class NotificationService : INotificationService
    {

        private readonly IHubContext<NotificationHub> hubContext;
        private IClientConnectionService _clientConnectionService;


        public NotificationService(IHubContext<NotificationHub> hubNotificationContext, IClientConnectionService clientConnectionService)
        {
            hubContext = hubNotificationContext;
            _clientConnectionService = clientConnectionService;

        }

        public async Task SendNotificationOnSubjectCreated()
        {
            BaseHub<string> notificationOnSubjectCreated = new BaseHub<string>();
            notificationOnSubjectCreated.notification = "New Subject Created";
            await hubContext.Clients.All.SendAsync("SendNotification",notificationOnSubjectCreated.notification);
            
        }

        public async Task SendAll()
        {
            BaseHub<string> notificationToSendAll = new BaseHub<string>();
            notificationToSendAll.notification = "Connection Established";
            await hubContext.Clients.All.SendAsync("SendNotification", notificationToSendAll.notification);
        }

        public async Task SendOneClient( string clientId)
        {
            BaseHub<string> notificationToSendOne = new BaseHub<string>();
            notificationToSendOne.notification = "Hello User";
            string connectionId = await _clientConnectionService.GetConnectionIdByClientId(clientId);
            if(connectionId != null)
            {
                await hubContext.Clients.Client(connectionId).SendAsync("SendNotification", notificationToSendOne.notification);
            }
            
        }

    }
}
    

