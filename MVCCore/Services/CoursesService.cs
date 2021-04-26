using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVCCore.Models;

namespace MVCCore.Services
{
    public class CoursesService
    {
        private readonly SchoolContext _context;
        
        public CoursesService(SchoolContext context)
        {
            _context = context;
        }
        
        public async Task<ActionResult<IEnumerable<Course>>> GetCourses()
        {
            return await _context.Courses.ToListAsync();
        }
    }
}