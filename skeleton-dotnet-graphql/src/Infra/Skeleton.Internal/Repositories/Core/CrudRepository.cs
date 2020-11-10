using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Skeleton.Domain.Models.Core;
using Skeleton.Domain.Repositories.Abstraction;
using Skeleton.Domain.UnitOfWork.Abstraction;
using Skeleton.Internal.UnitOfWork;

namespace Skeleton.Internal.Repositories.Core
{
    public class CrudRepository<TEntity, TKey> : ICrudRepository<TEntity, TKey>
        where TEntity : BaseEntity<TKey>, new()
    {
        private readonly IUnitOfWork _unitOfWork;
        protected readonly DbSet<TEntity> Set;

        public CrudRepository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            Set = unitOfWork.GetSet<DbSet<TEntity>, TEntity>();
        }

        public virtual Task<TEntity> GetAsync(TKey id, bool track = false)
        {
            var query = track ? Set : Set.AsNoTracking();

            return query
                .FirstOrDefaultAsync(entity => id.Equals(entity.Id));
        }

        public virtual Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> filter, bool track = false)
        {
            var query = track ? Set : Set.AsNoTracking();
            
            return query.AsNoTracking()
                .FirstOrDefaultAsync(filter);
        }

        public virtual Task<List<TEntity>> ListAsync(Expression<Func<TEntity, bool>> filter)
        {
            return Set
                .AsNoTracking()
                .Where(filter ?? (_ => true))
                .OrderBy(o => o.Id)
                .ToListAsync();
        }

        public async Task InsertAsync(TEntity entity)
        { 
            await Set.AddAsync(entity);
        }

        public async Task InsertRangeAsync(IEnumerable<TEntity> entity)
        {
            await Set.AddRangeAsync(entity);
        }

        public void Update(TEntity entity)
        {
            Set.Update(entity);
        }

        public void UpdateRange(IEnumerable<TEntity> entities)
        {
            Set.UpdateRange(entities);
        }

        public IQueryable<TEntity> AsQueryable(bool track = false)
        {
            return track ? Set : Set.AsNoTracking();
        }

        public async Task<bool> DeleteAsync(TKey id)
        {
            TEntity entity = await  Set.FirstOrDefaultAsync(x => x.Id.Equals(id));

            if (entity == null) return false;

            Set.Remove(entity);

            return true;
        }

        public async Task DeleteRangeAsync(Expression<Func<TEntity, bool>> filter)
        {
            IEnumerable<TEntity> query = await Set.Where(filter).ToListAsync();

            Set.RemoveRange(query);
        }
    }
}