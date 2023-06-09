﻿using FinalProj.ApiModels.Response.Interfaces;
using FinalProj.Domain.Models.Entities.Persons.Users;

namespace FinalProj.Services.Interfaces
{
    public interface IClientService
    {
        //public Task<IBaseResponse<bool>> CreateClient(Client client);
        public Task<IBaseResponse<IEnumerable<Client>>> GetClientsWithRequests();
        //public Task<IBaseResponse<IEnumerable<Request>>> GetActiveRequests(string clientId);
        //public Task<IBaseResponse<IEnumerable<Request>>> GetClosedRequests(string clientId);

    }
}
