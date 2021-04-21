using System.Data.Entity;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Threading.Tasks;
using ContosoUniversity;
using ContosoUniversity.Models;
using Microsoft.AspNetCore.Mvc;

namespace MVCCore.Rest_Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoursesController : ControllerBase
    {
        private readonly SchoolContext _context;

        public CoursesController(SchoolContext context)
        {
            _context = context;
        }

        // GET: api/TodoItems
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Course>>> GetStudents()
        {
            return await _context.Courses.ToListAsync();
        }
    }
}