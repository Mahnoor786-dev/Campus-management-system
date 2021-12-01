using System;
using Microsoft.Data.SqlClient;

namespace myAdmin
{
    public class Admin
    {
        public bool AdminLogin()
        {
            Console.WriteLine("\n\n******************Admin Login************************ \n");
            string conStr = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=BDSpucit;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            SqlConnection con = new SqlConnection(conStr);
            Console.Write("Enter admin's username: ");
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

            if (dr.HasRows)
            {
                DisplayAdminMenu();
                con.Close();
                return true;
            }
            con.Close();
            Console.WriteLine("Login Unsucessfull. Invalid username or password entered");
            return false;
        }


        public void DisplayAdminMenu()
        {
            int choice = 1;
            while ((choice >= 1) && (choice <= 3))
            {
            string msg = "\n\n Enter 1 to Manage Students \n Enter 2 to Manage Teacher \n Enter 3 to Manage Courses \n 4. Enter 4 to Exit";
            Console.WriteLine(msg);
            string result = Console.ReadLine();
            choice = int.Parse(result);
            switch (choice)
                {
                    case 1:
                        ManageStudent();
                        break;

                    case 2:
                        ManageTeacher();
                        break;

                    case 3:
                        ManageCourses();
                        break;

                    default:
                        break;
                }
            }
        }


        //---------------------------------manage STUDENTS functions-----

        public void ManageStudent()
        {
            string menu1= " 1. Enter 1 to Add Student \n 2. Enter 2 to Update Student \n 3. Enter 3 to Delete Student\n 4. Enter 4 to View All Students\n 5. Enter 5 to Display Outstanding Semester Dues\n 6. Enter 6 to Assign Course to Student \n 7. Press 7 to Exit";
            Console.WriteLine(menu1);
            string result = Console.ReadLine();
            int choice = int.Parse(result);
            switch (choice)
            {
                case 1:
                    AddStudent();
                    break;

                case 2:
                    UpdateStudent();
                    break;

                case 3:
                    DeleteStudent();
                    break;

                case 4:
                    ViewAllStudents();
                    break;

                case 5:
                    DisplayOutstandingSemesterDues();
                    break;

                case 6:
                    assignCourse();
                    break;

                default:
                    break;
            }
        }

        private void AddStudent()
       {
            string conStr = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=BDSpucit;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            SqlConnection con = new SqlConnection(conStr);
            Console.Write("Enter student name: ");
            string Name = Console.ReadLine();
            Console.Write("Enter student roll no: ");
            string rollNo = Console.ReadLine();
            Console.Write("Enter student batch: ");
            string batch = Console.ReadLine();
            Console.Write("Enter student's payable semester dues: ");
            string dues = Console.ReadLine();
            Console.Write("Enter student current semester: ");
            string semester = Console.ReadLine();
            con.Open();
            SqlParameter p1 = new SqlParameter("n", Name);
            SqlParameter p2 = new SqlParameter("r", rollNo);
            SqlParameter p3 = new SqlParameter("b", batch);
            SqlParameter p4 = new SqlParameter("d", dues);
            SqlParameter p5 = new SqlParameter("s", semester);
            string query = $"INSERT INTO Student(Name, RollNo, batch, semseterDues, currentSemester) VALUES(@n, @r, @b, @d, @s)";
            
            SqlCommand comand = new SqlCommand(query, con);
            comand.Parameters.Add(p1);
            comand.Parameters.Add(p2);
            comand.Parameters.Add(p3);
            comand.Parameters.Add(p4);
            comand.Parameters.Add(p5);

            comand.ExecuteNonQuery();
            con.Close();

        }

        private void UpdateStudent()
        {
            string conStr = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=BDSpucit;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            SqlConnection con = new SqlConnection(conStr);
           
            Console.Write("Enter student roll no whose info you want to update: ");
            string rollNo = Console.ReadLine();
            Console.Write("Enter student semester dues(updated): ");
            string dues = Console.ReadLine();
            Console.Write("Enter student current semester(updated): ");
            string semester = Console.ReadLine();
            con.Open();
            SqlParameter p2 = new SqlParameter("r", rollNo);
            SqlParameter p4 = new SqlParameter("d", dues);
            SqlParameter p5 = new SqlParameter("s", semester);
            string query = $"UPDATE Student SET semseterDues=@d, currentSemester=@s WHERE RollNo =@r";

            SqlCommand comand = new SqlCommand(query, con);
            comand.Parameters.Add(p2);
            comand.Parameters.Add(p4);
            comand.Parameters.Add(p5);

            comand.ExecuteNonQuery();
            con.Close();

        }

