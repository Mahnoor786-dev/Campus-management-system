using System;
using Microsoft.Data.SqlClient;
namespace student
{
    public class Student
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


        private int RollNo;

        public int MyRoll
        {
            get { return RollNo; }
            set { RollNo = value; }
        }

        private int batch;

        public int MyBatch
        {
            get { return batch; }
            set { batch = value; }
        }

        private int semDues;

        public int MyDues
        {
            get { return semDues; }
            set { semDues = value; }
        }

        private int currentSem;

        public int MySemester
        {
            get { return currentSem; }
            set { currentSem = value; }
        }



        public bool StudentLogin()
        {
            Console.WriteLine("\n\n******************Student Login************************ \n");
            string conStr = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=BDSpucit;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            SqlConnection con = new SqlConnection(conStr);
            Console.Write("Enter student UserName i.e Roll no: ");
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
            bool result = dr.HasRows;
            dr.Close();
            SqlParameter p4 = new SqlParameter("n", userNam);
            if (result)
            {
                query = $"UPDATE Student SET loginStatus=@v WHERE RollNo=@n"; //username of student in CMS is his/her roll no
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.Add(p3);
                cmd.Parameters.Add(p4);
                cmd.ExecuteNonQuery();
            }
            if (result)
            {
                Console.WriteLine("Login Sucessfull.");
                DisplayStudentMenu();
                con.Close();
                return true;
            }

            con.Close();
            Console.WriteLine("Login Unsucessfull. Invalid username or password entered");
            return false;
        }


        public void DisplayStudentMenu()
        {
            int choice;
            do
            {
                string msg = "\n\n1. Enter 1 to Pay Semester Dues \n 2. Enter 2 to view enrolled courses \n 3. Enter 3 to view Assignments \n 4. Enter 4 to Exit";
                Console.WriteLine(msg);
                string result = Console.ReadLine();
                choice = int.Parse(result);
                switch (choice)
                {
                    case 1:
                        PaySemesterDues();
                        break;

                    case 2:
                        ViewCoursesEnrolled();
                        break;

                    case 3:
                        Console.WriteLine("Enter course Id whose assignments you want to view: ");
                        string Id = Console.ReadLine();
                        ViewAssignments(Id);
                        break;

                    default:
                        break;
                }
            } while ((choice >= 1) && (choice <= 3));


        }

        private void PaySemesterDues()
        {
            string conStr = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=BDSpucit;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            SqlConnection con = new SqlConnection(conStr);
            string status = "True";
            SqlParameter p1 = new SqlParameter("v", status);
            Console.Write("Enter semester fee you want to pay now: ");
            string fee1 = Console.ReadLine();
            int fee = int.Parse(fee1);
            SqlParameter p2 = new SqlParameter("f", fee);
            con.Open();
            string query = "UPDATE Student SET semseterDues = semseterDues-@f WHERE loginStatus=@v"; //update paid dues to keep track of original dues
            SqlCommand comand = new SqlCommand(query, con);
            comand.Parameters.Add(p1);
            comand.Parameters.Add(p2);
            comand.ExecuteNonQuery();
            con.Close();
        }


        private void ViewCoursesEnrolled()
        {
            string conStr = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=BDSpucit;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            SqlConnection con = new SqlConnection(conStr);
            bool value = true;
            SqlParameter p3 = new SqlParameter("v", value);
            string query = "SELECT courseName, courseId FROM Course WHERE courseId IN (SELECT c_Id FROM studentCourses WHERE sRollno=(SELECT RollNo FROM Student WHERE loginStatus =@v))";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.Add(p3);
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            Console.WriteLine("Your enrolled courses are:");
            while (dr.Read())
            {
                Console.WriteLine(dr["courseName"] + " --> " + dr["courseId"]);
            }

        }


        private void ViewAssignments(string courseId)
        {
            string conStr = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=BDSpucit;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            SqlConnection con = new SqlConnection(conStr);
            con.Open();
            SqlParameter p3 = new SqlParameter("i", courseId);
            string query = "SELECT Astopic, AsDescriptn, AsDeadline FROM AssignmentInfo WHERE courseId = @i";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.Add(p3);
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                String s = String.Format("{0,-30} {1,-30} {2,-35}  \n", "Topic", "| Deadline", "| Description");
                Console.WriteLine(s);
                s = String.Format("{0,-30} {1,-30} {2,-35}  \n", dr[0].ToString(), dr[2].ToString(), dr[1].ToString());
                Console.WriteLine(s);
            }
            dr.Close();
            con.Close();
        }



    }
}
