using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAppDemo.Models
{
    public class Student_SubjectModel
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
      
        public long id { get; set; }

        [Required]
        public long student_id { get; set; }

        [Required] 
        public long sub_id { get; set; }

        public float marks { get; set; } 


    }
}
