using FinalApp.ApiModels.DTOs.EntitiesDTOs.RequestsDTO;
using FinalApp.ApiModels.Response.Helpers;
using FinalApp.ApiModels.Response.Interfaces;
using FinalApp.DAL.Repository.Interfaces;
using FinalApp.Domain.Models.Entities.Requests.RequestsInfo;
using FinalApp.Services.Interfaces;
using FinallApp.ValidationHelper;

namespace FinalApp.Services.Implemintations
{
    public class RequestHistoryService : IRequestHistoryService
    {
        private readonly IBaseAsyncRepository<Request> _repository;
        public RequestHistoryService(IBaseAsyncRepository<Request> repository)
        {
            _repository = repository;
        }


        public async Task<IBaseResponse<IEnumerable<RequestStatusHistoryDTO>>> GetRequestHistoryStatus(int requestId)
        {
            try
            {
                NumberValidator<int>.IsPositive(requestId);

                var request = await _repository.ReadByIdAsync(requestId);
                ObjectValidator<Request>.CheckIsNotNullObject(request);

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
            catch (ArgumentException argException)
            {
                return ResponseFactory<IEnumerable<RequestStatusHistoryDTO>>.CreateNotFoundResponse(argException);
            }
            catch (Exception exception)
            {
                return ResponseFactory<IEnumerable<RequestStatusHistoryDTO>>.CreateErrorResponse(exception);
            }
        }
    }
}
