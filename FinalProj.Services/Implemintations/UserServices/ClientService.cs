using FinalApp.ApiModels.Response.Helpers;
using FinalApp.ApiModels.Response.Interfaces;
using FinalApp.DAL.Repository.Interfaces;
using FinalApp.Domain.Models.Entities.Persons.Users;
using FinalApp.Domain.Models.Entities.Requests.RequestsInfo;
using FinalApp.Domain.Models.Enums;
using FinalApp.Services.Helpers;
using FinalApp.Services.Interfaces;
using FinallApp.ValidationHelper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace FinalProj.Services.Implemintations.UserServices
{
    public class ClientService : IClientService
    {
        protected readonly UserManager<Client> _userManager;

        public ClientService( UserManager<Client> userManager) 
        {
            _userManager = userManager;
        }
 
        public async Task<IBaseResponse<IEnumerable<Client>>> GetClientsWithRequests()
        {
            try
            {
                var clientsWithRequests = await _userManager.Users.Include(c => c.Requests).ToListAsync();

                ObjectValidator<IEnumerable<Client>>.CheckIsNotNullObject(clientsWithRequests);

                return ResponseFactory<IEnumerable<Client>>.CreateSuccessResponse(clientsWithRequests);
            }
            catch (ArgumentNullException argNullException)
            {
                return ResponseFactory<IEnumerable<Client>>.CreateNotFoundResponse(argNullException);
            }
            catch (Exception exception)
            {
                return ResponseFactory<IEnumerable<Client>>.CreateErrorResponse(exception);
            }

        }

    }
}
