using FinalApp.ApiModels.DTOs.EntitiesDTOs.RequestsDTO;
using FinalApp.ApiModels.Response.Interfaces;
using FinalApp.Domain.Models.Abstractions.BaseUsers;

namespace FinalApp.Services.Interfaces
{
    public interface IBaseUserService<T>
        where T : ApplicationUser
    {
        public Task<IBaseResponse<IEnumerable<RequestDTO>>> GetActiveRequests(string Id);
        public Task<IBaseResponse<IEnumerable<RequestDTO>>> GetClosedRequests(string Id);
        public Task<IBaseResponse<bool>> AcceptRequest(Guid requestId, string Id);
        public Task<IBaseResponse<bool>> MarkRequestAsCompleted(Guid requestId);
        public Task<IBaseResponse<bool>> CloseRequestByUser(Guid requestId, string Id);
        public Task<IBaseResponse<bool>> CreateAsync(T user, string password);
        public Task<IBaseResponse<IEnumerable<T>>> ReadAllAsync();
        public Task<IBaseResponse<T>> ReadByIdAsync(string userId);
        public Task<IBaseResponse<bool>> UpdateAsync(T user);
        public Task<IBaseResponse<bool>> DeleteAsync(T user);
    }
}
