using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task.Model;

namespace Task.Data
{
    public class AppDbContext:DbContext
    {
        public DbSet<Student> Students { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Grade> Grades { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<TeacherStudent> TeacherStudent { get; set; }
        public DbSet<TeacherCourse> TeacherCourse { get; set;}
        public DbSet<StudentCourse> StudentCourse { get; set; }
        public string ConnectionString { get; }
        public AppDbContext()
        {
            ConnectionString = "Data Source=DESKTOP-A2HB7G2;Initial Catalog=School;Integrated Security=True";
        }

        protected override void  OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies().UseSqlServer(ConnectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //many to many for teacher student
            modelBuilder.Entity<TeacherStudent>()
                .HasKey(ep => new { ep.StudentID, ep.TeacherID });
            modelBuilder.Entity<TeacherStudent>()
                .HasOne(ep => ep.Teacher)
                .WithMany(e => e.TeacherStudents)
                .HasForeignKey(ep => ep.TeacherID);
            modelBuilder.Entity<TeacherStudent>()
                .HasOne(ep => ep.Student)
                .WithMany(e => e.TeacherStudents)
                .HasForeignKey(ep => ep.StudentID);

            //many to many for teacher course
            modelBuilder.Entity<TeacherCourse>()
                .HasKey(ep => new { ep.TeacherID, ep.CourseID });
            modelBuilder.Entity<TeacherCourse>()
                .HasOne(ep => ep.Teacher)
                .WithMany(ep => ep.TeacherCourses)
                .HasForeignKey(ep => ep.TeacherID);
            modelBuilder.Entity<TeacherCourse>()
                .HasOne(ep=>ep.Course)
                .WithMany(ep=>ep.TeachersCourse)
                .HasForeignKey(ep=>ep.CourseID);

            //many to many for student and course
            modelBuilder.Entity<StudentCourse>()
                .HasKey(ep => new {ep.StudentID, ep.CourseID});
            modelBuilder.Entity<StudentCourse>()
                .HasOne(ep => ep.Student)
                .WithMany(ep => ep.StudentCourses)
                .HasForeignKey(ep => ep.StudentID);
            modelBuilder.Entity<StudentCourse>()
                .HasOne(ep => ep.Course)
                .WithMany(ep => ep.StudentsCourse)
                .HasForeignKey(ep => ep.CourseID);
        }

    }
}
