using System.Data.Entity.Infrastructure;

namespace MVCCore
{
    public class SchoolContextFactory : IDbContextFactory<SchoolContext>
    {
        public SchoolContext Create()
        {
            return new SchoolContext("Server=localhost\\SQLEXPRESS;Database=EF6MVCCore;Trusted_Connection=True;MultipleActiveResultSets=true");
        }
    }
}
