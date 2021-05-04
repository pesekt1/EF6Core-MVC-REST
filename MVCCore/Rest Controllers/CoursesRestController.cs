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

        // GET: api/courses
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Course>>> GetCourses()
        {
            return await _coursesService.GetCourses();
        }
        
        // PUT: api/courses/1
        [HttpPut("{id}")]
        public async Task<ActionResult<Course>> UpdateCourse(long id, Course course)
        {
            return await _coursesService.UpdateCourse(id, course);
        }
        
        // POST: api/courses
        [HttpPost]
        public async Task<ActionResult<Course>> CreateCourse(Course course)
        {
            return await _coursesService.CreateCourse(course);
        }
        
        // DELETE: api/courses/1
        [HttpDelete("{id}")]
        public async Task<ActionResult<Course>> DeleteCourse(long id)
        {
            return await _coursesService.DeleteCourse(id);
        }
    }
}