using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task.Model
{
    public class Course
    {
        public int CourseID { get; set; }   
        public string CourseName { get; set; }
        public virtual Grade Grade { get; set; }
        public virtual ICollection<StudentCourse> StudentsCourse { get; set; }// navigation (many-many)
        public virtual ICollection<TeacherCourse> TeachersCourse { get; set; }//navigation
    }
}
