using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Reflection;

namespace AwesomeCare.DataAccess.Database
{
    public class AwesomeCareDbContext : DbContext, IDbContext
    {

        public AwesomeCareDbContext(DbContextOptions<AwesomeCareDbContext> options):base(options)
        {

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
