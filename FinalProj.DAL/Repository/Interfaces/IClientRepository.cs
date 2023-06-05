using FinalProj.Domain.Models.Entities.Persons.Users;

namespace FinalProj.DAL.Repository.Interfaces
{
    public interface IClientRepository
    {
        public IQueryable<Client> GetAllClients();
    }
}
