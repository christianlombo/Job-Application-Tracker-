using JobApplicationTracker.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace JobApplicationTracker.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<JobApplication> JobApplications { get; set; }
        public DbSet<User> Users { get; set; } // Add Users table
        public DbSet<Login> Logins { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>()
              .HasKey(u => u.UserId);

            modelBuilder.Entity<JobApplication>()
                .HasIndex(j => j.UserId);

            modelBuilder.Entity<JobApplication>()
                .Property(j => j.CompanyName)
                .IsRequired()
                .HasMaxLength(100);

            modelBuilder.Entity<JobApplication>()
                .Property(j => j.JobTitle)
                .IsRequired()
                .HasMaxLength(100);
        }
    }
}
