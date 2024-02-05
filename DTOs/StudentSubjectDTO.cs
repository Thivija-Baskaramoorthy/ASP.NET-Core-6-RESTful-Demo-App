using System.ComponentModel.DataAnnotations;

namespace WebAppDemo.DTOs
{
    public class StudentSubjectDTO
    {
        [Required]
        internal long student_id { get; set; }

        [Required]
        public long sub_id { get; set; }

        [Required]
        public string subject_name { get; set; }

        [Required]
        public float marks { get; set;}
    }
}
