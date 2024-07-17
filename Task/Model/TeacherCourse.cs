using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task.Model
{
    public class TeacherCourse
    {
        public int TeacherID { get; set; } //foreign key
        public virtual Teacher Teacher { get; set; } //navigation
        public int CourseID { get; set; }//foreign key
        public virtual Course Course { get; set;}//navigation
    }
}
