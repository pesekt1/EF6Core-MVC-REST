using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace MVCCore.Models
{
    public class Student
    {
        public Student()
        {
        }

        public Student(string lastName, string firstMidName, DateTime enrollmentDate)
        {
            LastName = lastName;
            FirstMidName = firstMidName;
            EnrollmentDate = enrollmentDate;
        }

        public int ID { get; set; }
        public string LastName { get; set; }
        public string FirstMidName { get; set; }
        public DateTime EnrollmentDate { get; set; }

        public virtual ICollection<Enrollment> Enrollments { get; set; }
    }
}
