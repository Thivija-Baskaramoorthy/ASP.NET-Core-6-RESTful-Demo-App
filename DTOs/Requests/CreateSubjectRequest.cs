using System.ComponentModel.DataAnnotations;

namespace WebAppDemo.DTOs.Requests
{
    public class CreateSubjectRequest
    {
       
        [Required]
        public string subject_name { get; set; }

    }
}
