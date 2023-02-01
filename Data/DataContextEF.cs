











using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ModelsTutorial.Models;

namespace ModelsTutorial.Data
{

    public class DataContextEF : DbContext
    {
        private IConfiguration _config;

        // Constructer
        // A constructor in C# is a special method of a class that is executed whenever an object of that class is created. 
        // Constructors are used to initialize class fields, set default values, and perform other actions necessary to create an object of a class.
        // In C#, constructors are defined using the same name as the class and have no return type. 
        // They can be parameterized or non-parameterized and can be overloaded to provide multiple ways to create objects of a class.
        public DataContextEF(IConfiguration config)
        {
            _config = config;
        }

        public DbSet<Computer>? Computer { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            if (!options.IsConfigured)
            {
                options.UseSqlServer(_config.GetConnectionString("DefaultConnection"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("TutorialAppSchema");
            modelBuilder.Entity<Computer>()
              // .HasNoKey();
              .HasKey(c => c.ComputerId);
            // .ToTable("Computer", "TutorialAppSchema");
            // .ToTable("TableName", "SchemaName");
        }
    }
}
