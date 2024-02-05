using WebAppDemo.DTOs.Requests;
using WebAppDemo.Models;
using WebAppDemo.DTOs;
using WebAppDemo.DTOs.Resposes;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace WebAppDemo.Services.Student_Subject
{
    public class StudentSubjectService : IStudentSubjectService
    {
        private readonly ApplicationDbContext context;

        public StudentSubjectService(ApplicationDbContext applicationDbContext)
        {
            context = applicationDbContext;
        }

        public BaseResponse CreateStudentSubject(CreateStudentSubjectRequest request)
        {
            BaseResponse response;

            try
            {
                StudentModel existingStudent = context.Students.Find(request.student_id);
                SubjectModel existingSubject = context.Subjects.Find(request.sub_id);

                if (existingSubject == null && existingStudent == null)
                {
                    response = new BaseResponse
                    {
                        status_code = StatusCodes.Status400BadRequest,
                        data = new { Message = "Both Subject and Student Not Found" }
                    };
                    return response;
                }

                if (existingStudent == null)
                {
                    response = new BaseResponse
                    {
                        status_code = StatusCodes.Status400BadRequest,
                        data = new { message = "Student Not Found" }
                    };
                    return response;
                }

                if (existingSubject == null)
                {
                    response = new BaseResponse
                    {
                        status_code = StatusCodes.Status400BadRequest,
                        data = new { Message = "Subject Not Found" }
                    };
                    return response;
                }

                // Create a new Student_SubjectModel
                Student_SubjectModel newStudentSubject = new Student_SubjectModel
                {
                    sub_id = request.sub_id,
                    student_id = request.student_id
                };

                // Add and save changes
                context.Add(newStudentSubject);
                context.SaveChanges();

                response = new BaseResponse
                {
                    status_code = StatusCodes.Status200OK,
                    data = new { message = "User Successfully enrolled" }
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

        public BaseResponse UpdateStudentSubjectMarksById(long student_id, long sub_id, UpdateStudentSubjectRequest request)
        {
            BaseResponse response;

            try
            {
                using (context)
                {
                    Student_SubjectModel filteredStudentSubject = context.Student_Subject.
                        Where(Student_Subject => Student_Subject.student_id == student_id && Student_Subject.sub_id== sub_id).FirstOrDefault();

                    if (filteredStudentSubject != null)
                    {
                        filteredStudentSubject.marks = request.marks;

                        context.SaveChanges();

                        response = new BaseResponse
                        {
                            status_code = StatusCodes.Status200OK,
                            data = new { message = "Student's Marks updated successfully" }
                        };
                    }
                    else
                    {
                        response = new BaseResponse
                        {
                            status_code = StatusCodes.Status400BadRequest,
                            data = new { message = "No Details found" }
                        };
                    }
                }
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

        public BaseResponse GetEnrolledSubjectsbyId(long student_id)
        {
            BaseResponse response;

            try
            {

                List<StudentSubjectDTO> enrolledSubjects = new List<StudentSubjectDTO>();

                using (context)
                {
                    // query to join the tables and retrieve the desired columns
                    var query = from Student_Subject in context.Student_Subject
                                join Subjects in context.Subjects on Student_Subject.sub_id equals Subjects.id
                                where Student_Subject.student_id == student_id
                                select new StudentSubjectDTO
                                {
                                    sub_id = Subjects.id,
                                    subject_name = Subjects.subject_name,
                                    marks= Student_Subject.marks

                                };
                    enrolledSubjects = query.ToList();
                }

                if (enrolledSubjects.Any())
                {
                    response = new BaseResponse
                    {
                        status_code = StatusCodes.Status200OK,
                        data = new { enrolledSubjects }
                    };
                }
                else
                {
                    response = new BaseResponse
                    {
                        status_code = StatusCodes.Status400BadRequest,
                        data = new { message = "No student found" }
                    };

                }
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

        public BaseResponse FindAverageOfAStudentbyId(long student_id)
        {
            BaseResponse response;

            try
            {
                using (context)
                {
                    StudentModel existingStudent = context.Students.Find(student_id);
                    if (existingStudent == null)
                    {
                        response = new BaseResponse
                        {
                            status_code = StatusCodes.Status400BadRequest,
                            data = new { message = "Student has not enrolled to this subject " }
                        };
                        return response;
                    }
                    var query = from StudentSubject in context.Student_Subject
                                join subject in context.Subjects on StudentSubject.sub_id equals subject.id
                                where StudentSubject.student_id == student_id
                                select new
                                {
                                    StudentSubject.marks
                                };

                    var subjectsAndMarks = query.ToList();

                    if (subjectsAndMarks.Any())
                    {
                        double averageMarks = subjectsAndMarks.Average(x => x.marks);

                        response = new BaseResponse
                        {
                            status_code = StatusCodes.Status200OK,
                            data = new { averageMarks }
                        };
                    }
                    else
                    {
                        response = new BaseResponse
                        {
                            status_code = StatusCodes.Status400BadRequest,
                            data = new { message = "No subjects found for the specified student" }
                        };
                    }
                }
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


