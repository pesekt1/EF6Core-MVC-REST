﻿using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using MVCCore.Models;

namespace MVCCore
{
    public class SchoolContext : DbContext
    {
        public SchoolContext(string connString) : base(connString)
        {
        }

        public DbSet<Student> Students { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }
        public DbSet<Course> Courses { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}
