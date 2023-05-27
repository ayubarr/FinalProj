using FinalApp.ApiModels.DTOs.EntitiesDTOs.RequestsDTO;
using FinalApp.ApiModels.DTOs.EntitiesDTOs.UsersDTOs;
using FinalApp.ApiModels.Response.Helpers;
using FinalApp.ApiModels.Response.Interfaces;
using FinalApp.DAL.Repository.Implemintations;
using FinalApp.DAL.Repository.Interfaces;
using FinalApp.Domain.Models.Entities.Persons.Users;
using FinalApp.Domain.Models.Entities.Requests.RequestsInfo;
using FinalApp.Domain.Models.Enums;
using FinalApp.Services.Helpers;
using FinalApp.Services.Interfaces;
using FinalApp.Services.Mapping.Helpers;
using FinallApp.ValidationHelper;
using Microsoft.EntityFrameworkCore;

namespace FinalApp.Services.Implemintations
{
    public class ClientService : IClientService
    {
        private readonly IUserRepository<Client> _repository;

        public ClientService(IUserRepository<Client> repository)
        {
            _repository = repository;
        }

        public async Task<IBaseResponse<IEnumerable<Client>>> GetClientsWithRequests()
        {
            try
            {
                var clientsWithRequests = await _repository.ReadAllAsync().Result.Include(c => c.Requests).ToListAsync();

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

        //public async Task<IBaseResponse<bool>> RegisterClient(ClientDTO client)
        //{
        //    try
        //    {
        //        ObjectValidator<ClientDTO>.CheckIsNotNullObject(client);
        //            //TODO: Check and refact this method 
        //        var newClient = MapperHelperForEntity<ClientDTO, Client>.Map(client);
        //        newClient.Password = HashHelper.HashPassword(client.Password);

        //        await _repository.Create(newClient);

        //        return ResponseFactory<bool>.CreateSuccessResponse(true);
        //    }
        //    catch (ArgumentNullException argNullException)
        //    {
        //        return ResponseFactory<bool>
        //            .CreateNotFoundResponse(argNullException);
        //    }
        //    catch (Exception exception)
        //    {
        //        return ResponseFactory<bool>.CreateErrorResponse(exception);
        //    }
        //}
        public async Task<IBaseResponse<IEnumerable<Request>>> GetActiveRequests(int clientId)
        {
            try
            {
                NumberValidator<int>.IsPositive(clientId);

                var client = await _repository.ReadByIdAsync(clientId);
                ObjectValidator<Client>.CheckIsNotNullObject(client);

                var activeRequests = client.Requests.Where(r => r.RequestStatus == Status.Active);

                return ResponseFactory<IEnumerable<Request>>.CreateSuccessResponse(activeRequests);
            }
            catch (ArgumentException argException)
            {
                return ResponseFactory<IEnumerable<Request>>.CreateNotFoundResponse(argException);
            }
            catch (Exception exception)
            {
                return ResponseFactory<IEnumerable<Request>>.CreateErrorResponse(exception);
            }
        }

        public async Task<IBaseResponse<IEnumerable<Request>>> GetClosedRequests(int clientId)
        {
            try
            {
                NumberValidator<int>.IsPositive(clientId);

                var client = await _repository.ReadByIdAsync(clientId);
                ObjectValidator<Client>.CheckIsNotNullObject(client);

                var closedRequests = client.Requests.Where(r => r.RequestStatus == Status.Closed);

                return ResponseFactory<IEnumerable<Request>>.CreateSuccessResponse(closedRequests);
            }
            catch (ArgumentException argException)
            {
                return ResponseFactory<IEnumerable<Request>>.CreateNotFoundResponse(argException);
            }
            catch (Exception exception)
            {
                return ResponseFactory<IEnumerable<Request>>.CreateErrorResponse(exception);
            }
        }

    }
}