        private void DeleteStudent()
        {
            string conStr = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=BDSpucit;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            SqlConnection con = new SqlConnection(conStr);
           
            Console.Write("Enter student roll no whose record you want to delete: ");
            string rollNo = Console.ReadLine();
            con.Open();
            SqlParameter p1 = new SqlParameter("r", rollNo);
            string query = $"DELETE from Student WHERE RollNo=@r";
            SqlCommand comand = new SqlCommand(query, con);
            comand.Parameters.Add(p1);
           
            comand.ExecuteNonQuery();
            con.Close();

        }

        private void ViewAllStudents()
        {
            string conStr = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=BDSpucit;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            SqlConnection con = new SqlConnection(conStr);
            con.Open();
           
            string query = "SELECT * FROM Student";
            SqlCommand comand = new SqlCommand(query, con);
            SqlDataReader dr = comand.ExecuteReader();
              String s = String.Format("{0,-11} {1,-14} {2,-15} {3,-10} \n", "Name", "RollNo", "Batch", "currentSemester");
                Console.WriteLine(s);
            while (dr.Read())
            {
                s = String.Format("{0,-11} {1,-14} {2,-15} {3,-10} \n", dr[0].ToString(), dr[1].ToString(), dr[2].ToString(), dr[3].ToString());
                Console.WriteLine(s);
            }
            con.Close();
        }

        private void DisplayOutstandingSemesterDues()
        {
            string conStr = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=BDSpucit;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            SqlConnection con = new SqlConnection(conStr);
            con.Open();
          //  string value;
           // int value1;
            string query1 = "SELECT Name, RollNo, semseterDues FROM Student WHERE semseterDues!=0;";
            SqlCommand comand1 = new SqlCommand(query1, con);
            SqlDataReader dr = comand1.ExecuteReader();
            String s = String.Format("{0,-16} {1,-14} {2,-15} \n", "Name", "RollNo", "semseterDues");
            Console.WriteLine(s);
            while (dr.Read())
            {
               // value = dr["semseterDues"].ToString();
               // value1 = int.Parse(value);
                s = String.Format("{0,-16} {1,-14} {2,-15} \n", dr["Name"].ToString(), dr["RollNo"].ToString(), dr["semseterDues"].ToString());
                Console.WriteLine(s);
            }
            dr.Close();
            con.Close();
        }

        private void assignCourse()
        {
            string conStr = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=BDSpucit;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            SqlConnection con = new SqlConnection(conStr);
           
            Console.Write("Enter student roll no whom you want to assign course: ");
            string rollNo = Console.ReadLine();
            Console.Write("Enter course id: ");
            string cId = Console.ReadLine();
            con.Open();
          
            SqlParameter p1 = new SqlParameter("r", rollNo);
            SqlParameter p2 = new SqlParameter("c", cId);

            string query = $"INSERT INTO studentCourses(sRollno, c_Id) VALUES(@r, @c)";

            SqlCommand comand = new SqlCommand(query, con);
            comand.Parameters.Add(p1);
            comand.Parameters.Add(p2);
           
            comand.ExecuteNonQuery();
            con.Close();
        }


        //---------------------------------manage TEACHERS functions-----

        public void ManageTeacher()
        {
            string menu1 = "1- Enter 1 to Add Teacher \n 2- Enter 2 to Update Teacher \n 3- Enter 3 to Delete Teacher \n 4- Enter 4 to View All Teachers \n 5- Enter 5 to Assign Course to Teacher \n 6-Enter 6 to Exit";
            Console.WriteLine(menu1);
            string result = Console.ReadLine();
            int choice = int.Parse(result);
            switch (choice)
            {
                case 1:
                    AddTeacher();
                    break;

                case 2:
                    UpdateTeacher();
                    break;

                case 3:
                    DeleteTeacher();
                    break;

                case 4:
                    ViewAllTeachers();
                    break;

                case 5:
                    assignCourseToTeacher();
                    break;

                default:
                    break;
            }
        }
       
