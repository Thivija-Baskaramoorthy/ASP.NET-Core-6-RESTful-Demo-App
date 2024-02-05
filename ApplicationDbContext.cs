using WebAppDemo.Models;
using Microsoft.EntityFrameworkCore;

namespace WebAppDemo
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options ) : base (options) { }

        public  virtual DbSet<StudentModel> Students { get; set; }
        public virtual DbSet <SubjectModel> Subjects { get; set; }
        public virtual DbSet<Student_SubjectModel> Student_Subject { get; set; }
    }
}
