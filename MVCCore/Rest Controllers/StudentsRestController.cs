
using System;
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
    public class StudentsController : ControllerBase
    {
        private readonly SchoolContext _context;

        public StudentsController(SchoolContext context)
        {
            _context = context;
        }

        // GET: api/Students
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Object>>> GetStudents()
        {
            
            return await _context
                .Students
                .Select(s => new
                {
                    s.ID, 
                    s.FirstMidName, 
                    s.LastName,
                    s.EnrollmentDate,
                    Enrollments = s
                        .Enrollments
                        .Select(e => new
                        {
                            e.EnrollmentID,
                            Course = new {e.Course.Title, e.Course.CourseID, e.Course.Credits},
                            e.Grade
                        })
                        .ToList()
                })
                .ToListAsync();
        }
        
        // GET: api/Students/1
        [HttpGet("{id}")]
        public async Task<ActionResult<Student>> GetStudentById(long id)
        {
            var student = await _context.Students.FindAsync(id);

            if (student == null)
            {
                return NotFound();
            }

            return student;
        }


        // PUT: api/Students/1
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateStudent(long id, Student student)
        {
            if (id != student.ID)
            {
                return BadRequest();
            }

            _context.Entry(student).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StudentExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }
        
        // POST: api/Students
        [HttpPost]
        public async Task<ActionResult<Student>> CreateStudent(Student student)
        {
            _context.Students.Add(student);
            await _context.SaveChangesAsync();

            //return CreatedAtAction("CreateStudent", new { id = student.ID }, student);
            return CreatedAtAction(nameof(CreateStudent), new { id = student.ID }, student);
        }
        
        // DELETE: api/Students/1
        [HttpDelete("{id}")]
        public async Task<ActionResult<Student>> DeleteStudent(long id)
        {
            var student = await _context.Students.FindAsync(id);
            if (student == null)
            {
                return NotFound();
            }

            _context.Students.Remove(student);
            await _context.SaveChangesAsync();

            return student;
        }

        private bool StudentExists(long id)
        {
            return _context.Students.Any(e => e.ID == id);
        }
    }
}
