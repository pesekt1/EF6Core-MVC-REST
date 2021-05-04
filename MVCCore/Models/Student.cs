using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace MVCCore.Models
{
    [Table("Student")]
    public class Student
    {
        public Student(string lastName, string firstMidName, DateTime enrollmentDate)
        {
            LastName = lastName;
            FirstMidName = firstMidName;
            EnrollmentDate = enrollmentDate;
        }

        public Student()
        {
        }

        public int ID { get; set; }
        public string LastName { get; set; }
        public string FirstMidName { get; set; }
        public DateTime EnrollmentDate { get; set; }

        public virtual ICollection<Enrollment> Enrollments { get; set; }
    }
}
