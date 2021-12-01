using System;
using Microsoft.Data.SqlClient;
namespace teacher
{
    public class Teacher
    {
        private int Id;
        public int MyId
        {
            get { return Id; }
            set { Id = value; }
        }

        private int name;
        public int Myname
        {
            get { return name; }
            set { name = value; }
        }


        private int salary;
        public int MySalry
        {
            get { return salary; }
            set { salary = value; }
        }

        private int experience;
        public int MyExp
        {
            get { return experience; }
            set { experience = value; }
        }

        private int NoOfCoursesAssigned;
        public int MyDues
        {
            get { return NoOfCoursesAssigned; }
            set { NoOfCoursesAssigned = value; }
        }

        public bool TeacherLogin()
        {
            Console.WriteLine("\n\n******************Teacher Login************************ \n");
            string conStr = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=BDSpucit;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            SqlConnection con = new SqlConnection(conStr);
            Console.Write("Enter teacher's username: ");
            string userNam = Console.ReadLine();
            Console.Write("Enter Password: ");
            string pwd = Console.ReadLine();

            SqlParameter p1 = new SqlParameter("u", userNam);
            SqlParameter p2 = new SqlParameter("p", pwd);
            string query = $"SELECT * FROM users WHERE username=@u AND password=@p";

            SqlCommand comand = new SqlCommand(query, con);
            comand.Parameters.Add(p1);
            comand.Parameters.Add(p2);

            con.Open();

            SqlDataReader dr = comand.ExecuteReader();
            bool value = true;
            SqlParameter p3 = new SqlParameter("v", value);
            SqlParameter p4 = new SqlParameter("n", userNam);
            bool result = dr.HasRows;
            dr.Close();
            if(result)
            {
                query = $"UPDATE Teacher SET loginStatus=@v WHERE username=@n"; //username of student in CMS is his/her roll no
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.Add(p3);
                cmd.Parameters.Add(p4);
                cmd.ExecuteNonQuery();
            }
            if(result)
            {
                Console.WriteLine("Login Sucessfull.");
                DisplayTeacherMenu();
                con.Close();
                return true;
            }
            con.Close();
            Console.WriteLine("Login Unsucessfull. Invalid username or password entered");
            return false;
        }


        public void DisplayTeacherMenu()
        {
            int choice;
            do
            {
                string msg = "\n\nEnter 1 to Mark attendance \n Enter 2 to post assignment \n Enter 3 to View Assigned Courses \n 4.Exit";
                Console.WriteLine(msg);
                string result = Console.ReadLine();
                choice = int.Parse(result);
                switch (choice)
                {
                    case 1:
                        MarkAttendance();
                        break;

                    case 2:
                        PostAssignment();
                        break;

                    case 3:
                        ViewAssignedCourses();
                        break;

                    default:
                        break;
                }
            } while ((choice >= 1) && (choice <= 3));

        }

        public void MarkAttendance()
        { 
        }
        public void PostAssignment()
        {
            string conStr2 = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=BDSpucit;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            SqlConnection con2 = new SqlConnection(conStr2);
            con2.Open();
            string query2 = "SELECT courseId FROM AssignmentInfo WHERE t_Id = ( SELECT Id FROM Teacher WHERE loginStatus=1)"; //courses of  currently logged in teacher
            SqlCommand comand2 = new SqlCommand(query2, con2);
            SqlDataReader dr2 = comand2.ExecuteReader();
            Console.WriteLine("(Id of) Your assigned courses:");
            while (dr2.Read())
            {
                Console.WriteLine(dr2[0]);
            }
            Console.WriteLine("Enter the course Id whose assignment you want to assign: ");
            string c_Id = Console.ReadLine();

            Console.WriteLine("Enter the assignment topic: ");
            string topc = Console.ReadLine();
            Console.WriteLine("Enter the assignment descriptn: ");
            string descr = Console.ReadLine();
            Console.WriteLine("Enter the assignment deadline: ");
            string deadl = Console.ReadLine();
           
            dr2.Close();
            string query = $"UPDATE AssignmentInfo SET Astopic=@t, AsDescriptn=@d, AsDeadline=@l WHERE courseId=@i";
            SqlParameter p1 = new SqlParameter("t", topc);
            SqlParameter p2 = new SqlParameter("d", descr);
            SqlParameter p3 = new SqlParameter("l", deadl);
            SqlParameter p4 = new SqlParameter("i", c_Id);

            SqlCommand comand = new SqlCommand(query, con2);
            comand.Parameters.Add(p1);
            comand.Parameters.Add(p2);
            comand.Parameters.Add(p3);
            comand.Parameters.Add(p4);
            comand.ExecuteNonQuery();

            con2.Close();

        }
        public void ViewAssignedCourses()
        {
            string conStr2 = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=BDSpucit;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            SqlConnection con2 = new SqlConnection(conStr2);
            con2.Open();
            bool value = true;
            SqlParameter p3 = new SqlParameter("v", value);
            string query2 = " SELECT courseName FROM Course WHERE courseId IN (SELECT courseId from AssignmentInfo WHERE t_Id=(SELECT Id FROM Teacher WHERE loginStatus=@v))"; //courses of  currently logged in teacher
            SqlCommand comand = new SqlCommand(query2, con2);
            comand.Parameters.Add(p3);
            SqlDataReader dr = comand.ExecuteReader();
            Console.WriteLine("Your assigned courses:");
            while (dr.Read())
            {
                Console.WriteLine(dr[0]);
            }
        }


    }
}
