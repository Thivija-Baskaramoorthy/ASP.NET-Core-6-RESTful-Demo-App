using WebAppDemo.DTOs;
using WebAppDemo.DTOs.Requests;
using WebAppDemo.DTOs.Resposes;
using WebAppDemo.Models;

namespace WebAppDemo.Services.StudentService
{
    public class StudentService : IStudentService

    {
        private readonly ApplicationDbContext context;

        public StudentService(ApplicationDbContext applicationDbContext)
        {
            context = applicationDbContext;
        }


        public BaseResponse CreateStudent(CreateStudentRequest request)
        {
            BaseResponse response;

            try
            {
                StudentModel newStudent = new StudentModel
                {
                    first_name = request.first_name,
                    last_name = request.last_name,
                    address = request.address,
                    email = request.email,
                    contact_number = request.contact_number
                };

                context.Add(newStudent);
                context.SaveChanges();

                response = new BaseResponse
                {
                    status_code = StatusCodes.Status200OK,
                    data = new { message = "Successfully created the new student" }
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

        public BaseResponse StudentList()
        {
            BaseResponse response;

            try
            {
                List<StudentDTO> students = new List<StudentDTO>();

                foreach (var student in context.Students.ToList())
                {
                    students.Add(new StudentDTO
                    {
                        student_id = student.id,
                        first_name = student.first_name,
                        last_name = student.last_name,
                        address = student.address,
                        email = student.email,
                        contact_number = student.contact_number
                    });
                }

                response = new BaseResponse
                {
                    status_code = StatusCodes.Status200OK,
                    data = new { students }
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

        public BaseResponse GetStudentById(long student_id)
        {
            BaseResponse response;

            try
            {
                StudentDTO student = new StudentDTO();

                using (context)
                {
                    StudentModel filteredStudent = context.Students.Where(student => student.id == student_id).FirstOrDefault();

                    if (filteredStudent != null)
                    {
                        student.student_id = filteredStudent.id;
                        student.first_name = filteredStudent.first_name;
                        student.last_name = filteredStudent.last_name;
                        student.address = filteredStudent.address;
                        student.email = filteredStudent.email;
                        student.contact_number = filteredStudent.contact_number;
                    }
                    else
                    {
                        student = null;
                    }
                }

                if (student != null)
                {
                    response = new BaseResponse
                    {
                        status_code = StatusCodes.Status200OK,
                        data = new { student }
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

        public BaseResponse UpdateStudentById(long student_id, UpdateStudentRequest request)
        {
            BaseResponse response;

            try
            {
                using (context)
                {
                    StudentModel filteredStudent = context.Students.Where(students => students.id == student_id).FirstOrDefault();

                    if (filteredStudent != null)
                    {
                        filteredStudent.first_name = request.first_name;
                        filteredStudent.last_name = request.last_name;
                        filteredStudent.address = request.address;
                        filteredStudent.email = request.email;
                        filteredStudent.contact_number = request.contact_number;

                        context.SaveChanges();

                        response = new BaseResponse
                        {
                            status_code = StatusCodes.Status200OK,
                            data = new { message = "Student updated successfully" }
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

        public BaseResponse DeleteStudentById(long student_id)
        {
            BaseResponse response;

            try
            {
                using (context)
                {
                    StudentModel studentToDelete = context.Students.Where(students => students.id ==    student_id).FirstOrDefault();

                    if (studentToDelete != null)
                    {
                        context.Students.Remove(studentToDelete);
                        context.SaveChanges();

                        response = new BaseResponse
                        {
                            status_code = StatusCodes.Status200OK,
                            data = new { message = "Student deleted successfully" }
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