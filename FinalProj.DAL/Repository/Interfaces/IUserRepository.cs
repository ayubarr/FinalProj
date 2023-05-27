using FinalApp.Domain.Models.Abstractions.BaseUsers;
using Microsoft.AspNetCore.Identity;

namespace FinalApp.DAL.Repository.Interfaces
{
    public interface IUserRepository<T>
        where T : ApplicationUser
    {
        public Task Create(T entity);
        public Task<IQueryable<T>> ReadAllAsync();
        public Task<T> ReadByIdAsync(string id);
    }
}
