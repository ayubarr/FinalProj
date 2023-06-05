using FinalProj.DAL.SqlServer;
using FinalProj.Domain.Models.Entities.Persons.Users;
using FinalProj.DAL.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FinalProj.DAL.Repository.Implemintations
{
    public class ClientRepository : IClientRepository
    {
        protected readonly AppDbContext _context;
        protected readonly DbSet<Client> _dbSet;
        public ClientRepository(AppDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<Client>();

            if (_dbSet == default(DbSet<Client>))
                throw new ArgumentNullException(nameof(DbSet<Client>));
        }
        public IQueryable<Client> GetAllClients()
        {
            return _dbSet;
        }
    }
}
