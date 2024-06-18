using Microsoft.AspNetCore.SignalR;
using MimeKit;
using WebAppDemo.DTOs;
using WebAppDemo.DTOs.Requests;
using WebAppDemo.DTOs.Resposes;
using WebAppDemo.Helpers.Email;

// using WebAppDemo.Hubs;
using WebAppDemo.Models;
using WebAppDemo.Services.Email_Service;
using WebAppDemo.Services.HubService;
using WebAppDemo.Services.StudentService;

namespace WebAppDemo.Services.SubjectService
{
    public class SubjectService : ISubjectService
    {
        private readonly ApplicationDbContext context;
        private readonly IHubContext<NotificationHub> hubContext;
        private readonly INotificationService notificationService;
        private readonly IEmailService emailService;
        private readonly IStudentService studentService;

        public SubjectService(ApplicationDbContext applicationDbContext, 
            IHubContext<NotificationHub> notificationHubContext, 
            INotificationService notificationHubService, 
            IEmailService testEmailService)
        {
            context = applicationDbContext;
            hubContext = notificationHubContext;
            notificationService = notificationHubService;
            emailService = testEmailService;
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
               // await notificationService.SendNotificationOnSubjectCreated();

                // send email when new subject is created 
                /*EmailDetailsDTO emailDetails = new EmailDetailsDTO
                {
                    subject = "New Subject",
                    recipient = new MailboxAddress("Thivi", "thivija27@yahoo.com"),
                    mailBody = "New Subject Created"

                };*/
                EmailDetailsDTO emailDetails = new EmailDetailsDTO
                {
                    subject =  "NEW SUBJECT" + " | " + "MOBILE APP DEVELOPMENT",
                    recipient = new MailboxAddress("Kaji", "thivija27@yahoo.com"),
                    mailBody = EmailTemplateProvider.SendEmailOnSubjectCreated("Thivija"),
                };       
                emailService.SendMail(emailDetails);


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


        public BaseResponse SendTestMail()
        {
            BaseResponse response;
            try
            {
                EmailDetailsDTO emailDetails = new EmailDetailsDTO
                {
                    subject = "Test",
                    recipient = new MailboxAddress("Thivi", "thivija27@yahoo.com"),
                    //mailBody = "This is a test Mail"

                };

                emailService.SendMail(emailDetails);

                response = new BaseResponse
                {
                    status_code = StatusCodes.Status200OK,
                    data = new { message = "Email sent successfully" }
                };


                return response;

            }
            catch (Exception ex)
            {

                response = new BaseResponse
                {
                    status_code = StatusCodes.Status500InternalServerError,
                    data = new { message = "Failed to send the email" }
                };

                return response;

            }
        }
    }
}
