using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task.Model
{
    public class Teacher
    {
        public int TeacherId { get; set; }
        public string TeacherName { get; set; }
        public int TeacherSalary { get; set; }
        public string TeacherAddress { get; set; }
        public string TeacherPhoneNumber { get; set; }
        public virtual ICollection<TeacherStudent> TeacherStudents { get; set; }//navigation (many-to-many)
        public virtual ICollection<TeacherCourse> TeacherCourses { get;set; }//navigation (many-to-many)
    }
}
