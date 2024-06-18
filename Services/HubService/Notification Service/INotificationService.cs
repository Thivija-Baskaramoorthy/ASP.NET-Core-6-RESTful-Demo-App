using WebAppDemo.DTOs.Resposes;

namespace WebAppDemo.Services.HubService
{
    public interface INotificationService 
    {
        Task SendNotificationOnSubjectCreated();
       
        public  Task SendAll();

        public  Task SendOneClient(string clientId);

    }
}
