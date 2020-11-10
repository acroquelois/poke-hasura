using System.Threading.Tasks;

namespace Skeleton.Domain.UnitOfWork.Abstraction
{
    public interface IUnitOfWork
    {
        TSet GetSet<TSet, TEntity>()
            where TSet : class
            where TEntity : class;

        Task<int> CommitAsync();

    }
}