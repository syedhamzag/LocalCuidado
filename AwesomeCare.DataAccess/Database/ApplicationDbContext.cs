using AwesomeCare.Model.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace AwesomeCare.DataAccess.Database
{
 public  class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            //builder.Entity<Client>()
            //   .HasIndex(p => p.IdNumber)
            //   .IsUnique();
            //var typesToRegister = Assembly.Load("AwesomeCare.Model").GetTypes().
            //  Where(type => !string.IsNullOrEmpty(type.Namespace)).
            //  Where(type => type.GetInterface(typeof(IEntityTypeConfiguration<>).FullName) != null);

            //foreach (var type in typesToRegister)
            //{
            //    dynamic configurationInstance = Activator.CreateInstance(type);
            //    builder.ApplyConfiguration(configurationInstance);

            //}
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);

        }
    }
}
