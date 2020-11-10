using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Skeleton.Domain.Models.Core;
using Skeleton.Domain.Repositories.Abstraction;
using Skeleton.Domain.UnitOfWork.Abstraction;

namespace Skeleton.Domain.Services.Core
{
    public class CrudService<TEntity, TKey> : ICrudService<TEntity, TKey>
        where TEntity : BaseEntity<TKey>, new()
    {
        private readonly ICrudRepository<TEntity, TKey> _repository;
        private readonly IUnitOfWork _unitOfWork;
        
        public CrudService(
            ICrudRepository<TEntity, TKey> repository,
            IUnitOfWork unitOfWork
            )
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }
        
        public virtual async Task<TEntity> GetAsync(TKey id, bool tracking = false)
        {
            return await _repository.GetAsync(id);
        }

        public virtual async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> filter, bool track)
        {
            return await _repository.GetAsync(filter);
        }

        public virtual async Task<List<TEntity>> ListAsync(Expression<Func<TEntity, bool>> filter = null)
        {
            return await _repository.ListAsync(filter);
        }

        public virtual async Task<TEntity> InsertAsync(TEntity entity)
        {
            await _repository.InsertAsync(entity);
            await _unitOfWork.CommitAsync();
            return entity;
        }

        public virtual async Task InsertRangeAsync(IEnumerable<TEntity> entities)
        {
            await _repository.InsertRangeAsync(entities);
            await _unitOfWork.CommitAsync();
        }

        public virtual async Task<TEntity> Update(TEntity entity)
        {
            _repository.Update(entity);
            await _unitOfWork.CommitAsync();
            return entity;
        }

        public virtual async Task UpdateRange(IEnumerable<TEntity> entities)
        {
            _repository.UpdateRange(entities);
            await _unitOfWork.CommitAsync();
        }

        public virtual async Task<bool> DeleteAsync(TKey id)
        {
            var response = await _repository.DeleteAsync(id);
            await _unitOfWork.CommitAsync();
            return response;
        }

        public async Task DeleteRangeAsync(Expression<Func<TEntity, bool>> filter)
        {
            await _repository.DeleteRangeAsync(filter);
            await _unitOfWork.CommitAsync();
        }

        public IQueryable<TEntity> AsQueryable(bool track = false)
        {
            return _repository.AsQueryable();
        }
    }
}