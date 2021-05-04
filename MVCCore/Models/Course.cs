using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace MVCCore.Models
{
    [Table("Course")]
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

        [JsonIgnore]
        public virtual ICollection<Enrollment> Enrollments { get; set; }
    }
}
