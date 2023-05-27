using FinalApp.ApiModels.DTOs.EntitiesDTOs.RequestsDTO;
using FinalApp.ApiModels.Response.Helpers;
using FinalApp.ApiModels.Response.Interfaces;
using FinalApp.DAL.Repository.Interfaces;
using FinalApp.Domain.Models.Abstractions.BaseUsers;
using FinalApp.Domain.Models.Entities.Requests.RequestsInfo;
using FinalApp.Domain.Models.Enums;
using FinalApp.Services.Helpers;
using FinalApp.Services.Interfaces;
using FinalApp.Services.Mapping.Helpers;
using FinallApp.ValidationHelper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace FinalProj.Services.Implemintations.UserServices
{
    public class BaseUserService<T> : IBaseUserService<T>
        where T : ApplicationUser
    {
        private readonly IBaseAsyncRepository<Request> _repository;
        protected readonly UserManager<T> _userManager;


        public BaseUserService(IBaseAsyncRepository<Request> repository, UserManager<T> userManager)
        {
            _repository = repository;
            _userManager = userManager;

        }

        public async Task<IBaseResponse<IEnumerable<RequestDTO>>> GetActiveRequests(string Id)
        {
            try
            {
                StringValidator.CheckIsNotNull(Id);

                var requests = TypeHelper<T>.CheckUserTypeForActiveRequest(Id, _repository).Result;

                ObjectValidator<IEnumerable<Request>>.CheckIsNotNullObject(requests);
                IEnumerable<RequestDTO> requestsDTO = MapperHelperForDto<Request, RequestDTO>.Map(requests);

                return ResponseFactory<IEnumerable<RequestDTO>>.CreateSuccessResponse(requestsDTO);
            }
            catch (ArgumentException argException)
            {
                return ResponseFactory<IEnumerable<RequestDTO>>.CreateNotFoundResponse(argException);
            }
            catch (Exception exception)
            {
                return ResponseFactory<IEnumerable<RequestDTO>>.CreateErrorResponse(exception);
            }
        }

        public async Task<IBaseResponse<IEnumerable<RequestDTO>>> GetClosedRequests(string Id)
        {
            try
            {
                StringValidator.CheckIsNotNull(Id);

                var requests = TypeHelper<T>.CheckUserTypeForClosedRequest(Id, _repository).Result;

                ObjectValidator<IEnumerable<Request>>.CheckIsNotNullObject(requests);
                IEnumerable<RequestDTO> requestsDTO = MapperHelperForDto<Request, RequestDTO>.Map(requests);

                return ResponseFactory<IEnumerable<RequestDTO>>.CreateSuccessResponse(requestsDTO);
            }
            catch (ArgumentException argException)
            {
                return ResponseFactory<IEnumerable<RequestDTO>>.CreateNotFoundResponse(argException);
            }
            catch (Exception exception)
            {
                return ResponseFactory<IEnumerable<RequestDTO>>.CreateErrorResponse(exception);
            }
        }
        public async Task<IBaseResponse<bool>> AcceptRequest(Guid requestId, string Id)
        {
            try
            {
                ObjectValidator<Guid>.CheckIsNotNullObject(requestId);
                StringValidator.CheckIsNotNull(Id);

                var request = await TypeHelper<T>.CheckUserTypeForAcceptRequest(requestId, Id, _repository);
                ObjectValidator<Request>.CheckIsNotNullObject(request);

                await _repository.UpdateAsync(request);

                return ResponseFactory<bool>.CreateSuccessResponse(true);
            }
            catch (ArgumentException argException)
            {
                return ResponseFactory<bool>.CreateNotFoundResponse(argException);
            }
            catch (Exception exception)
            {
                return ResponseFactory<bool>.CreateErrorResponse(exception);
            }
        }
        public async Task<IBaseResponse<bool>> MarkRequestAsCompleted(Guid requestId)
        {
            try
            {
                ObjectValidator<Guid>.CheckIsNotNullObject(requestId);
                var request = await _repository.ReadByIdAsync(requestId);
                ObjectValidator<Request>.CheckIsNotNullObject(request);

                request.RequestStatus = Status.Completed;
                await _repository.UpdateAsync(request);

                return ResponseFactory<bool>.CreateSuccessResponse(true);
            }
            catch (ArgumentException argException)
            {
                return ResponseFactory<bool>.CreateNotFoundResponse(argException);
            }
            catch (Exception exception)
            {
                return ResponseFactory<bool>.CreateErrorResponse(exception);
            }
        }

        public async Task<IBaseResponse<bool>> CloseRequestByUser(Guid requestId, string Id)
        {
            try
            {
                ObjectValidator<Guid>.CheckIsNotNullObject(requestId);
                StringValidator.CheckIsNotNull(Id);

                var request = await _repository.ReadByIdAsync(requestId);
                ObjectValidator<Request>.CheckIsNotNullObject(request);

                request.RequestStatus = Status.Closed;
                await _repository.UpdateAsync(request);

                return ResponseFactory<bool>.CreateSuccessResponse(true);
            }
            catch (ArgumentException argException)
            {
                return ResponseFactory<bool>.CreateNotFoundResponse(argException);
            }
            catch (Exception exception)
            {
                return ResponseFactory<bool>.CreateErrorResponse(exception);
            }
        }

        public async Task<IBaseResponse<bool>> CreateAsync(T user, string password)
        {
            try
            {
                ObjectValidator<ApplicationUser>.CheckIsNotNullObject(user);

                var result = await _userManager.CreateAsync(user, password);

                if (result.Succeeded)
                {
                    return ResponseFactory<bool>.CreateSuccessResponse(true);
                }
                else
                {
                    return ResponseFactory<bool>.CreateErrorResponse(new Exception("Failed to create user."));
                }
            }
            catch (ArgumentNullException ex)
            {
                return ResponseFactory<bool>.CreateNotFoundResponse(ex);
            }
            catch (Exception ex)
            {
                return ResponseFactory<bool>.CreateErrorResponse(ex);
            }
        }

        public async Task<IBaseResponse<IEnumerable<T>>> ReadAllAsync()
        {
            try
            {
                var users = await _userManager.Users.ToListAsync();
                return ResponseFactory<IEnumerable<T>>.CreateSuccessResponse(users);
            }
            catch (Exception ex)
            {
                return ResponseFactory<IEnumerable<T>>.CreateErrorResponse(ex);
            }
        }

        public async Task<IBaseResponse<T>> ReadByIdAsync(string userId)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(userId);

                if (user != null)
                {
                    return ResponseFactory<T>.CreateSuccessResponse(user);
                }
                else
                {
                    throw new ArgumentNullException();
                }
            }
            catch (ArgumentNullException ex)
            {
                return ResponseFactory<T>.CreateNotFoundResponse(ex);
            }
            catch (Exception ex)
            {
                return ResponseFactory<T>.CreateErrorResponse(ex);
            }
        }

        public async Task<IBaseResponse<bool>> UpdateAsync(T user)
        {
            try
            {
                ObjectValidator<ApplicationUser>.CheckIsNotNullObject(user);

                var result = await _userManager.UpdateAsync(user);

                if (result.Succeeded)
                {
                    return ResponseFactory<bool>.CreateSuccessResponse(true);
                }
                else
                {
                    return ResponseFactory<bool>.CreateErrorResponse(new Exception("Failed to update user."));
                }
            }
            catch (ArgumentNullException ex)
            {
                return ResponseFactory<bool>.CreateNotFoundResponse(ex);
            }
            catch (Exception ex)
            {
                return ResponseFactory<bool>.CreateErrorResponse(ex);
            }
        }

        public async Task<IBaseResponse<bool>> DeleteAsync(T user)
        {
            try
            {
                ObjectValidator<ApplicationUser>.CheckIsNotNullObject(user);

                var result = await _userManager.DeleteAsync(user);

                if (result.Succeeded)
                {
                    return ResponseFactory<bool>.CreateSuccessResponse(true);
                }
                else
                {
                    return ResponseFactory<bool>.CreateErrorResponse(new Exception("Failed to delete user."));
                }
            }
            catch (ArgumentNullException ex)
            {
                return ResponseFactory<bool>.CreateNotFoundResponse(ex);
            }
            catch (Exception ex)
            {
                return ResponseFactory<bool>.CreateErrorResponse(ex);
            }
        }

        public async Task<IBaseResponse<bool>> DeleteByIdAsync(string userId)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(userId);

                if (user != null)
                {
                    var result = await _userManager.DeleteAsync(user);

                    if (result.Succeeded)
                    {
                        return ResponseFactory<bool>.CreateSuccessResponse(true);
                    }
                    else
                    {
                        return ResponseFactory<bool>.CreateErrorResponse(new Exception("Failed to delete user."));
                    }
                }
                else
                {
                    throw new ArgumentNullException();
                }
            }
            catch (ArgumentNullException ex)
            {
                return ResponseFactory<bool>.CreateNotFoundResponse(ex);
            }
            catch (Exception ex)
            {
                return ResponseFactory<bool>.CreateErrorResponse(ex);
            }
        }
    }
}

