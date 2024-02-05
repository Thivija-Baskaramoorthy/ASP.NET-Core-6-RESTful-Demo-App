using System.ComponentModel.DataAnnotations;

namespace WebAppDemo.DTOs
{
    public class StudentDTO
    {
        internal long student_id;

        [Required]
        public string first_name { get; set; }

        [Required]
        public string last_name { get; set; }

        [Required]
        public string address { get; set; }

        [Required]
        public string email { get; set; }

        [Required]
        public string contact_number { get; set; }
    }
}
