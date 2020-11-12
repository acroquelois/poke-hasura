using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Skeleton.Domain.Models;

namespace Skeleton.Domain.Services
{
    public interface IQuestionService
    {
        Task<Question> GetAsync(int id, bool tracking = false);

        Task<Question> GetAsync(Expression<Func<Question, bool>> filter, bool track = false);
        
        Task<List<Question>> ListAsync(Expression<Func<Question, bool>> filter = null);

        Task<Question> InsertAsync(Question entity);

        Task InsertRangeAsync(IEnumerable<Question> entities);

        Task<Question> Update(Question entities);
        
        Task UpdateRange(IEnumerable<Question> entities);

        Task<bool> DeleteAsync(int id);

        Task DeleteRangeAsync(Expression<Func<Question, bool>> filter);
        
        IQueryable<Question> AsQueryable(bool track = false);
    }
}