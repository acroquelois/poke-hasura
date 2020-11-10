using System.Threading.Tasks;
using Skeleton.Domain.UnitOfWork.Abstraction;

namespace Skeleton.Internal.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly SkeletonApiContext _dbContext;

        public UnitOfWork(SkeletonApiContext dbContext)
        {
            _dbContext = dbContext;
        }
        
        public TSet GetSet<TSet, TEntity>() where TSet : class where TEntity : class
        {
            return _dbContext.Set<TEntity>() as TSet;
        }

        public Task<int> CommitAsync()
        {
            return _dbContext.SaveChangesAsync();
        }
    }
}