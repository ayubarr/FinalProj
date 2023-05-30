using FinalApp.ApiModels.Auth.Models;
using FinalApp.DAL.Repository.Interfaces;
using FinalApp.Domain.Models.Abstractions.BaseUsers;
using FinalApp.Domain.Models.Entities.Persons.Users;
using FinalApp.Domain.Models.Entities.Requests.RequestsInfo;
using FinalApp.Domain.Models.Enums;
using FinallApp.ValidationHelper;
using Microsoft.EntityFrameworkCore;

namespace FinalApp.Services.Helpers
{
    public static class TypeHelper<T>
        where T : ApplicationUser
    {
        public static async Task<IEnumerable<Request>> CheckUserTypeForActiveRequest(string Id, IBaseAsyncRepository<Request> repository)
        {
            StringValidator.CheckIsNotNull(Id);
            ObjectValidator<IBaseAsyncRepository<Request>>.CheckIsNotNullObject(repository);

            if (typeof(T) == typeof(TechTeam))
            {
                return await repository
                    .ReadAllAsync().Result
                    .Where(request => request.TechTeamId == Id && request.RequestStatus == Status.Active)
                    .ToListAsync();
            }
            if (typeof(T) == typeof(SupportOperator))
            {
                return await repository
                   .ReadAllAsync().Result
                   .Where(request => request.OperatorId == Id && request.RequestStatus == Status.Active)
                .ToListAsync();
            }
            if (typeof(T) == typeof(Client))
            {
                return await repository
                 .ReadAllAsync().Result
                 .Where(request => request.ClientId == Id && request.RequestStatus == Status.Active)
              .ToListAsync();
            }

            throw new ArgumentNullException();
        }

        public static async Task<IEnumerable<Request>> CheckUserTypeForClosedRequest(string Id, IBaseAsyncRepository<Request> repository)
        {
            StringValidator.CheckIsNotNull(Id);
            ObjectValidator<IBaseAsyncRepository<Request>>.CheckIsNotNullObject(repository);

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
            if (typeof(T) == typeof(Client))
            {
                return await repository
                  .ReadAllAsync().Result
                  .Where(request => request.ClientId == Id && request.RequestStatus == Status.Closed)
               .ToListAsync();
            }

            throw new ArgumentNullException();
        }

        public static async Task<Request> CheckUserTypeForAcceptRequest(Guid requestId, string Id, IBaseAsyncRepository<Request> repository)
        {
            StringValidator.CheckIsNotNull(Id);
            ObjectValidator<IBaseAsyncRepository<Request>>.CheckIsNotNullObject(repository);
            ObjectValidator<Guid>.CheckIsNotNullObject(requestId);

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
            if (typeof(T) == typeof(Client))
            {
                request.RequestStatus = Status.InProgress;
                request.ClientId = Id;

                return request;
            }

            throw new ArgumentNullException();
        }
        public static async Task<Request> CheckUserTypeForMarkAsCompleteRequest(Guid requestId, IBaseAsyncRepository<Request> repository)
        {
            ObjectValidator<IBaseAsyncRepository<Request>>.CheckIsNotNullObject(repository);
            ObjectValidator<Guid>.CheckIsNotNullObject(requestId);

            var request = await repository.ReadByIdAsync(requestId);
            if (typeof(T) == typeof(TechTeam))
            {
                request.StatusTeamInfo = true;

                return request;
            }
            if (typeof(T) == typeof(SupportOperator))
            {
                if (request.StatusClientInfo && request.StatusTeamInfo)
                    request.RequestStatus = Status.Completed;

                return request;
            }
            if (typeof(T) == typeof(Client))
            {
                request.StatusClientInfo = true;

                return request;
            }

            throw new ArgumentNullException();
        }

        public static async Task<ApplicationUser> CheckUserTypeForRegistration(RegisterModel model)
        {
            ObjectValidator<RegisterModel>.CheckIsNotNullObject(model);

            if (typeof(T) == typeof(TechTeam))
            {
                var user = new TechTeam
                {
                    Email = model.Email,
                    SecurityStamp = Guid.NewGuid().ToString(),
                    UserName = model.UserName
                };
                return user;
            }
            if (typeof(T) == typeof(SupportOperator))
            {
                var user = new SupportOperator
                {
                    Email = model.Email,
                    SecurityStamp = Guid.NewGuid().ToString(),
                    UserName = model.UserName
                };
                return user;

            }
            if (typeof(T) == typeof(Client))
            {
                var user = new Client
                {
                    Email = model.Email,
                    SecurityStamp = Guid.NewGuid().ToString(),
                    UserName = model.UserName
                };
                return user;
            }

            throw new ArgumentNullException();
        }
    }
}
