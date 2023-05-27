using FinalApp.DAL.Repository.Interfaces;
using FinalApp.DAL.SqlServer;
using FinalApp.Domain.Models.Abstractions.BaseUsers;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace FinalApp.DAL.Repository.Implemintations
{
    public class UserRepository<T> : IUserRepository<T>
        where T : ApplicationUser
    {

        protected readonly AppDbContext _context;
        protected readonly UserManager<T> _userManager;
        protected readonly DbSet<T> _dbSet;
        public UserRepository(AppDbContext context, UserManager<T> userManager)
        {
            _context = context;
            _userManager = userManager;
            _dbSet = _context.Set<T>();

            if (_dbSet == default(DbSet<T>))
                throw new ArgumentNullException(nameof(DbSet<T>));
        }
        public async Task Create(T entity)
        {
            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
        }
        public async Task<IQueryable<T>> ReadAllAsync()
        {
            return await Task.FromResult(_dbSet);
        }
        public async Task<T> ReadByIdAsync(string id)
        {
            var user = await _userManager.FindByIdAsync(id);

            return user == null
            ? throw new ArgumentNullException(nameof(id), $"Entity not found by id {id}")
            : user;
        }


    }
}
