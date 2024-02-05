using WebAppDemo.DTOs.Requests;
using WebAppDemo.DTOs.Resposes;

namespace WebAppDemo.Services.SubjectService
{
    public interface ISubjectService
    {
        BaseResponse CreateSubject(CreateSubjectRequest request);
    }
}
