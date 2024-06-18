using MimeKit;
using Org.BouncyCastle.Asn1.X509;

namespace WebAppDemo.DTOs
{
    public class EmailDetailsDTO
    {
        public MailboxAddress recipient { get; set; }
        
        public  string subject { get; set; }

        public string body { get; set; }

        public string mailBody { get; set; }
       
    }
}
