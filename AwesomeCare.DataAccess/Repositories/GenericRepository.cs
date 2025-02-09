﻿using AwesomeCare.DataAccess.Database;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace AwesomeCare.DataAccess.Repositories
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        private AwesomeCareDbContext _dbContext;
        private DbSet<TEntity> _entities;
        public GenericRepository(AwesomeCareDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IQueryable<TEntity> Table => Entities;

        public async Task DeleteEntity(TEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException("entity");

            Entities.Remove(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteEntities(List<int> entityIds)
        {
            if (entityIds == null || entityIds.Count == 0)
                throw new ArgumentNullException("entity");

            var entities =await Entities.FindAsync(entityIds);
          //  Entities..Remove(entity);
            //await _dbContext.SaveChangesAsync();
        }

        public async Task<List<TEntity>> GetEntities()
        {
            return await Entities.ToListAsync();
        }

        public async Task<TEntity> GetEntity(object id)
        {
            return await Entities.FindAsync(id) as TEntity;
        }
        public async Task<TEntity> GetEntityWithRelatedEntity<TRelatedProperty>(Expression<Func<TEntity, TRelatedProperty>> includeExpression,Expression<Func<TEntity, bool>>  firstOrDefault)
        {            
            return await Entities.Include(includeExpression).FirstOrDefaultAsync(firstOrDefault) as TEntity;
        }
        public async Task InsertEntities(List<TEntity> entities)
        {
            if (entities == null || entities.Count == 0)
                throw new ArgumentNullException("entities");

            await Entities.AddRangeAsync(entities);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<TEntity> InsertEntity(TEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException("entity");

            await Entities.AddAsync(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }

        public async Task<TEntity> UpdateEntity(TEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException("entity");
            
            Entities.Update(entity);           
            await _dbContext.SaveChangesAsync();
            return entity;
        }

        private DbSet<TEntity> Entities
        {
            get
            {
                if (_entities == null)
                {
                    _entities = _dbContext.Set<TEntity>();
                }
                return _entities;
            }
        }
    }
}
