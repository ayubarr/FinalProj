using FinalApp.DAL.Repository.Interfaces;
using FinalApp.DAL.SqlServer;
using FinalApp.Domain.Models.Abstractions.BaseUsers;
using Microsoft.EntityFrameworkCore;

namespace FinalApp.DAL.Repository.Implemintations
{
    //public class UserRepository<T> : IUserRepository<T>
    //    where T : User
    //{

    //    protected readonly AppDbContext _context;
    //    protected readonly DbSet<T> _dbSet;
    //    public UserRepository(AppDbContext context)
    //    {
    //        _context = context;
    //        _dbSet = _context.Set<T>();

    //        if (_dbSet == default(DbSet<T>))
    //            throw new ArgumentNullException(nameof(DbSet<T>));
    //    }
    //    public async Task Create(T entity)
    //    {
    //        await _dbSet.AddAsync(entity);
    //        await _context.SaveChangesAsync();
    //    }
    //    public async Task<IQueryable<T>> ReadAllAsync()
    //    {
    //        return await Task.FromResult(_dbSet);
    //    }
    //    public async Task<T> ReadByIdAsync(int id)
    //    {

    //        var entity = await ReadAllAsync().Result.FirstOrDefaultAsync(x => x.userId == id);

    //        return entity == null
    //        ? throw new ArgumentNullException(nameof(id), $"Entity not found by id {id}")
    //        : entity;
    //    }


    //}
}
