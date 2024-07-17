using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task.Model
{
    public class Student
    {
        public int StudentId { get; set; }
        public string StudentName { get; set; }
        public float StudentGpa { get; set; }
        public string StudenAddress { get; set; }
        public string StudentPhone { get; set; }
        public virtual ICollection<TeacherStudent> TeacherStudents { get; set; }//navigation (many-to-many)
        public virtual ICollection<StudentCourse> StudentCourses { get; set; }//navigation (many-to-many)
    }
}
