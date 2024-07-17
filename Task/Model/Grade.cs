using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task.Model
{
    public class Grade
    {
        public int GradeID { get; set; }
        [StringLength(3)]
        public string GradeValue { get; set; }
        public int CourseID { get; set; }
        public virtual Course Course { get; set;}
    }
}
