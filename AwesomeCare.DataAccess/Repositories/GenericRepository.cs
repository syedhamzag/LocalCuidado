using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.DataAccess.Repositories
{
    public class GenericRepository<TEntity, Tkey> : IGenericInterface<TEntity, Tkey> where TEntity:class
    {
        public void DeleteEntity(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public List<TEntity> GetEntities()
        {
            throw new NotImplementedException();
        }

        public TEntity GetEntity(Tkey id)
        {
            throw new NotImplementedException();
        }

        public void InsertEntities(List<TEntity> entities)
        {
            throw new NotImplementedException();
        }

        public TEntity InsertEntity(TEntity entity)
        {
            throw new NotImplementedException();
        }
    }
}
