using FinalApp.DAL.Repository.Interfaces;
using FinalApp.DAL.SqlServer;
using FinalApp.Domain.Models.Abstractions.BaseEntities;
using FinallApp.ValidationHelper;
using Microsoft.EntityFrameworkCore;

namespace FinalApp.DAL.Repository.Implemintations
{
    public class BaseAsyncRepository<T> : IBaseAsyncRepository<T>
        where T : BaseEntity
    {
        protected readonly AppDbContext _context;
        protected readonly DbSet<T> _dbSet;
        public BaseAsyncRepository(AppDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();

            if (_dbSet == default(DbSet<T>))
                throw new ArgumentNullException(nameof(DbSet<T>));
        }

        public async Task Create(T entity)
        {
            ObjectValidator<T>.CheckIsNotNullObject(entity);

            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
        }
        public IQueryable<T> ReadAll()
        {
            return _dbSet;
        }
        public async Task<IQueryable<T>> ReadAllAsync()
        {
            return await Task.FromResult(_dbSet);
        }
        public T ReadById(Guid id)
        {
            ObjectValidator<Guid>.CheckIsNotNullObject(id);

            var entity = ReadAll().FirstOrDefault(x => x.Id == id);

            return entity == null
             ? throw new ArgumentNullException(nameof(id), $"Entity not found by id {id}")
             : entity;
        }
        public async Task<T> ReadByIdAsync(Guid id)
        {
            ObjectValidator<Guid>.CheckIsNotNullObject(id);
            var entity = await ReadAllAsync().Result.FirstOrDefaultAsync(x => x.Id == id);

            return entity == null
            ? throw new ArgumentNullException(nameof(id), $"Entity not found by id {id}")
            : entity;
        }
        public async Task UpdateAsync(T entity)
        {
            ObjectValidator<T>.CheckIsNotNullObject(entity);

            _dbSet.Update(entity);
            await _context.SaveChangesAsync();
        }


        public async Task DeleteAsync(T entity)
        {
            ObjectValidator<T>.CheckIsNotNullObject(entity);

            _dbSet.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteByIdAsync(Guid id)
        {
            ObjectValidator<Guid>.CheckIsNotNullObject(id);

            var entity = await ReadByIdAsync(id);
            await DeleteAsync(entity);
        }
    }
}
