using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using MVCCore.Models;

namespace MVCCore.DbContext
{
    public class SchoolContext : System.Data.Entity.DbContext
    {
        public SchoolContext(string connString) : base(connString)
        {
        }

        public DbSet<Student> Students { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }
        public DbSet<Course> Courses { get; set; }

        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserRoles> UserRoles { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}
