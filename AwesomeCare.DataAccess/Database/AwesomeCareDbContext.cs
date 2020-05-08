using AwesomeCare.Model.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace AwesomeCare.DataAccess.Database
{
    public class AwesomeCareDbContext : IdentityDbContext<ApplicationUser>
    {

        public AwesomeCareDbContext(DbContextOptions<AwesomeCareDbContext> options) : base(options)
        {
            //'DbContextOptionsBuilder.EnableSensitiveDataLogging
            
        }

       
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Client>()
                .HasIndex(p => p.IdNumber)
                .IsUnique();
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

        //async Task<EntityEntry> IDbContext.AddAsync(object entity, CancellationToken cancellationToken)
        //{
        //    var entityEntry = await this.AddAsync(entity);
        //    return entityEntry;
        //}

        //async Task<EntityEntry<TEntity>> IDbContext.AddAsync<TEntity>(TEntity entity, CancellationToken cancellationToken)
        //{
        //    var entityEntry = await this.AddAsync<TEntity>(entity);
        //    return entityEntry;
        //}

        //async Task<TEntity> IDbContext.FindAsync<TEntity>(params object[] keyValues)
        //{
        //    var entity = await this.FindAsync<TEntity>(keyValues);
        //    return entity;
        //}

        //async Task<object> IDbContext.FindAsync(Type entityType, object[] keyValues, CancellationToken cancellationToken)
        //{
        //    var entity = await this.FindAsync(entityType,keyValues);
        //    return entity;
        //}

        //async Task<TEntity> IDbContext.FindAsync<TEntity>(object[] keyValues, CancellationToken cancellationToken)
        //{
        //    var entity = await this.FindAsync<TEntity>(keyValues);
        //    return entity;
        //}

        //async Task<object> IDbContext.FindAsync(Type entityType, params object[] keyValues)
        //{
        //    var entity = await this.FindAsync(entityType, keyValues);
        //    return entity;
        //}
    }
}
