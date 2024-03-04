using Microsoft.AspNetCore.SignalR;
using WebAppDemo.DTOs.Requests;
using WebAppDemo.DTOs.Resposes;
// using WebAppDemo.Hubs;
using WebAppDemo.Models;
using WebAppDemo.Services.HubService;

namespace WebAppDemo.Services.SubjectService
{
    public class SubjectService : ISubjectService
    {
        private readonly ApplicationDbContext context;
        private readonly IHubContext<NotificationHub> hubContext;
        private readonly INotificationService notificationService;

        public SubjectService(ApplicationDbContext applicationDbContext, IHubContext<NotificationHub> notificationHubContext, INotificationService notificationHubService)
        {
            context = applicationDbContext;
            hubContext = notificationHubContext;
            notificationService = notificationHubService;
        }


        public async Task <BaseResponse> CreateSubject(CreateSubjectRequest request)
        {
            BaseResponse response;

            try
            {
                SubjectModel newSubject = new SubjectModel
                {
                    subject_name = request.subject_name 
                };

                context.Add(newSubject);
                await context.SaveChangesAsync();

                // Trigger notification when a new subject is created
                await notificationService.SendNotificationOnSubjectCreated();

                response = new BaseResponse
                {
                    status_code = StatusCodes.Status200OK,
                    data = new { message = "Successfully created the new subject" }
                };
                
            }
            catch (Exception ex)
            {
                response = new BaseResponse
                {
                    status_code = StatusCodes.Status500InternalServerError,
                    data = new { message = "Internal server error: " + ex.Message }
                };
            }

            return response;
        }
    }
}
