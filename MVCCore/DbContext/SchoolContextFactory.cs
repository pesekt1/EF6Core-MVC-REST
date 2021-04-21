﻿using System.Data.Entity.Infrastructure;

namespace ContosoUniversity
{
    public class SchoolContextFactory : IDbContextFactory<SchoolContext>
    {
        public SchoolContext Create()
        {
            return new SchoolContext("Server=localhost\\SQLEXPRESS;Database=EF6MVCCoreExample;Trusted_Connection=True;MultipleActiveResultSets=true");
        }
    }
}
