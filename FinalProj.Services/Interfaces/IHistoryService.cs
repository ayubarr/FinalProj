using FinalApp.ApiModels.DTOs.EntitiesDTOs.RequestsDTO;
using FinalApp.ApiModels.Response.Interfaces;

namespace FinalApp.Services.Interfaces
{
    public interface IRequestHistoryService
    {
        public Task<IBaseResponse<IEnumerable<RequestStatusHistoryDTO>>> GetRequestHistoryStatus(int requestId);
    }
}
