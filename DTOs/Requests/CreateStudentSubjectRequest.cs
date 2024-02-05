using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;

namespace WebAppDemo.DTOs.Requests
{
    public class CreateStudentSubjectRequest
    {

        [Required]
        public long student_id { get; set; }

        [Required]
        public long sub_id { get; set; }

        public float marks { get; set; }
        

    }
}
