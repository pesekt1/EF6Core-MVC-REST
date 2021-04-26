#MVC Core project:

### SQL Server database:
- connection string in appsettings.json:
 ```c#
      "ConnectionStrings": {
        "DefaultConnection": "Server=localhost\\SQLEXPRESS;Database=EF6MVCCore;Trusted_Connection=True;MultipleActiveResultSets=true"
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

### Scaffolding - generating the mapping classes from existing database
For generating model classes from the existing database we can use scaffolding:
Run something like this in your Package Manager Console in Visual Studio:


    Scaffold-DbContext "Data Source=localhost\SQLEXPRESS;Initial Catalog=SchoolContext6;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Models2

- We need to specify:
    - data source
    - database (initial catalog)
    - output directory

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

### Swagger