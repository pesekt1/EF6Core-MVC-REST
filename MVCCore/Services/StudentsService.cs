using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MVCCore.DbContext;
//using Microsoft.EntityFrameworkCore; //this is an alternative to System.Data.Entity
using MVCCore.Models;

namespace MVCCore.Services
{
    public class StudentsService : ControllerBase
    {
        private readonly SchoolContext _context;

        public StudentsService(SchoolContext context)
        {
            _context = context;
        }
        
        public async Task<ActionResult<IEnumerable<Student>>> GetStudents()
        {
            return await _context.Students.ToListAsync();
        }
        
        public async Task<ActionResult<IEnumerable<Object>>> GetStudentsWithSelect()
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
        
        public async Task<ActionResult<IEnumerable<Object>>> GetStudentsSP()
        {
            var data = _context.Database.SqlQuery<Student>("exec sp_getAllStudents");
            return await data.ToListAsync();
        }
        
        public async Task<ActionResult<IEnumerable<Object>>> GetStudentsByIdSP(int id)
        {
            var data = _context.Database.SqlQuery<Student>("exec sp_getStudentById @id", new SqlParameter("id", id));
            return await data.ToListAsync();
        }
        
        public async Task<ActionResult<Student>> GetStudentById(long id)
        {
            var student = await _context.Students.FindAsync(id);

            if (student == null)
            {
                return NotFound();
            }

            return student;
        }
        
        public async Task<ActionResult<IEnumerable<Object>>> CreateStudentSP(String LastName, String FirstMidName, DateTime EnrollmentDate)
        {
            // await _context.Database.ExecuteSqlCommandAsync(
            //      "exec sp_createStudent @LastName, @FirstMidName,@EnrollmentDate", 
            //      new SqlParameter("LastName", LastName),
            //      new SqlParameter("FirstMidName", FirstMidName),
            //      new SqlParameter("EnrollmentDate", EnrollmentDate)
            //  );
            
            var data = _context.Database.SqlQuery<Student>(
                "exec sp_createStudent @LastName, @FirstMidName,@EnrollmentDate", 
                    new SqlParameter("LastName", LastName),
                    new SqlParameter("FirstMidName", FirstMidName),
                    new SqlParameter("EnrollmentDate", EnrollmentDate)
                );
            return await data.ToListAsync();
        }
        
        //Expected student object structure: there must be the id
        // {
        //     "id": 5,
        //     "lastName": "string"
        //     ...
        // }
        
        //if we want to return the updated object we need type Task<ActionResult<Student>>
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
        
        //Expected student object structure:
        // {
        //     "lastName": "string",
        //     "firstMidName": "string",
        //     "enrollmentDate": "2021-05-04T08:38:37.287Z"
        // }
        public async Task<ActionResult<Student>> CreateStudent(Student student)
        {
            _context.Students.Add(student);
            await _context.SaveChangesAsync();

            //return CreatedAtAction("CreateStudent", new { id = student.ID }, student);
            return CreatedAtAction(nameof(CreateStudent), new { id = student.ID }, student);
        }
        
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