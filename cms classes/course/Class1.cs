using System;
namespace mycourse
{
    public class Course
    {
        private int Id;
        public int MyProperty
        {
            get { return Id; }
            set { Id = value; }
        }

        private int CourseName;
        public int MyProperty2
        {
            get { return CourseName; }
            set { CourseName = value; }
        }

        private int CreditHours;
        public int MyProperty3
        {
            get { return CreditHours; }
            set { CreditHours = value; }
        }

    }
}
