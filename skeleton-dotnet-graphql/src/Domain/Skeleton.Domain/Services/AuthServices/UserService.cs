using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Skeleton.Domain.Models.Users;
using Skeleton.Domain.Repositories.Abstraction;
using Skeleton.Domain.Services.AuthServices.Abstractions;
using Skeleton.Domain.UnitOfWork.Abstraction;

namespace Skeleton.Domain.Services.AuthServices
{
    public class UserService : IUserService
    {
        private readonly ICrudRepository<User, int> _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPasswordHasher<User> _passwordHasher;
        
        public UserService(ICrudRepository<User, int> repository, IUnitOfWork unitOfWork, IPasswordHasher<User> passwordHasher)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _passwordHasher = passwordHasher;
        }
        
        public async Task<User> InsertAsync(User entity)
        {
            entity.Password = _passwordHasher.HashPassword(new User(), entity.Password);
            await _repository.InsertAsync(entity);
            await _unitOfWork.CommitAsync();
            return entity;
        }
        
        public async Task<User> GetAsync(Expression<Func<User, bool>> filter, bool track = false)
        {
            return await _repository.GetAsync(filter);
        }
        
        public async Task<User> GetAsync(int id, bool tracking = false)
        {
            return await _repository.GetAsync(id);
        }
        
        public async Task<User> Update(User entity)
        {
            _repository.Update(entity);
            await _unitOfWork.CommitAsync();
            return entity;
        }
        
    }
}