using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace SwiftTalk.Data
{
    public class AuthDbContextFactory : IDesignTimeDbContextFactory<AuthDbContext>
    {
        public AuthDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<AuthDbContext>();
            optionsBuilder.UseSqlServer("Server=Amonezeer;Database=AuthAppDB_New;Integrated Security=True;TrustServerCertificate=True;");

            return new AuthDbContext(optionsBuilder.Options);
        }
    }
}