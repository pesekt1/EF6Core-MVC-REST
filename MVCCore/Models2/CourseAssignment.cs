using System;
using System.Collections.Generic;

#nullable disable

namespace MVCCore.Models2
{
    public partial class CourseAssignment
    {
        public int InstructorId { get; set; }
        public int CourseId { get; set; }

        public virtual Course Course { get; set; }
        public virtual Instructor Instructor { get; set; }
    }
}
