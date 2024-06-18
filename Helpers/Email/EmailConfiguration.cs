namespace WebAppDemo.Helpers.Email
{
    public class EmailConfiguration
    {
        public string server { get; set; }
        public int port { get; set; }
        public string senderName { get; set; }
        public string senderEmail { get; set; }
        public string userName { get; set; }
        public string password { get; set; }
     //   public bool noAuth { get; set; }
        public bool useSSL { get; set; }
    }
}