        private void AddTeacher()
        {
            string conStr = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=BDSpucit;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            SqlConnection con = new SqlConnection(conStr);
            Console.Write("Enter teacher Id: ");
            string id1 = Console.ReadLine();
            int id = int.Parse(id1);
            Console.Write("Enter teacher name: ");
            string Name = Console.ReadLine();
            string uName = Name + ".faculty";
            Console.Write("Enter teacher salary: ");
            string sal1 = Console.ReadLine();
            int sal = int.Parse(sal1);
            Console.Write("Enter teacher experience: ");
            string exp = Console.ReadLine();
            Console.Write("Enter no of assigned courses to teacher: ");
            string assignedCourses1 = Console.ReadLine();
            int assignedCourses = int.Parse(assignedCourses1);

            con.Open();
            SqlParameter p1 = new SqlParameter("n", Name);
            SqlParameter p2 = new SqlParameter("s", sal);
            SqlParameter p3 = new SqlParameter("e", exp);
            SqlParameter p4 = new SqlParameter("c", assignedCourses);
            SqlParameter p5 = new SqlParameter("u", uName);
            SqlParameter p6 = new SqlParameter("i", id);

            string query = $"INSERT INTO Teacher(Id, Name, salary, experience, noOfAssCourses, username) VALUES(@i, @n, @s, @e, @c, @u)";

            SqlCommand comand = new SqlCommand(query, con);
            comand.Parameters.Add(p1);
            comand.Parameters.Add(p2);
            comand.Parameters.Add(p3);
            comand.Parameters.Add(p4);
            comand.Parameters.Add(p5);
            comand.Parameters.Add(p6);

            comand.ExecuteNonQuery();
            con.Close();
        }

        private void UpdateTeacher()
        {
            string conStr = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=BDSpucit;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            SqlConnection con = new SqlConnection(conStr);
            Console.Write("Enter teacher id  whose information you want to update: ");
            string id1 = Console.ReadLine();
            int id = int.Parse(id1);
            Console.Write("Enter teacher salary (updated): ");
            string sal1 = Console.ReadLine();
            int sal = int.Parse(sal1);
            Console.Write("Enter teacher experience(updated): ");
            string exp = Console.ReadLine();
            Console.Write("Enter no of assigned courses to teacher(updated): ");
            string assignedCourses1 = Console.ReadLine();
            int assignedCourses = int.Parse(assignedCourses1);

            con.Open();
            SqlParameter p1 = new SqlParameter("i", id);
            SqlParameter p2 = new SqlParameter("s", sal);
            SqlParameter p3 = new SqlParameter("e", exp);
            SqlParameter p4 = new SqlParameter("c", assignedCourses);

            string query = $"UPDATE Teacher SET salary =@s, experience=@e, noOfAssCourses=@c WHERE Id=@i";

            SqlCommand comand = new SqlCommand(query, con);
            comand.Parameters.Add(p1);
            comand.Parameters.Add(p2);
            comand.Parameters.Add(p3);
            comand.Parameters.Add(p4);
       
            comand.ExecuteNonQuery();
            con.Close();
        }

        private void DeleteTeacher()
        {
            string conStr = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=BDSpucit;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            SqlConnection con = new SqlConnection(conStr);

            Console.Write("Enter teacher's Id whose record you want to delete: ");
            string id1 = Console.ReadLine();
            int id = int.Parse(id1);
            con.Open();
            SqlParameter p1 = new SqlParameter("i", id);
            string query = $"DELETE from Teacher WHERE Id=@i";
            SqlCommand comand = new SqlCommand(query, con);
            comand.Parameters.Add(p1);
            comand.ExecuteNonQuery();
            con.Close();
        }

        private void ViewAllTeachers()
        {
            string conStr = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=BDSpucit;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            SqlConnection con = new SqlConnection(conStr);
            con.Open();

            string query = "SELECT * FROM Teacher";
            SqlCommand comand = new SqlCommand(query, con);
            SqlDataReader dr = comand.ExecuteReader();
            String s = String.Format("{0,-11} {1,-14} {2,-15} {3,-17} \n", "Name", "salary", "experience", "no Of Assigned Courses");
            Console.WriteLine(s);
            while (dr.Read())
            {
                s = String.Format("{0,-11} {1,-14} {2,-15} {3,-17} \n", dr["Name"].ToString(), dr["salary"].ToString(), dr["experience"].ToString(), dr["noOfAssCourses"].ToString());
                Console.WriteLine(s);
            }
            con.Close();
        }

        private void assignCourseToTeacher()
        {
            string conStr = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=BDSpucit;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            SqlConnection con = new SqlConnection(conStr);
            Console.Write("Enter teacher's Id to whom you want to assign a course: ");
            string id = Console.ReadLine();
            Console.Write("Enter course ID that you want to assign to this teacher: ");
            string courseId = Console.ReadLine();
            
            con.Open();
            SqlParameter p1 = new SqlParameter("i", id);
            SqlParameter p2 = new SqlParameter("c", courseId);

            string query = $"INSERT INTO AssignmentInfo(t_Id, courseId) VALUES(@i, @c)";
            SqlCommand comand = new SqlCommand(query, con);
            comand.Parameters.Add(p1);
            comand.Parameters.Add(p2);
            comand.ExecuteNonQuery();

            SqlParameter p3 = new SqlParameter("d", id);
            string query2 = "UPDATE Teacher SET noOfAssCourses = noOfAssCourses+1 WHERE Id=@d";
            SqlCommand comand2 = new SqlCommand(query2, con);
            comand2.Parameters.Add(p3);
            comand.ExecuteNonQuery();

            con.Close();
        }


