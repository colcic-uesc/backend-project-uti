using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using UescColcicAPI.Core;

namespace UescColcicAPI.Services.BD
{
    public class UescColcicAPIDbContext : DbContext
    {
        private readonly IConfiguration _configuration;

        
        public UescColcicAPIDbContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            
            var connectionString = _configuration.GetConnectionString("UescColcicDb");
            optionsBuilder.UseMySql(connectionString, new MySqlServerVersion(new Version(8, 0, 21)));
        }

        public DbSet<Professor> Professors { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Skill> Skills { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Professor>().HasKey(x => x.ProfessorId);
            modelBuilder.Entity<Professor>().HasMany<Project>(x => x.Projects).WithOne(x => x.Professor).HasForeignKey(x => x.ProfessorId);

            modelBuilder.Entity<Project>().HasKey(x => x.ProjectId);
            modelBuilder.Entity<Student>().HasKey(x => x.StudentId);
            modelBuilder.Entity<Skill>().HasKey(x => x.SkillId);
        }
    }
}
