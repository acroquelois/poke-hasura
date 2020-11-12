using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Skeleton.Domain.Models;
using Skeleton.Domain.Models.Core;
using Skeleton.Domain.Repositories.Abstraction;
using Skeleton.Domain.UnitOfWork.Abstraction;
using Skeleton.Internal.UnitOfWork;

namespace Skeleton.Internal.Repositories.Core
{
    public class QuestionRepository: IQuestionRepository
    {
        private readonly IUnitOfWork _unitOfWork;
        protected readonly DbSet<Question> Set;

        public QuestionRepository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            Set = unitOfWork.GetSet<DbSet<Question>, Question>();
        }

        public virtual Task<Question> GetAsync(int id, bool track = false)
        {
            var query = track ? Set : Set.AsNoTracking();

            return query
                .Include(x => x.QuestionAnswer)
                .Include(x => x.QuestionCategorie)
                .FirstOrDefaultAsync(entity => id.Equals(entity.Id));
        }

        public virtual Task<Question> GetAsync(Expression<Func<Question, bool>> filter, bool track = false)
        {
            var query = track ? Set : Set.AsNoTracking();
            
            return query
                .Include(x => x.QuestionAnswer)
                .Include(x => x.QuestionCategorie)
                .FirstOrDefaultAsync(filter);
        }

        public virtual Task<List<Question>> ListAsync(Expression<Func<Question, bool>> filter)
        {
            return Set
                .AsNoTracking()
                .Include(x => x.QuestionAnswer)
                .Include(x => x.QuestionCategorie)
                .Where(filter ?? (_ => true))
                .OrderBy(o => o.Id)
                .ToListAsync();
        }

        public async Task InsertAsync(Question entity)
        { 
            await Set.AddAsync(entity);
        }

        public async Task InsertRangeAsync(IEnumerable<Question> entity)
        {
            await Set.AddRangeAsync(entity);
        }

        public void Update(Question entity)
        {
            Set.Update(entity);
        }

        public void UpdateRange(IEnumerable<Question> entities)
        {
            Set.UpdateRange(entities);
        }

        public IQueryable<Question> AsQueryable(bool track = false)
        {
            return track ? Set : Set.AsNoTracking();
        }

        public async Task<bool> DeleteAsync(int id)
        {
            Question entity = await  Set.FirstOrDefaultAsync(x => x.Id.Equals(id));

            if (entity == null) return false;

            Set.Remove(entity);

            return true;
        }

        public async Task DeleteRangeAsync(Expression<Func<Question, bool>> filter)
        {
            IEnumerable<Question> query = await Set.Where(filter).ToListAsync();

            Set.RemoveRange(query);
        }
    }
}