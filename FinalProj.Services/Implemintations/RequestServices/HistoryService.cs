using FinalApp.ApiModels.DTOs.EntitiesDTOs.RequestsDTO;
using FinalApp.ApiModels.Response.Helpers;
using FinalApp.ApiModels.Response.Interfaces;
using FinalApp.DAL.Repository.Interfaces;
using FinalApp.Domain.Models.Entities.Requests.RequestsInfo;
using FinalApp.Services.Interfaces;
using FinallApp.ValidationHelper;

namespace FinalProj.Services.Implemintations.RequestServices
{
    public class RequestHistoryService : IRequestHistoryService
    {
        private readonly IBaseAsyncRepository<Request> _repository;
        public RequestHistoryService(IBaseAsyncRepository<Request> repository)
        {
            _repository = repository;
        }

        public async Task<IBaseResponse<IEnumerable<RequestStatusHistoryDTO>>> GetRequestHistoryStatus(Guid requestId)
        {
            try
            {
                ObjectValidator<Guid>.CheckIsNotNullObject(requestId);

                var request = await _repository.ReadByIdAsync(requestId);
                var history = request.StatusHistory.Select(s => new RequestStatusHistoryDTO
                {
                    RequestId = s.RequestId,
                    UserId = s.UserId,
                    Timestamp = s.Timestamp,
                    PreviousStatus = s.PreviousStatus,
                    NewStatus = s.NewStatus
                });

                return ResponseFactory<IEnumerable<RequestStatusHistoryDTO>>.CreateSuccessResponse(history);
            }
            catch (ArgumentNullException argNullException)
            {
                return ResponseFactory<IEnumerable<RequestStatusHistoryDTO>>.CreateNotFoundResponse(argNullException);
            }
            catch (Exception exception)
            {
                return ResponseFactory<IEnumerable<RequestStatusHistoryDTO>>.CreateErrorResponse(exception);
            }
        }
    }
}
