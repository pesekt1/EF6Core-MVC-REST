using System;
using System.Collections.Generic;

#nullable disable

namespace MVCCore.Models2
{
    public partial class Course
    {
        public Course()
        {
            CourseAssignments = new HashSet<CourseAssignment>();
            Enrollments = new HashSet<Enrollment>();
        }

        public int CourseId { get; set; }
        public string Title { get; set; }
        public int Credits { get; set; }
        public int DepartmentId { get; set; }

        public virtual Department Department { get; set; }
        public virtual ICollection<CourseAssignment> CourseAssignments { get; set; }
        public virtual ICollection<Enrollment> Enrollments { get; set; }
    }
}
