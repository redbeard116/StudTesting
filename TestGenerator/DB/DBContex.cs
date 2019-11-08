using Microsoft.EntityFrameworkCore;
using TestGenerator.Model;

namespace TestGenerator.DB
{
    public class DBContex: DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Server=127.0.0.1;User Id=postgres;Password=nagimullin;Database=testgenerator;");
        }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<StudentProgress> StudProgress { get; set; }
        public DbSet<Test> Tests { get; set; }
    }
}
