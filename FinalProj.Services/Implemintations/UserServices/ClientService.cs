﻿using FinalApp.ApiModels.Response.Helpers;
using FinalApp.ApiModels.Response.Interfaces;
using FinalApp.Domain.Models.Entities.Persons.Users;
using FinalApp.Domain.Models.Entities.Requests.RequestsInfo;
using FinalApp.Domain.Models.Enums;
using FinalApp.Services.Helpers;
using FinalApp.Services.Implemintations;
using FinalApp.Services.Interfaces;
using FinallApp.ValidationHelper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace FinalProj.Services.Implemintations.UserServices
{
    public class ClientService : BaseUserService<Client> IClientService
    {
        protected readonly UserManager<Client> _userManager;


        public ClientService(UserManager<Client> userManager) : base()
        {

            _userManager = userManager;
        }
        public async Task<IBaseResponse<bool>> CreateClient(Client client)
        {
            try
            {

                //TODO: REFACT THIS
                ObjectValidator<Client>.CheckIsNotNullObject(client);

                client.Password = HashHelper.HashPassword(client.Password);

                await _userManager.CreateAsync(client);


                return ResponseFactory<bool>.CreateSuccessResponse(true);
            }
            catch (ArgumentNullException argNullException)
            {
                return ResponseFactory<bool>
                    .CreateNotFoundResponse(argNullException);
            }
            catch (Exception exception)
            {
                return ResponseFactory<bool>.CreateErrorResponse(exception);
            }
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
