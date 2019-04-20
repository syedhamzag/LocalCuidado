using System.Collections.Generic;

namespace AwesomeCare.DataAccess.Repositories
{
    public interface IGenericInterface<TEntity, TKey> where TEntity : class
    {
        TEntity GetEntity(TKey id);
        List<TEntity> GetEntities();
        TEntity InsertEntity(TEntity entity);
        void InsertEntities(List<TEntity> entities);
        void DeleteEntity(TEntity entity);
    }
}
