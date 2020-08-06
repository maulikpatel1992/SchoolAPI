
using Entities.Configuration;
using Entities.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Entities
{
    public class RepositoryContext : IdentityDbContext<UserAuth>
    {
        public RepositoryContext(DbContextOptions options)
        : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new SecEnrollmentMgtConfiguration());
            modelBuilder.ApplyConfiguration(new CourseSectionMgtConfiguration());
            modelBuilder.ApplyConfiguration(new CourseMgtConfiguration());
            modelBuilder.ApplyConfiguration(new SecAssignmentMgtConfiguration());
            modelBuilder.ApplyConfiguration(new AssignmentConfiguration());
            modelBuilder.ApplyConfiguration(new RoleConfiguration());

        }

        public DbSet<User> User { get; set; }
        public DbSet<SecEnrollmentMgt> SecEnrollmentMgt { get; set; }
        public DbSet<CourseSectionMgt> CourseSectionMgt { get; set; }
        public DbSet<CourseMgt> CourseMgt { get; set; }
        public DbSet<SecAssignmentMgt> SecAssignmentMgt { get; set; }
        public DbSet<Assignment> Assignment { get; set; }

    }
}
