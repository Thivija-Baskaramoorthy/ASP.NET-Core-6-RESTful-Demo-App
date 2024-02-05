using Microsoft.AspNetCore.Mvc;
using WebAppDemo.DTOs.Requests;
using WebAppDemo.DTOs.Resposes;
using WebAppDemo.Services.Student_Subject;
using WebAppDemo.Services.StudentService;

namespace WebAppDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IStudentService studentService;

        // Constructor
        public StudentController(IStudentService studentService)
        {
            this.studentService = studentService;
        }


        // end points
        [HttpPost("save")]
        public BaseResponse CreateStudent(CreateStudentRequest request)
        {
            return studentService.CreateStudent(request);
        }

        [HttpGet("list")]
        public BaseResponse StudentList()
        {
            return studentService.StudentList();
        }

        [HttpGet("{id}")]
        public BaseResponse GetStudentById(long id)
        {
            return studentService.GetStudentById(id);
        }

        [HttpPut("{id}")]
        public BaseResponse UpdateStudentById(long id, UpdateStudentRequest request)
        {
            return studentService.UpdateStudentById(id,request );
        }

        [HttpDelete("{id}")]
        public BaseResponse DeleteStudentById(long id)
        {
            return studentService.DeleteStudentById(id);
        }

      
    }
}
