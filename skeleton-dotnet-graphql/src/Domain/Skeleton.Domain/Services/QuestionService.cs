using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Skeleton.Domain.Models;
using Skeleton.Domain.Models.Core;
using Skeleton.Domain.Repositories.Abstraction;
using Skeleton.Domain.UnitOfWork.Abstraction;

namespace Skeleton.Domain.Services
{
    public class QuestionService: IQuestionService
    {
        private readonly IQuestionRepository _repository;
        private readonly IUnitOfWork _unitOfWork;
        
        public QuestionService(
            IQuestionRepository repository,
            IUnitOfWork unitOfWork
            )
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }
        
        public virtual async Task<Question> GetAsync(int id, bool tracking = false)
        {
            return await _repository.GetAsync(id);
        }

        public virtual async Task<Question> GetAsync(Expression<Func<Question, bool>> filter, bool track)
        {
            return await _repository.GetAsync(filter);
        }

        public virtual async Task<List<Question>> ListAsync(Expression<Func<Question, bool>> filter = null)
        {
            return await _repository.ListAsync(filter);
        }

        public virtual async Task<Question> InsertAsync(Question entity)
        {
            await _repository.InsertAsync(entity);
            await _unitOfWork.CommitAsync();
            return entity;
        }

        public virtual async Task InsertRangeAsync(IEnumerable<Question> entities)
        {
            await _repository.InsertRangeAsync(entities);
            await _unitOfWork.CommitAsync();
        }

        public virtual async Task<Question> Update(Question entity)
        {
            _repository.Update(entity);
            await _unitOfWork.CommitAsync();
            return entity;
        }

        public virtual async Task UpdateRange(IEnumerable<Question> entities)
        {
            _repository.UpdateRange(entities);
            await _unitOfWork.CommitAsync();
        }

        public virtual async Task<bool> DeleteAsync(int id)
        {
            var response = await _repository.DeleteAsync(id);
            await _unitOfWork.CommitAsync();
            return response;
        }

        public async Task DeleteRangeAsync(Expression<Func<Question, bool>> filter)
        {
            await _repository.DeleteRangeAsync(filter);
            await _unitOfWork.CommitAsync();
        }

        public IQueryable<Question> AsQueryable(bool track = false)
        {
            return _repository.AsQueryable();
        }
    }
}