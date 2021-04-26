using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using ContosoUniversity;
using ContosoUniversity.Models;
using Microsoft.AspNetCore.Mvc;

namespace MVCCore.Services
{
    public class StudentsService : ControllerBase
    {
        private readonly SchoolContext _context;

        public StudentsService(SchoolContext context)
        {
            _context = context;
        }
        
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
        
        public async Task<ActionResult<IEnumerable<Object>>> GetStudentsSP()
        {
            var data = _context.Database.SqlQuery<Student>("getAllStudentsSP");
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
        
    }
}