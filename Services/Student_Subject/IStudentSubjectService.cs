using WebAppDemo.DTOs.Requests;
using WebAppDemo.DTOs.Resposes;


namespace WebAppDemo.Services.Student_Subject
{
    public interface IStudentSubjectService
    {
        BaseResponse CreateStudentSubject(CreateStudentSubjectRequest request);
        BaseResponse UpdateStudentSubjectMarksById(long student_id, long sub_id, UpdateStudentSubjectRequest request);
        BaseResponse GetEnrolledSubjectsbyId(long student_id);
        BaseResponse FindAverageOfAStudentbyId(long student_id);
    }
}
