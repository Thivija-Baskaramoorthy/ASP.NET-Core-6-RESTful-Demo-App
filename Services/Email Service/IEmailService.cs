using Org.BouncyCastle.Asn1.Pkcs;
using WebAppDemo.DTOs;
using WebAppDemo.DTOs.Resposes;
using WebAppDemo.Models;

namespace WebAppDemo.Services.Email_Service
{
    public interface IEmailService
    {
        public  void SendMail(EmailDetailsDTO emailDetailsDTO);

    }
}
