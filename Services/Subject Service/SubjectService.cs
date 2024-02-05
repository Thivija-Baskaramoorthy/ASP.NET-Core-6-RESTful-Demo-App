using WebAppDemo.DTOs.Requests;
using WebAppDemo.DTOs.Resposes;
using WebAppDemo.Models;

namespace WebAppDemo.Services.SubjectService
{
    public class SubjectService : ISubjectService
    {
        private readonly ApplicationDbContext context;

        public SubjectService(ApplicationDbContext applicationDbContext)
        {
            context = applicationDbContext;
        }


        public BaseResponse CreateSubject(CreateSubjectRequest request)
        {
            BaseResponse response;

            try
            {
                SubjectModel newSubject = new SubjectModel
                {
                    subject_name = request.subject_name 
                 };

                context.Add(newSubject);
                context.SaveChanges();

                response = new BaseResponse
                {
                    status_code = StatusCodes.Status200OK,
                    data = new { message = "Successfully created the new subject" }
                };
                
            }
            catch (Exception ex)
            {
                response = new BaseResponse
                {
                    status_code = StatusCodes.Status500InternalServerError,
                    data = new { message = "Internal server error: " + ex.Message }
                };
            }

            return response;
        }
    }
}
