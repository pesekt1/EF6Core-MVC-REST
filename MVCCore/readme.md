#MVC Core project:

### SQL Server database:
- connection string in appsettings.json:
 ```c#
      "ConnectionStrings": {
        "DefaultConnection": "Server=localhost\\SQLEXPRESS;Database=EF6MVCCore;Trusted_Connection=True;MultipleActiveResultSets=true"
      }
 ```     

### Scaffolding - generating the mapping classes from existing database
For generating model classes from the existing database we can use scaffolding:
Run something like this in your Package Manager Console in Visual Studio:


    Scaffold-DbContext "Data Source=localhost\SQLEXPRESS;Initial Catalog=SchoolContext6;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Models2

- We need to specify:
    - data source
    - database (initial catalog)
    - output directory

### Database Mapping
- Models mapping tables
```c#
    public class Student
    {
        public int ID { get; set; }
        public string LastName { get; set; }
        public string FirstMidName { get; set; }
        public DateTime EnrollmentDate { get; set; }

        public virtual ICollection<Enrollment> Enrollments { get; set; }
    }
```

- DbContext defining the schema - DbSets:
```c#
    public class SchoolContext : DbContext
    {
        public DbSet<Student> Students { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }
        public DbSet<Course> Courses { get; set; }
    }
```

### Migration

- Creating the initial schema structure - in case there is no database:
```c#
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Course",
                c => new
                {
                    CourseID = c.Int(nullable: false),
                    Title = c.String(),
                    Credits = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.CourseID);
//... creation of the other tables
```

### Rest Controllers - returning data as json object

- Rest Controllers are using the service layer
```c#
    [Route("api/[controller]")]
    [ApiController]
    public class CoursesController : ControllerBase
    {
        private readonly CoursesService _coursesService;

        public CoursesController(CoursesService coursesService)
        {
            _coursesService = coursesService;
        }

        // GET: api/TodoItems
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Course>>> GetCourses()
        {
            return await _coursesService.GetCourses();
        }
    }
```

- Service layer works with the DbContext

```c#
    public class CoursesService
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
    }
```

### Startup

- Configure services - like services for REST Controllers, and Swagger API documentation
```c#
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
            services.AddScoped<SchoolContext>(_ => 
                new SchoolContext(Configuration.GetConnectionString("DefaultConnection")));
            services.AddScoped<CoursesService>();
            services.AddScoped<StudentsService>();

            // Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerGen();
        }
```

- Enable Swagger API documentation
```c#
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
        // some more code...

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });
        }
```

### Swagger (API documentation)
- Swagger json endpoint: <localhost:5001/swagger/v1/swagger.json>
- Swagger UI: <localhost:5001/swagger>

![swagger](img/swagger.png)

### Stored procedures - executing from the application

- Service layer:
    - Executing stored procedure getAllStudentsSp:

```c#
        public async Task<ActionResult<IEnumerable<Object>>> GetStudentsSP()
        {
            var data = _context.Database.SqlQuery<Student>("getAllStudentsSP");
            return await data.ToListAsync();
        }
```

- Rest Controller:
```c#
        [HttpGet]
        [Route("getStudentsSP")]
        public async Task<ActionResult<IEnumerable<Object>>> GetStudentsSP()
        {
            return await _studentsService.GetStudentsSP();
        }
```

- Stored procedure with parameter:
```c#
        public async Task<ActionResult<IEnumerable<Object>>> GetStudentsByIdSP(int id)
        {
            var data = _context.Database.SqlQuery<Student>("exec sp_getStudentById @id", new SqlParameter("id", id));
            return await data.ToListAsync();
        }
```

- Stored procedure for inserting a record
    - procedure looks like this:
```sql
CREATE PROCEDURE sp_createStudent(
	@LastName AS VARCHAR(50),
	@FirstMidName AS VARCHAR(50),
	@EnrollmentDate AS DATE
	)
AS
BEGIN
	INSERT INTO Student values(@LastName, @FirstMidName, @EnrollmentDate)
END;
go
```
- service layer:
```c#
        public void GreateStudentSP(String LastName, String FirstMidName, DateTime EnrollmentDate)
        {
            _context.Database.ExecuteSqlCommand(
                "exec sp_createStudent @LastName, @FirstMidName,@EnrollmentDate", 
                    new SqlParameter("LastName", LastName),
                    new SqlParameter("FirstMidName", FirstMidName),
                    new SqlParameter("EnrollmentDate", EnrollmentDate)
                );
        }
```
- rest controller:
```c#
        // POST: api/Students/createStudentSP
        [HttpPost]
        [Route("createStudentSP")]
        public void CreateStudentSP(String LastName, String FirstMidName, DateTime EnrollmentDate)
        {
            //Student student = new Student(LastName, FirstMidName, EnrollmentDate);
            _studentsService.GreateStudentSP(LastName, FirstMidName, EnrollmentDate);
        }
```

### MVC architecture ... controllers returning the view - http web page
```c#
        // GET: Students
        public async Task<IActionResult> Index()
        {
            return View(await _context.Students.ToListAsync());
        }
```