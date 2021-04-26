
using System;
using System.Data.Entity;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;
using MVCCore.Services;
using MVCCore.Models;

namespace MVCCore.Rest_Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly SchoolContext _context;
        private readonly StudentsService _studentsService;

        public StudentsController(SchoolContext context, StudentsService studentsService)
        {
            _context = context;
            _studentsService = studentsService;
        }

        // GET: api/Students
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Object>>> GetStudents()
        {
            return await _studentsService.GetStudents();
        }
        
        //using stored procedure
        // GET: api/Students/getStudentsSP
        [HttpGet]
        [Route("getStudentsSP")]
        public async Task<ActionResult<IEnumerable<Object>>> GetStudentsSP()
        {
            return await _studentsService.GetStudentsSP();
        }
        
        //using stored procedure with a parameter
        // GET: api/Students/getStudentsSP
        [HttpGet]
        [Route("getStudentsSP/{id}")]
        public async Task<ActionResult<IEnumerable<Object>>> GetStudentsByIdSP(int id)
        {
            return await _studentsService.GetStudentsByIdSP(id);
        }
        
        // GET: api/Students/bad - example of a loop
        [HttpGet]
        [Route("bad")]
        public async Task<ActionResult<IEnumerable<Student>>> GetStudentsBad()
        {
            return await _context.Students.ToListAsync();
        }
        
        // GET: api/Students/1
        [HttpGet("{id}")]
        public async Task<ActionResult<Student>> GetStudentById(long id)
        {
            return await _studentsService.GetStudentById(id);
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
        
        // POST: api/Students/createStudentSP
        [HttpPost]
        [Route("createStudentSP")]
        public void CreateStudentSP(String LastName, String FirstMidName, DateTime EnrollmentDate)
        {
            //Student student = new Student(LastName, FirstMidName, EnrollmentDate);
            _studentsService.GreateStudentSP(LastName, FirstMidName, EnrollmentDate);
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
