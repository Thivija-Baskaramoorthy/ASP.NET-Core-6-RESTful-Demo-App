using System.ComponentModel.DataAnnotations;

namespace WebAppDemo.DTOs
{
    public class SubjectDTO
    {
        
        internal long sub_id { get; set; }

        [Required]
        public string subject_name { get; set; }
    }
}
