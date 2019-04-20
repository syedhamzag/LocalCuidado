using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Reflection;

namespace AwesomeCare.DataAccess.Database
{
    public class AwesomeCareDbContext : DbContext
    {

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=HQ-TECH-L048;Initial Catalog=AwesomeCareCMSDb;User ID=sa;Password=olamide@123");

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {


            var typesToRegister = Assembly.Load("AwesomeCare.Model").GetTypes().
              Where(type => !string.IsNullOrEmpty(type.Namespace)).
              Where(type => type.GetInterface(typeof(IEntityTypeConfiguration<>).FullName) != null);

            foreach (var type in typesToRegister)
            {
                dynamic configurationInstance = Activator.CreateInstance(type);
                modelBuilder.ApplyConfiguration(configurationInstance);

            }
            base.OnModelCreating(modelBuilder);

        }
    }
}
