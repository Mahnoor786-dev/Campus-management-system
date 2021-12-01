using System;
using teacher;
using student;
using myAdmin;
namespace As_2
{
    class Program
    {
        static void Main(string[] args)
        {
            bool decision;
            string mychoice;
            int choice;
            do
            {
                string menu1 = "\n\n***************Course Management System******************* \n Press 1 to Login as Student \n Press 2 to Login as Instructor \n Press 3 to Login as Admin";
                Console.WriteLine(menu1);
                mychoice = Console.ReadLine();
                choice = int.Parse(mychoice);

                switch (choice)
                {
                    case 1:
                        Student student1 = new Student();
                        decision= student1.StudentLogin();
                        if(decision==true)
                            student1.DisplayStudentMenu();
                        break;

                    case 2:
                        Teacher teacher1 = new Teacher();
                        decision= teacher1.TeacherLogin();
                        if (decision == true)
                            teacher1.DisplayTeacherMenu();
                        break;

                    case 3:
                        Admin admin1 = new Admin();
                        decision = admin1.AdminLogin();
                        if (decision == true)
                            admin1.DisplayAdminMenu();
                        break;

                    default:
                        Console.WriteLine("Invalid entry");
                        break;

                }
            } while ((choice >= 1) && (choice <= 3));


        }
    }
}
