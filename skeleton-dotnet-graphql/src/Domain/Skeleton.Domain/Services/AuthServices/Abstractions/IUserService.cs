using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Skeleton.Domain.Models.Users;

namespace Skeleton.Domain.Services.AuthServices.Abstractions
{
    public interface IUserService
    {
        Task<User> InsertAsync(User entity);

        Task<User> GetAsync(Expression<Func<User, bool>> filter, bool track = false);
        Task<User> Update(User entity);

        Task<User> GetAsync(int id, bool tracking = false);
    }
}