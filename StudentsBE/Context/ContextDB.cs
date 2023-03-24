using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using StudentsBE.Migrations;
using StudentsBE.Models;

namespace StudentsBE.Context
{
    public class ContextDB : IdentityDbContext
    {
        public ContextDB(DbContextOptions<ContextDB> options) : base (options)
        { 
        
        }
        
        public DbSet<Student> Students { get; set; }

        public DbSet<Subject> Subjects { get; set; }

        public DbSet<Hability> Habilities { get; set; }

        public DbSet<StudentSubjectHability> StudentSubjectHabilities { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Student>().ToTable("Student");

            modelBuilder.Entity<Subject>().ToTable("Subject");

            modelBuilder.Entity<Hability>().ToTable("Hability");

            modelBuilder.Entity<StudentSubjectHability>().ToTable("StudentSubjectHability");

            modelBuilder.ApplyConfiguration(new SubjectConfiguration());

            modelBuilder.ApplyConfiguration(new HabilitiesConfiguration());
            
            base.OnModelCreating(modelBuilder);

    }

        
    }
}
