using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using MimeKit;
using MimeKit.Text;
using Org.BouncyCastle.Asn1.Pkcs;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;
using WebAppDemo.DTOs;
using WebAppDemo.DTOs.Resposes;
using WebAppDemo.Helpers.Email;
using WebAppDemo.Models;
using SmtpClient = MailKit.Net.Smtp.SmtpClient;

namespace WebAppDemo.Services.Email_Service
{
    public class EmailService : IEmailService
    {
        private readonly EmailConfiguration emailConfiguration;
        public EmailService(IOptions<EmailConfiguration> emailConfig)
        {
            emailConfiguration = emailConfig.Value;
        }
        public async void SendMail(EmailDetailsDTO emailDetailsDTO)
        {
            try
            {
                using (MimeMessage emailMessage = new MimeMessage())
                {
                    // initialize new message
                    MimeMessage newMessage = new MimeMessage();
                    newMessage.Subject = emailDetailsDTO.subject;
                    newMessage.From.Add(new MailboxAddress("DEMO WEB APP", emailConfiguration.senderName));
                    newMessage.To.Add(emailDetailsDTO.recipient);

                    BodyBuilder emailBodyBuilder = new BodyBuilder();
                    emailBodyBuilder.TextBody = emailDetailsDTO.mailBody;

                    // get HTML class as email body
                    emailBodyBuilder.HtmlBody = emailDetailsDTO.mailBody;

                    newMessage.Body = emailBodyBuilder.ToMessageBody();
                    // sending message
                    using (var mailClient = new SmtpClient())
                    {
                       mailClient.Connect(emailConfiguration.server, emailConfiguration.port, emailConfiguration.useSSL);
                       mailClient.Authenticate(emailConfiguration.userName, emailConfiguration.password);
                       var response = mailClient.Send(newMessage);
                       mailClient.Disconnect(true);
                    }

                }

            }
            catch (Exception ex)
            {

                 var response = new BaseResponse
                {
                    status_code = StatusCodes.Status500InternalServerError,
                    data = new { message = "Internal server error: " + ex.Message }
                };
            }
        }

    }
}
