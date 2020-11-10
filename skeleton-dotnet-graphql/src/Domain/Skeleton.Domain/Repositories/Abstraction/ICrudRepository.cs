using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Skeleton.Domain.Models.Core;

namespace Skeleton.Domain.Repositories.Abstraction
{
    public interface ICrudRepository<TEntity, TKey> where TEntity : BaseEntity<TKey>, new()
    {

        Task<TEntity> GetAsync(TKey id, bool tracking = false);

        Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> filter, bool track = false);
        
        Task<List<TEntity>> ListAsync(Expression<Func<TEntity, bool>> filter = null);

        Task InsertAsync(TEntity entity);

        Task InsertRangeAsync(IEnumerable<TEntity> entities);

        void Update(TEntity entities);
        
        void UpdateRange(IEnumerable<TEntity> entities);

        Task<bool> DeleteAsync(TKey id);

        Task DeleteRangeAsync(Expression<Func<TEntity, bool>> filter);
        
        IQueryable<TEntity> AsQueryable(bool track = false);

    }
}