using System.Threading.Tasks;
using Skeleton.Domain.Enum;
using Skeleton.Domain.Models.Users;

namespace Skeleton.Domain.Services.AuthServices.Abstractions
{
    public interface IAuthService
    {
        Task<(AuthError, string Token)> LoginAsync(User userAuth);

        Task<string> CreateResetToken(string mail);
    }
}