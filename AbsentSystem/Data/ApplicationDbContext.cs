using AbsentSystem.Data.Configurations;
using AbsentSystem.Data.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AbsentSystem.Data
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public ApplicationDbContext(DbContextOptions options)
            : base(options)
        {
        }

        public DbSet<AttendanceList> AttendanceLists { get; set; }
        public DbSet<TypeVacation> TypeVacations { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            
            builder.ApplyConfiguration(new UserConfig());
            builder.ApplyConfiguration(new AttendanceConfig());
            builder.ApplyConfiguration(new VacationsConfig());
            base.OnModelCreating(builder);
        }
    }
}