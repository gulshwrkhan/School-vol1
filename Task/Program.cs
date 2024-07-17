

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Task.Data;
using Task.Model;

namespace Task
{
    class Task1
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("\t\t\t\t Main Menu\n");
                Console.WriteLine("Which database you want to use: ");
                Console.WriteLine("1.Student");
                Console.WriteLine("2.Teacher");
                Console.WriteLine("3.Courses");
                Console.WriteLine("4.Exit");
                Console.Write("Enter Your Options: ");
                char option = char.Parse(Console.ReadLine());
                if (option == '1')
                {
                    StudentFunc();
                }
                else if (option == '2')
                {
                    TeacherFunc();
                }
                else if (option == '3')
                {
                    CourseFunc();
                }
                else if (option == '4')
                {
                    System.Environment.Exit(0);
                }
                else
                {
                    Console.WriteLine("Please Write Correct Options........");
                    Console.ReadKey();
                }
            }

        }

        static void ATeacherCourse()
        {
            while(true)
            {
                Console.Clear();
                using (var context=new AppDbContext())
                {
                    var st = context.Teachers.ToList();
                    foreach (var i in st)
                    {
                        Console.WriteLine("Teacher's id " + i.TeacherId + " : " + i.TeacherName);
                    }
                    Console.Write("Which Teacher you want to assign Course: ");
                    int Tid = int.Parse(Console.ReadLine());

                    var t = context.Courses.ToList();
                    foreach (var i in t)
                    {
                        Console.WriteLine("Course's id " + i.CourseID + " : " + i.CourseName);
                    }
                    Console.Write("Which Course you want to be assigned to teacher: ");
                    int Cid = int.Parse(Console.ReadLine());

                    var intput = new TeacherCourse();
                    intput.TeacherID = Tid;
                    intput.CourseID = Cid;

                    context.TeacherCourse.Add(intput);
                    context.SaveChanges();

                    Console.WriteLine("Teacher assigned Successfully. Do you want to assign more teachers(y/*):");
                    char option = char.Parse(Console.ReadLine());
                    if(option=='Y'||option=='y')
                    {
                        continue;
                    }
                    else 
                    {
                        break;
                    }
                }
            }
        }
        static void StudentFunc()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("\t\t\t\t Student Menu");
                Console.WriteLine("What functionality you want to do with the database: ");
                Console.WriteLine("1.Add. ");
                Console.WriteLine("2.Remove. ");
                Console.WriteLine("3.List All Students. ");
                Console.WriteLine("4.Find One Student.");
                Console.WriteLine("5.Update. ");
                Console.WriteLine("6.Register Course.");
                Console.WriteLine("7.Main Menu. ");
                Console.Write("\t Enter your option: ");
                char option = char.Parse(Console.ReadLine());
                if (option == '1')
                {
                    AddMember();
                }
                else if (option == '2')
                {
                    RemoveMember();
                }
                else if (option == '3')
                {
                    ListMembers();
                }
                else if (option == '4')
                {
                    FindMember();
                }
                else if (option == '5')
                {
                    UpdateMember();
                }
                else if (option == '6')
                {
                    AStudentCourse();
                }
                else if(option=='7')
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Wrong input. PLease put correct input.......");
                    Console.ReadKey();
                }
            }
        }

        static void AStudentCourse()
        {
            while (true)
            {
                Console.Clear();
                using (var context = new AppDbContext())
                {
                    var st = context.Students.ToList();
                    foreach (var i in st)
                    {
                        Console.WriteLine("Student's id " + i.StudentId + " : " + i.StudentName);
                    }
                    Console.Write("Which Student you want to register Course: ");
                    int Tid = int.Parse(Console.ReadLine());

                    var t = context.Courses.ToList();
                    foreach (var i in t)
                    {
                        Console.WriteLine("Course's id " + i.CourseID + " : " + i.CourseName);
                    }
                    Console.Write("Which Course you want to be assigned to Student: ");
                    int Cid = int.Parse(Console.ReadLine());

                    var intput = new StudentCourse();
                    intput.StudentID = Tid;
                    intput.CourseID = Cid;

                    context.StudentCourse.Add(intput);
                    context.SaveChanges();

                    Console.WriteLine("Student assigned Successfully. Do you want to register more students(y/*):");
                    char option = char.Parse(Console.ReadLine());
                    if (option == 'Y' || option == 'y')
                    {
                        continue;
                    }
                    else
                    {
                        break;
                    }
                }
            }
        }

        static void AddMember()
        {
            using (var context = new AppDbContext())
            {
                var st = new Student();
                Console.Write("Enter Student's name: ");
                st.StudentName = Console.ReadLine();
                Console.Write("Enter Student's Gpa: ");
                st.StudentGpa = float.Parse(Console.ReadLine());
                Console.Write("Enter Student's Address: ");
                st.StudenAddress = Console.ReadLine();
                Console.Write("Enter Student's Number: ");
                st.StudentPhone = Console.ReadLine();

                context.Students.Add(st);
                context.SaveChanges();
            }
        }

        static void RemoveMember()
        {
            using (var context = new AppDbContext())
            {
                var st = context.Students.ToList();
                foreach (var i in st)
                {
                    Console.WriteLine("Student id " + i.StudentId + " : " + i.StudentName);
                }
                Console.Write("Which Student you want to delete: ");
                int id = int.Parse(Console.ReadLine());
                var man = context.Students.Single(e => e.StudentId == id);
                context.Students.Remove(man);
                context.SaveChanges();
            }
        }

        static void ListMembers()
        {
            using (var context = new AppDbContext())
            {
                var st = context.Students.ToList();
                foreach (var i in st)
                {
                    Console.WriteLine("Student's id: " + i.StudentId);
                    Console.WriteLine("Student's Name: " + i.StudentName);
                    Console.WriteLine("Student's Gpa: " + i.StudentGpa);
                    Console.WriteLine("Student's Adress: " + i.StudenAddress);
                    Console.WriteLine("Student's PhoneNumber: " + i.StudentPhone);
                    var courses = context.Students
                        .Where(e => e.StudentId == i.StudentId)
                        .SelectMany(s => s.StudentCourses)
                        .Select(sc => sc.Course)
                        .ToList();


                    
                    foreach (var course in courses)
                    {
                        var gr = context.Grades.ToList();
                        foreach (var t in gr)
                        {
                            if (course.CourseID == t.CourseID)
                            {
                                
                                Console.WriteLine("Student's Courses : "  + course.CourseName+ " \tGrade :"+ t.GradeValue);
                                
                            }
                            else
                            {
                                
                                Console.WriteLine("Student's Courses : " + course.CourseName + "\t Grade: (no grade Assigned)");
                                
                            }
                        }
                    }
                    var teacher = context.Students
                        .Where(e => e.StudentId == i.StudentId)
                        .SelectMany(s => s.TeacherStudents)
                        .Select(sc => sc.Teacher)
                        .ToList();
                    foreach (var t in teacher)
                    {
                        
                        Console.WriteLine("Student's Teachers : "  + t.TeacherName);
                        
                    }
                    Console.WriteLine("=======================================");
                }
                Console.ReadKey();
            }
        }

        static void FindMember()
        {
            using (var context = new AppDbContext())
            {
                Console.Write("Enter the student id to retreive: ");
                int id = int.Parse(Console.ReadLine());
                var i = context.Students.Single(i => i.StudentId == id);
                Console.WriteLine("Student's id: " + i.StudentId);
                Console.WriteLine("Student's Name: " + i.StudentName);
                Console.WriteLine("Student's Gpa: " + i.StudentGpa);
                Console.WriteLine("Student's Adress: " + i.StudenAddress);
                Console.WriteLine("Student's PhoneNumber: " + i.StudentPhone);
                var courses = context.Students
                    .Where(e => e.StudentId == i.StudentId)
                    .SelectMany(s => s.StudentCourses)
                    .Select(sc => sc.Course)
                    .ToList();
                foreach (var course in courses)
                {
                    var gr = context.Grades.ToList();
                    foreach (var t in gr)
                    {
                        if (course.CourseID == t.CourseID)
                        {
                            int n = 1;
                            Console.WriteLine("Student's Courses : " + n + course.CourseName + " \tGrade :" + t.GradeValue);
                            n++;
                        }
                        else
                        {
                            int n = 1;
                            Console.WriteLine("Student's Courses : " + n + course.CourseName + "\t Grade: (no grade Assigned)");
                            n++;
                        }
                    }
                }
                var teacher = context.Students
                    .Where(e => e.StudentId == i.StudentId)
                    .SelectMany(s => s.TeacherStudents)
                    .Select(sc => sc.Teacher)
                    .ToList();
                foreach (var t in teacher)
                {
                    int n = 1;
                    Console.WriteLine("Student's Teachers : " + n + t.TeacherName);
                    n++;
                }
            }
            Console.ReadKey();

        }

        static void UpdateMember()
        {
            using (var context = new AppDbContext())
            {
                var st = context.Students.ToList();
                foreach (var i in st)
                {
                    Console.WriteLine("Student id " + i.StudentId + " : " + i.StudentName);
                }
                Console.Write("Which Student you want to update: ");
                int id = int.Parse(Console.ReadLine());
                var man = context.Students.Single(i => i.StudentId == id);
                Console.Write("Enter Student's name: ");
                man.StudentName = Console.ReadLine();
                Console.Write("Enter Student's Gpa: ");
                man.StudentGpa = float.Parse(Console.ReadLine());
                Console.Write("Enter Student's Address: ");
                man.StudenAddress = Console.ReadLine();
                Console.Write("Enter Student's Number: ");
                man.StudentPhone = Console.ReadLine();

                context.SaveChanges();
            }
        }

        static void TeacherFunc()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("\t\t\t\t Teacher Menu");
                Console.WriteLine("What functionality you want to do with the database: ");
                Console.WriteLine("1.Add. ");
                Console.WriteLine("2.Remove. ");
                Console.WriteLine("3.List All Teacher. ");
                Console.WriteLine("4.Find One Teacher.");
                Console.WriteLine("5.Update. ");
                Console.WriteLine("6.Assign Teacher to Course.");
                Console.WriteLine("7.Assign Teacher to Students.");
                Console.WriteLine("8.Add Grade.");
                Console.WriteLine("9.Main Menu. ");
                Console.Write("\t Enter your option: ");
                char option = char.Parse(Console.ReadLine());
                if (option == '1')
                {
                    AddTeacher();
                }
                else if (option == '2')
                {
                    RemoveTeacher();
                }
                else if (option == '3')
                {
                    ListTeachers();
                }
                else if (option == '4')
                {
                    FindTeacher();
                }
                else if (option == '5')
                {
                    UpdateTeacher();
                }
                else if (option == '6')
                {
                    ATeacherCourse();
                }
                else if(option=='7')
                {
                    ATeacherStudent();
                }
                else if(option=='8')
                {
                    AGrade();
                }
                else if(option=='9')
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Wrong input. Please Enter Again......");
                    Console.ReadKey();
                }
            }
        }

        static void AGrade()
        {
            using(var context=new AppDbContext())
            {
                Console.Write("Please Enter your Teacher's ID: ");
                int id=int.Parse(Console.ReadLine());
                try
                {
                    var tID = context.TeacherCourse.ToList();
                    foreach (var i in tID)
                    {
                        if (i.TeacherID == id)
                        {
                            var gr = new Grade();
                            gr.CourseID = i.CourseID;
                            Console.Write("Please Enter the Grade For " + i.Course.CourseName + " : ");
                            gr.GradeValue = Console.ReadLine();

                            context.Grades.Add(gr);
                            context.SaveChanges();
                        }
                        else
                        {
                            Console.WriteLine("Not right id. Please Enter again.....");
                            Console.ReadKey();
                        }
                    }
                } catch(Exception e) { Console.WriteLine(e.ToString()); }
            }
        }

        static void ATeacherStudent()
        {
            Console.Clear();
            using (var context = new AppDbContext())
            {
                var t = context.TeacherCourse.AsNoTracking().ToList(); 
                foreach (var i in t)
                {
                    var st = context.StudentCourse.AsNoTracking().Where(e => e.CourseID == i.CourseID).ToList();
                    foreach (var j in st)
                    {
                        if (j.CourseID == i.CourseID)
                        {
                            var exists = context.TeacherStudent
                                .AsNoTracking()
                                .Any(l => l.StudentID == j.StudentID && l.TeacherID == i.TeacherID);

                            if (!exists)
                            {
                                var input = new TeacherStudent
                                {
                                    StudentID = j.StudentID,
                                    TeacherID = i.TeacherID
                                };

                                context.TeacherStudent.Add(input);
                                context.SaveChanges();
                            }
                        }
                    }
                }

                Console.WriteLine("Assigned teacher to students successfully...");
                Console.WriteLine("The Assigned teacher to students are: ");

                var output = context.TeacherStudent.Include(ts => ts.Teacher).Include(ts => ts.Student).AsNoTracking().ToList();  
                foreach (var i in output)
                {
                    Console.WriteLine($"\tTeacher: {i.Teacher.TeacherName} \t\t\tStudent: {i.Student.StudentName}");
                }

                Console.ReadKey();
            }
        }


        static void AddTeacher()
        {
            using (var context = new AppDbContext())
            {
                var t = new Teacher();
                Console.Write("Enter Teacher's name: ");
                t.TeacherName = Console.ReadLine();
                Console.Write("Enter Teacher's Phone Number: ");
                t.TeacherPhoneNumber = Console.ReadLine();
                Console.Write("Enter Teacher Salary: ");
                t.TeacherSalary = int.Parse(Console.ReadLine());
                Console.Write("Enter Teacher's Address: ");
                t.TeacherAddress = Console.ReadLine();

                context.Teachers.Add(t);
                context.SaveChanges();
            }
        }

        static void RemoveTeacher()
        {
            using (var context = new AppDbContext())
            {
                var st = context.Teachers.ToList();
                foreach (var i in st)
                {
                    Console.WriteLine("Student id " + i.TeacherId + " : " + i.TeacherName);
                }
                Console.Write("Which Student you want to delete: ");
                int id = int.Parse(Console.ReadLine());
                var man = context.Teachers.Single(e => e.TeacherId == id);
                context.Teachers.Remove(man);
                context.SaveChanges();
            }
        }

        static void ListTeachers()
        {
            using (var context = new AppDbContext())
            {
                var st = context.Teachers.ToList();
                foreach (var i in st)
                {
                    Console.WriteLine("Teacher's id: " + i.TeacherId);
                    Console.WriteLine("Teacher's Name: " + i.TeacherName);
                    Console.WriteLine("Teacher's Gpa: " + i.TeacherSalary);
                    Console.WriteLine("Teacher's Adress: " + i.TeacherAddress);
                    Console.WriteLine("Teacher's PhoneNumber: " + i.TeacherPhoneNumber);
                    var courses = context.Teachers
                        .Where(e => e.TeacherId == i.TeacherId)
                        .SelectMany(s => s.TeacherCourses)
                        .Select(sc => sc.Course)
                        .ToList();
                    foreach (var course in courses)
                    {
                        
                        Console.WriteLine("Teacher's Courses : " + course.CourseName);
                        
                    }
                    Console.WriteLine("======================================");
                }
                Console.ReadKey();
            }
        }

        static void FindTeacher()
        {
            using (var context = new AppDbContext())
            {
                Console.Write("Enter the Teacher id to retreive: ");
                int id = int.Parse(Console.ReadLine());
                var i = context.Teachers.Single(i => i.TeacherId == id);
                Console.WriteLine("Teacher's id: " + i.TeacherId);
                Console.WriteLine("Teacher's Name: " + i.TeacherName);
                Console.WriteLine("Teacher's Salary: " + i.TeacherSalary);
                Console.WriteLine("Teacher's Adress: " + i.TeacherAddress);
                Console.WriteLine("Teacher's PhoneNumber: " + i.TeacherPhoneNumber);
                var courses = context.Teachers
                    .Where(e => e.TeacherId == i.TeacherId)
                    .SelectMany(s => s.TeacherCourses)
                    .Select(sc => sc.Course)
                    .ToList();
                foreach (var course in courses)
                {
                    
                    Console.WriteLine("Teacher's Courses : "  + course.CourseName);
                    
                }
            }
            Console.ReadKey();
        }

        static void UpdateTeacher()
        {
            using (var context = new AppDbContext())
            {
                var st = context.Teachers.ToList();
                foreach (var i in st)
                {
                    Console.WriteLine("Teacher's id " + i.TeacherId + " : " + i.TeacherName);
                }
                Console.Write("Which Teacher you want to update: ");
                int id = int.Parse(Console.ReadLine());
                var man = context.Teachers.Single(i => i.TeacherId == id);
                Console.Write("Enter Teacher's name: ");
                man.TeacherName = Console.ReadLine();
                Console.Write("Enter Teacher's Salary: ");
                man.TeacherSalary = int.Parse(Console.ReadLine());
                Console.Write("Enter Teacher's Address: ");
                man.TeacherAddress = Console.ReadLine();
                Console.Write("Enter Teacher's Number: ");
                man.TeacherPhoneNumber = Console.ReadLine();

                context.SaveChanges();
            }
        }

        static void CourseFunc()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("\t\t\t\t Course Menu");
                Console.WriteLine("What functionality you want to do with the database: ");
                Console.WriteLine("1.Add. ");
                Console.WriteLine("2.Remove. ");
                Console.WriteLine("3.List All Course. ");
                Console.WriteLine("4.Find One Course.");
                Console.WriteLine("5.Update. ");
                Console.WriteLine("6.Main Menu. ");
                Console.Write("\t Enter your option: ");
                char option = char.Parse(Console.ReadLine());
                if (option == '1')
                {
                    AddCourse();
                }
                else if (option == '2')
                {
                    RemoveCourse();
                }
                else if (option == '3')
                {
                    ListCourses();
                }
                else if (option == '4')
                {
                    FindCourse();
                }
                else if(option == '5')
                {
                    UpdateCourse();
                }
                else if(option=='6')
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Wrong Input. PLease Enter Again.....");
                    Console.ReadKey();
                }
            }
        }

        static void AddCourse()
        {
            using (var context = new AppDbContext())
            {
                var t = new Course();
                Console.Write("Enter Course's name: ");
                t.CourseName = Console.ReadLine();

                context.Courses.Add(t);
                context.SaveChanges();
            }
        }

        static void RemoveCourse()
        {
            using (var context = new AppDbContext())
            {
                var st = context.Courses.ToList();
                foreach (var i in st)
                {
                    Console.WriteLine("Course id " + i.CourseID + " : " + i.CourseName);
                }
                Console.Write("Which Course you want to delete: ");
                int id = int.Parse(Console.ReadLine());
                var man = context.Courses.Single(e => e.CourseID == id);
                context.Courses.Remove(man);
                context.SaveChanges();
            }
        }

        static void ListCourses()
        {
            using (var context = new AppDbContext())
            {
                var st = context.Courses.ToList();
                foreach (var i in st)
                {
                    Console.WriteLine("Course's id: " + i.CourseID);
                    Console.WriteLine("Course's Name: " + i.CourseName);

                    Console.WriteLine("======================================");
                }
                Console.ReadKey();
            }
        }

        static void FindCourse()
        {
            using (var context = new AppDbContext())
            {
                Console.Write("Enter the Course id to retreive: ");
                int id = int.Parse(Console.ReadLine());
                var i = context.Courses.Single(i => i.CourseID == id);
                Console.WriteLine("Teacher's id: " + i.CourseID);
                Console.WriteLine("Teacher's Name: " + i.CourseName);
                
            }
            Console.ReadKey();
        }

        static void UpdateCourse()
        {
            using (var context = new AppDbContext())
            {
                var st = context.Courses.ToList();
                foreach (var i in st)
                {
                    Console.WriteLine("Course's id " + i.CourseID + " : " + i.CourseName);
                }
                Console.Write("Which Course you want to update: ");
                int id = int.Parse(Console.ReadLine());
                var man = context.Courses.Single(i => i.CourseID == id);
                Console.Write("Enter Course's name: ");
                man.CourseName = Console.ReadLine();
                

                context.SaveChanges();
            }
        }
    }
}