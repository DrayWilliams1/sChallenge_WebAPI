using Microsoft.EntityFrameworkCore;

namespace sChallenge.Models
{
    public class ApiContext : DbContext
    {
        public ApiContext(DbContextOptions<ApiContext> options) : base(options) { }

        public DbSet<User> User { get; set; } // to facilitate database operations with Users

        public DbSet<Patient> Patient { get; set; } // to facilitate database operations with Patients
    }
}
