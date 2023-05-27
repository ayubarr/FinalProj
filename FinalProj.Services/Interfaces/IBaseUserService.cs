using FinalApp.ApiModels.DTOs.EntitiesDTOs.RequestsDTO;
using FinalApp.ApiModels.Response.Interfaces;
using FinalApp.Domain.Models.Abstractions.BaseUsers;

namespace FinalApp.Services.Interfaces
{
    public interface IUserService<T>
        where T : ApplicationUser
    {
        public Task<IBaseResponse<IEnumerable<RequestDTO>>> GetActiveRequests(int Id);
        public Task<IBaseResponse<IEnumerable<RequestDTO>>> GetClosedRequests(int Id);
        public Task<IBaseResponse<bool>> AcceptRequest(int requestId, int Id);
        public Task<IBaseResponse<bool>> MarkRequestAsCompleted(int requestId);
        public Task<IBaseResponse<bool>> CloseRequestByUser(int requestId, int Id);
    }
}
