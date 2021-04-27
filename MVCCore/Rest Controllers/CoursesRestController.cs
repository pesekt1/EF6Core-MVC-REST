using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MVCCore.Services;
using MVCCore.Models;

namespace MVCCore.Rest_Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoursesController : ControllerBase
    {
        private readonly CoursesService _coursesService;
        private readonly SchoolContext _context;

        public CoursesController(CoursesService coursesService, SchoolContext context)
        {
            _coursesService = coursesService;
            _context = context;
        }

        // GET: api/Courses
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Course>>> GetCourses()
        {
            return await _coursesService.GetCourses();
        }
        
        // GET: api/Courses/bad
        [HttpGet]
        [Route("bad")]
        public async Task<ActionResult<IEnumerable<Course>>> GetCoursesBad()
        {
            return await _context.Courses.ToListAsync();
        }
    }
}