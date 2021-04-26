using System.Collections.Generic;
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

        public CoursesController(CoursesService coursesService)
        {
            _coursesService = coursesService;
        }
        
        // private readonly SchoolContext _context;
        //
        // public CoursesController(SchoolContext context)
        // {
        //     _context = context;
        // }

        // GET: api/TodoItems
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Course>>> GetCourses()
        {
            return await _coursesService.GetCourses();
        }
    }
}