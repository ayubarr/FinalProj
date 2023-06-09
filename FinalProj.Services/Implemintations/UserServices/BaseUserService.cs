﻿using FinalProj.ApiModels.DTOs.EntitiesDTOs.RequestsDTO;
using FinalProj.ApiModels.Response.Helpers;
using FinalProj.ApiModels.Response.Interfaces;
using FinalProj.DAL.Repository.Interfaces;
using FinalProj.Domain.Models.Abstractions.BaseUsers;
using FinalProj.Domain.Models.Entities.Requests.RequestsInfo;
using FinalProj.Domain.Models.Enums;
using FinalProj.Services.Helpers;
using FinalProj.Services.Interfaces;
using FinalProj.Services.Mapping.Helpers;
using FinallApp.ValidationHelper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace FinalProj.Services.Implemintations.UserServices
{
    public class BaseUserService<T> : IBaseUserService<T>
        where T : ApplicationUser
    {

        private const int RolesMinIndex = 0;
        private const int RolesMaxIndex = 4;
        private readonly IBaseAsyncRepository<Request> _repository;
        protected readonly UserManager<T> _userManager;


        public BaseUserService(IBaseAsyncRepository<Request> repository, UserManager<T> userManager)
        {
            _repository = repository;
            _userManager = userManager;

        }
        public async Task<IBaseResponse<bool>> SetUserAsRoleById(string userId, int roleId)
        {
            try
            {
                StringValidator.CheckIsNotNull(userId);
                NumberValidator<int>.IsRange(roleId, RolesMinIndex, RolesMaxIndex);

                var user = await TypeHelper<T>.CheckUserTypeForRole(_userManager, userId);

                Roles role = (Roles)(object)roleId;

                var roleName = role.ToString();

                var result = await _userManager.AddToRoleAsync(user, roleName);

                if (result.Succeeded)
                {
                    return ResponseFactory<bool>.CreateSuccessResponse(true);
                }
                else
                {
                    return ResponseFactory<bool>.CreateErrorResponse(new Exception("Failed to set user as role."));
                }

            }
            catch (ArgumentNullException argNullException)
            {
                return ResponseFactory<bool>.CreateNotFoundResponse(argNullException);
            }
            catch (Exception ex)
            {
                return ResponseFactory<bool>.CreateErrorResponse(ex);
            }
        }

        public async Task<IBaseResponse<string>> CheckUserRole(string userId, int roleId)
        {
            try
            {
                StringValidator.CheckIsNotNull(userId);
                NumberValidator<int>.IsRange(roleId, RolesMinIndex, RolesMaxIndex);

                var user = await _userManager.FindByIdAsync(userId);
                ObjectValidator<T>.CheckIsNotNullObject(user);

                Roles role = (Roles)(object)roleId;
                var roleName = role.ToString();

                bool isInRole = await _userManager.IsInRoleAsync(user, roleName);

                if (isInRole)
                {
                    return ResponseFactory<string>.CreateSuccessResponse($"User role is: {roleName}");
                }
                else
                {
                    throw new ArgumentNullException("its role not found");
                }
            }
            catch (ArgumentNullException argNullException)
            {
                return ResponseFactory<string>.CreateNotFoundResponse(argNullException);
            }
            catch (Exception ex)
            {
                return ResponseFactory<string>.CreateErrorResponse(ex);
            }
        }



        public async Task<IBaseResponse<bool>> SetUserAsAdminAsync(string userId)
        {
            try
            {
                StringValidator.CheckIsNotNull(userId);
                var user = await _userManager.FindByIdAsync(userId);
                ObjectValidator<T>.CheckIsNotNullObject(user);

                var role = Roles.Administrator.ToString();
                var result = await _userManager.AddToRoleAsync(user, role);

                if (result.Succeeded)
                {
                    return ResponseFactory<bool>.CreateSuccessResponse(true);
                }
                else
                {
                    return ResponseFactory<bool>.CreateErrorResponse(new Exception("Failed to set user as admin."));
                }
            }
            catch (ArgumentNullException argNullException)
            {
                return ResponseFactory<bool>.CreateNotFoundResponse(argNullException);
            }
            catch (Exception ex)
            {
                return ResponseFactory<bool>.CreateErrorResponse(ex);
            }
        }

        public async Task<IBaseResponse<IEnumerable<RequestDTO>>> GetActiveRequests(string Id)
        {
            try
            {
                StringValidator.CheckIsNotNull(Id);

                var requests = await TypeHelper<T>.CheckUserTypeForActiveRequest(Id, _repository);
                IEnumerable<RequestDTO> requestsDTO = MapperHelperForDto<Request, RequestDTO>.Map(requests);

                return ResponseFactory<IEnumerable<RequestDTO>>.CreateSuccessResponse(requestsDTO);
            }
            catch (ArgumentNullException argNullException)
            {
                return ResponseFactory<IEnumerable<RequestDTO>>.CreateNotFoundResponse(argNullException);
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
                IEnumerable<RequestDTO> requestsDTO = MapperHelperForDto<Request, RequestDTO>.Map(requests);

                return ResponseFactory<IEnumerable<RequestDTO>>.CreateSuccessResponse(requestsDTO);
            }
            catch (ArgumentNullException argNullException)
            {
                return ResponseFactory<IEnumerable<RequestDTO>>.CreateNotFoundResponse(argNullException);
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

                await _repository.UpdateAsync(request);

                return ResponseFactory<bool>.CreateSuccessResponse(true);
            }
            catch (ArgumentNullException argNullException)
            {
                return ResponseFactory<bool>.CreateNotFoundResponse(argNullException);
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
                var request = await TypeHelper<T>.CheckUserTypeForMarkAsCompleteRequest(requestId, _repository);

                await _repository.UpdateAsync(request);

                return ResponseFactory<bool>.CreateSuccessResponse(true);
            }
            catch (ArgumentNullException argNullException)
            {
                return ResponseFactory<bool>.CreateNotFoundResponse(argNullException);
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

                request.RequestStatus = Status.Closed;
                await _repository.UpdateAsync(request);

                return ResponseFactory<bool>.CreateSuccessResponse(true);
            }
            catch (ArgumentNullException argNullException)
            {
                return ResponseFactory<bool>.CreateNotFoundResponse(argNullException);
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
                ObjectValidator<IEnumerable<T>>.CheckIsNotNullObject(users);

                return ResponseFactory<IEnumerable<T>>.CreateSuccessResponse(users);
            }
            catch (ArgumentNullException ex)
            {
                return ResponseFactory<IEnumerable<T>>.CreateNotFoundResponse(ex);
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
                ObjectValidator<T>.CheckIsNotNullObject(user);

                return ResponseFactory<T>.CreateSuccessResponse(user);
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
                ObjectValidator<T>.CheckIsNotNullObject(user);

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
    }
}

