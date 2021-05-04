
using System;
using System.Collections.Generic;
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
        private readonly StudentsService _studentsService;

        public StudentsController( StudentsService studentsService)
        {
            _studentsService = studentsService;
        }

        // GET: api/students
        [HttpGet]
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<IEnumerable<Student>>> GetStudents()
        {
            return await _studentsService.GetStudents();
        }
        
        // GET: api/Students/withSelect
        [HttpGet]
        [Route("withSelect")]
        public async Task<ActionResult<IEnumerable<Object>>> GetStudentsWithSelect()
        {
            return await _studentsService.GetStudentsWithSelect();
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
            return await _studentsService.UpdateStudent(id, student);
        }
        
        // POST: api/Students
        [HttpPost]
        public async Task<ActionResult<Student>> CreateStudent(Student student)
        {
            return await _studentsService.CreateStudent(student);
        }
        
        // POST: api/Students/createStudentSP
        [HttpPost]
        [Route("createStudentSP")]
        public async Task<ActionResult<IEnumerable<Object>>> CreateStudentSP(String LastName, String FirstMidName, DateTime EnrollmentDate)
        {
            return await _studentsService.CreateStudentSP(LastName, FirstMidName, EnrollmentDate);
        }
        
        // DELETE: api/Students/1
        [HttpDelete("{id}")]
        public async Task<ActionResult<Student>> DeleteStudent(long id)
        {
            return await _studentsService.DeleteStudent(id);
        }
    }
}
