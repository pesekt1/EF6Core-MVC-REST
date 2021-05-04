using System.Collections.Generic;

namespace MVCCore.Models
{
    public class Course
    {
        public Course(string title, int credits)
        {
            Title = title;
            Credits = credits;
        }

        public Course()
        {
        }

        //[DatabaseGenerated(DatabaseGeneratedOption.None)]

        public int CourseID { get; set; }

        public string Title { get; set; }

        public int Credits { get; set; }

        public virtual ICollection<Enrollment> Enrollments { get; set; }
    }
}
