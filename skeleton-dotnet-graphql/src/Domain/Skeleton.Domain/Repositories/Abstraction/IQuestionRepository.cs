using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Skeleton.Domain.Models;
using Skeleton.Domain.Models.Core;

namespace Skeleton.Domain.Repositories.Abstraction
{
    public interface IQuestionRepository
    {

        Task<Question> GetAsync(int id, bool tracking = false);

        Task<Question> GetAsync(Expression<Func<Question, bool>> filter, bool track = false);
        
        Task<List<Question>> ListAsync(int? limit, Expression<Func<Question, bool>> filter = null);

        Task InsertAsync(Question entity);

        Task InsertRangeAsync(IEnumerable<Question> entities);

        void Update(Question entities);
        
        void UpdateRange(IEnumerable<Question> entities);

        Task<bool> DeleteAsync(int id);

        Task DeleteRangeAsync(Expression<Func<Question, bool>> filter);
        
        IQueryable<Question> AsQueryable(bool track = false);

    }
}