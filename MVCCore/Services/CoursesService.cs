using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MVCCore.Models;

namespace MVCCore.Services
{
    public class CoursesService: ControllerBase
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
        
        public async Task<ActionResult<Course>> CreateCourse(Course course)
        {
            _context.Courses.Add(course);
            await _context.SaveChangesAsync();
            
            return CreatedAtAction(nameof(CreateCourse), new { id = course.CourseID }, course);
        }
        
        public async Task<ActionResult<Course>> DeleteCourse(long id)
        {
            var course = await _context.Courses.FindAsync(id);
            if (course == null)
            {
                return NotFound();
            }

            _context.Courses.Remove(course);
            await _context.SaveChangesAsync();

            return course;
        }
        
        public async Task<ActionResult<Course>> UpdateCourse(long id, Course course)
        {
            if (id != course.CourseID)
            {
                return BadRequest();
            }

            _context.Entry(course).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CourseExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            //return NoContent();
            return await _context.Courses.FindAsync(id);
        }
        
        private bool CourseExists(long id)
        {
            return _context.Courses.Any(e => e.CourseID == id);
        }
    }
}