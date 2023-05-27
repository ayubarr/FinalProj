using FinalApp.DAL.Repository.Interfaces;
using FinalApp.Domain.Models.Abstractions.BaseEntities;
using FinalApp.Domain.Models.Abstractions.BaseUsers;
using FinalApp.Domain.Models.Entities.Persons.Users;
using FinalApp.Domain.Models.Entities.Requests.RequestsInfo;
using FinalApp.Domain.Models.Enums;
using Microsoft.EntityFrameworkCore;

namespace FinalApp.Services.Helpers
{
    public static class TypeHelper<T>
        where T : ApplicationUser
    {
        public static async Task<IEnumerable<Request>> CheckUserTypeForActiveRequest(int Id, IBaseAsyncRepository<Request> _repository)
        {
            if (typeof(T) == typeof(TechTeam))
            {
                return await _repository
                    .ReadAllAsync().Result
                    .Where(request => request.TechTeamId == Id && request.RequestStatus == Status.Active)
                    .ToListAsync();
            }
            if (typeof(T) == typeof(SupportOperator))
            {
                return await _repository
                   .ReadAllAsync().Result
                   .Where(request => request.OperatorId == Id && request.RequestStatus == Status.Active)
                .ToListAsync();
            }
            else throw new ArgumentException();
        }

        public static async Task<IEnumerable<Request>> CheckUserTypeForClosedRequest(int Id, IBaseAsyncRepository<Request> repository)
        {
            if (typeof(T) == typeof(TechTeam))
            {
                return await repository
                    .ReadAllAsync().Result
                    .Where(request => request.TechTeamId == Id && request.RequestStatus == Status.Closed)
                    .ToListAsync();
            }
            if (typeof(T) == typeof(SupportOperator))
            {
                return await repository
                   .ReadAllAsync().Result
                   .Where(request => request.OperatorId == Id && request.RequestStatus == Status.Closed)
                .ToListAsync();
            }
            else throw new ArgumentException();
        }

        public static async Task<Request> CheckUserTypeForAcceptRequest(int requestId, int Id, IBaseAsyncRepository<Request> repository)
        {
            var request = await repository.ReadByIdAsync(requestId);
            if (typeof(T) == typeof(TechTeam))
            {
                request.RequestStatus = Status.InProgress;
                request.TechTeamId = Id;

                return request;
            }
            if (typeof(T) == typeof(SupportOperator))
            {
                request.RequestStatus = Status.InProgress;
                request.OperatorId = Id;

                return request;
            }
            else throw new ArgumentException();
        }
    }
}