        //---------------------------------manage COURSES functions-----

        private void ManageCourses()
        {

            string menu = "1- Enter 1 to Add Courses \n 2- Enter 2 to Update Courses\n 3- Enter 3 to Delete Courses\n 4- Enter 4 to View All Courses \n 5- Press 5 to Exit";
            Console.WriteLine(menu);
            string result = Console.ReadLine();
            int choice = int.Parse(result);
            switch (choice)
            {
                case 1:
                    AddCourses();
                    break;

                case 2:
                    UpdateCourses();
                    break;

                case 3:
                    DeleteCourses();
                    break;

                case 4:
                    ViewAllCourses();
                    break;

                default:
                    break;
            }

        }


        private void AddCourses()
        {
            string conStr = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=BDSpucit;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            SqlConnection con = new SqlConnection(conStr);
            Console.Write("Enter Course Id: ");
            string id = Console.ReadLine();
            Console.Write("Enter Course name: ");
            string cName = Console.ReadLine();
            Console.Write("Enter its credit hours: ");
            string hrs = Console.ReadLine();
            int cHours = int.Parse(hrs);
            con.Open();

            SqlParameter p1 = new SqlParameter("i", id);
            SqlParameter p2 = new SqlParameter("n", cName);
            SqlParameter p3 = new SqlParameter("h", cHours);

            string query = $"INSERT INTO Course(courseId, courseName, creditHours) VALUES(@i, @n, @h)";
            SqlCommand comand = new SqlCommand(query, con);
            comand.Parameters.Add(p1);
            comand.Parameters.Add(p2);
            comand.Parameters.Add(p3);
            comand.ExecuteNonQuery();
            con.Close();
        }
        private void UpdateCourses()
        {
            string conStr = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=BDSpucit;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            SqlConnection con = new SqlConnection(conStr);
            //course Id not updated because its unique for each course
            Console.Write("Enter Course Id whose information you want to update: ");
            string cId = Console.ReadLine();
            SqlParameter p1 = new SqlParameter("i", cId);
            Console.Write("Enter updated Course name: ");
            string cName = Console.ReadLine();
            Console.Write("Enter updated credit hours: ");
            string hrs = Console.ReadLine();
            int cHours = int.Parse(hrs);
            con.Open();

            SqlParameter p2 = new SqlParameter("n", cName);
            SqlParameter p3 = new SqlParameter("h", cHours);

            string query = $"UPDATE Course SET courseName=@n, creditHours=@h WHERE courseId=@i";
            SqlCommand comand = new SqlCommand(query, con);
            comand.Parameters.Add(p1);
            comand.Parameters.Add(p2);
            comand.Parameters.Add(p3);
            comand.ExecuteNonQuery();
            con.Close();
        }

        private void DeleteCourses()
        {
            string conStr = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=BDSpucit;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            SqlConnection con1 = new SqlConnection(conStr);
            
            Console.Write("Enter Id of Course that you want to delete: ");
            string id = Console.ReadLine();
            con1.Open();

            SqlParameter p1 = new SqlParameter("d", id);
            string query1 = "DELETE FROM studentCourses WHERE c_Id = @d";
            SqlCommand comand = new SqlCommand(query1, con1);
            comand.Parameters.Add(p1);
            comand.ExecuteNonQuery();
            con1.Close();

            SqlConnection con = new SqlConnection(conStr);
            SqlParameter p2 = new SqlParameter("i", id);
            con.Open();
            string query = $"DELETE FROM Course WHERE courseId=@i";
            SqlCommand comand1 = new SqlCommand(query, con);
            comand1.Parameters.Add(p2);
            comand1.ExecuteNonQuery();
            con.Close();
        }

        private void ViewAllCourses()
        {
            string conStr = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=BDSpucit;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            SqlConnection con = new SqlConnection(conStr);
            con.Open();

            string query = "SELECT * FROM Course";
            SqlCommand comand = new SqlCommand(query, con);
            SqlDataReader dr = comand.ExecuteReader();
            String s = String.Format("{0,-11} {1,-14} {2,-15} \n", "courseId", "courseName", "creditHours");
            Console.WriteLine(s);
            while (dr.Read())
            {
                s = String.Format("{0,-11} {1,-14} {2,-15} \n", dr["courseId"].ToString(), dr["courseName"].ToString(), dr["creditHours"].ToString());
                Console.WriteLine(s);
            }
            con.Close();
        }

    }
}
