using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAppDemo.DTOs.Requests;
using WebAppDemo.DTOs.Resposes;
using WebAppDemo.Services.Email_Service;
using WebAppDemo.Services.StudentService;
using WebAppDemo.Services.SubjectService;

namespace WebAppDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubjectController : ControllerBase
    {
        private readonly ISubjectService subjectService;

        // Constructor
        public SubjectController(ISubjectService subjectService)
        {
            this.subjectService = subjectService;
        }


        // end points
        [HttpPost("save")]
        public Task <BaseResponse> CreateSubject(CreateSubjectRequest request)
        {
            return subjectService.CreateSubject(request);
        }


        [HttpPost("test-SendEmail")]
        public BaseResponse SendTestMail()
        {
            return subjectService.SendTestMail();
        }

    }
}
