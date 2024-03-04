using Microsoft.AspNetCore.Mvc;
using WebAppDemo.DTOs.Requests;
using WebAppDemo.DTOs.Resposes;
using WebAppDemo.Services.Student_Subject;

namespace WebAppDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentSubjectController : ControllerBase
    {
        private readonly IStudentSubjectService studentSubjectService;

        // Constructor
        public StudentSubjectController(IStudentSubjectService studentSubjectService)
        {
            this.studentSubjectService = studentSubjectService;
        }


        // end points
        [HttpPost("save")]
        public BaseResponse CreateStudentSubject(CreateStudentSubjectRequest request)
        {
            return studentSubjectService.CreateStudentSubject(request);
        }

        [HttpPut("update")]
        public BaseResponse UpdateStudentSubjectMarksById(long student_id, long sub_id ,  UpdateStudentSubjectRequest request)
        {
            return studentSubjectService.UpdateStudentSubjectMarksById(student_id, sub_id, request);
        }

        [HttpGet("enrolled-subjects")]
        public BaseResponse GetEnrolledSubjectsbyId(long student_id)
        {
            return studentSubjectService.GetEnrolledSubjectsbyId(student_id);
        }

        [HttpGet("average")]
        public BaseResponse FindAverageOfAStudentbyId(long student_id)
        {
            return studentSubjectService.FindAverageOfAStudentbyId(student_id);
        }
    }
}
