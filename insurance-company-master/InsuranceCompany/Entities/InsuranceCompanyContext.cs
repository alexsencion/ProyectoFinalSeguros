using Microsoft.EntityFrameworkCore;

namespace InsuranceCompany.Entities
{
    public sealed class InsuranceCompanyContext : DbContext
    {
        public InsuranceCompanyContext(string connString) : base (GetOptions(connString))
        {
        }

        private static DbContextOptions GetOptions(string connectionString)
        {
            return new DbContextOptionsBuilder().UseSqlServer(connectionString).Options;
        }

        public DbSet<Client> Clients { get; set; }
        public DbSet<Policy> Policies { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
    }
}