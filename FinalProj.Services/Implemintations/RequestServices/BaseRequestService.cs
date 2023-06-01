﻿using FinalApp.ApiModels.DTOs.CommonDTOs.BaseDTOs;
using FinalApp.ApiModels.Response.Helpers;
using FinalApp.ApiModels.Response.Interfaces;
using FinalApp.DAL.Repository.Interfaces;
using FinalApp.Domain.Models.Abstractions.BaseEntities;
using FinalApp.Domain.Models.Entities.Persons.Users;
using FinalApp.Domain.Models.Entities.Requests.RequestsInfo;
using FinalApp.Services.Helpers;
using FinalApp.Services.Interfaces;
using FinalApp.Services.Mapping.Helpers;
using FinallApp.ValidationHelper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace FinalProj.Services.Implemintations.RequestServices
{
    public class BaseRequestService<T, Tmodel> : IBaseRequestService<T, Tmodel>
        where T : BaseEntity
        where Tmodel : BaseEntityDTO
    {
        private readonly IBaseAsyncRepository<T> _repository;
        private readonly UserManager<Client> _clientManager;

        public BaseRequestService(IBaseAsyncRepository<T> repository, UserManager<Client> clientManager)
        {
            _repository = repository;
            _clientManager = clientManager;
        }

        public async Task<IBaseResponse<T>> CreateAsync(Tmodel entityDTO)
        {

            try
            {
                ObjectValidator<Tmodel>.CheckIsNotNullObject(entityDTO);

                T entity = MapperHelperForEntity<Tmodel, T>.Map(entityDTO);
                entity.Id = Guid.NewGuid();

                await _repository.Create(entity);

                return ResponseFactory<T>.CreateSuccessResponseWithId(entity, entity.Id);
            }
            catch (ArgumentNullException argNullException)
            {
                return ResponseFactory<T>.CreateNotFoundResponse(argNullException);
            }
            catch (Exception exception)
            {
                return ResponseFactory<T>.CreateErrorResponse(exception);
            }
        }

        public IBaseResponse<IEnumerable<T>> ReadAll()
        {
            try
            {
                var entities = _repository.ReadAll().ToList();

                return ResponseFactory<IEnumerable<T>>.CreateSuccessResponse(entities);
            }
            catch (ArgumentNullException argNullException)
            {
                return ResponseFactory<IEnumerable<T>>.CreateNotFoundResponse(argNullException);
            }
            catch (Exception exception)
            {
                return ResponseFactory<IEnumerable<T>>.CreateErrorResponse(exception);
            }
        }

        public async Task<IBaseResponse<IEnumerable<T>>> ReadAllAsync()
        {
            try
            {
                var entities = await _repository.ReadAllAsync().Result.ToListAsync();

                return ResponseFactory<IEnumerable<T>>.CreateSuccessResponse(entities);
            }
            catch (ArgumentNullException argNullException)
            {
                return ResponseFactory<IEnumerable<T>>.CreateNotFoundResponse(argNullException);
            }
            catch (Exception exception)
            {
                return ResponseFactory<IEnumerable<T>>.CreateErrorResponse(exception);
            }
        }

        public IBaseResponse<T> ReadById(Guid id)
        {
            try
            {
                ObjectValidator<Guid>.CheckIsNotNullObject(id);

                var entity = _repository.ReadById(id);

                return ResponseFactory<T>.CreateSuccessResponse(entity);
            }
            catch (ArgumentNullException argNullException)
            {
                return ResponseFactory<T>.CreateNotFoundResponse(argNullException);
            }
            catch (Exception exception)
            {
                return ResponseFactory<T>.CreateErrorResponse(exception);
            }
        }

        public async Task<IBaseResponse<T>> ReadByIdAsync(Guid id)
        {
            try
            {
                ObjectValidator<Guid>.CheckIsNotNullObject(id);

                var entity = await _repository.ReadByIdAsync(id);

                return ResponseFactory<T>.CreateSuccessResponse(entity);
            }
            catch (ArgumentNullException argNullException)
            {
                return ResponseFactory<T>.CreateNotFoundResponse(argNullException);
            }
            catch (Exception exception)
            {
                return ResponseFactory<T>.CreateErrorResponse(exception);
            }
        }

        public async Task<IBaseResponse<T>> UpdateAsync(Tmodel entityDTO)
        {
            try
            {
                ObjectValidator<Tmodel>.CheckIsNotNullObject(entityDTO);

                T entity = MapperHelperForEntity<Tmodel, T>.Map(entityDTO);
                await _repository.UpdateAsync(entity);

                return ResponseFactory<T>.CreateSuccessResponse(entity);
            }
            catch (ArgumentNullException argNullException)
            {
                return ResponseFactory<T>.CreateNotFoundResponse(argNullException);
            }
            catch (Exception exception)
            {
                return ResponseFactory<T>.CreateErrorResponse(exception);
            }
        }

        public async Task<IBaseResponse<bool>> DeleteAsync(Tmodel entityDTO)
        {
            try
            {
                ObjectValidator<Tmodel>.CheckIsNotNullObject(entityDTO);

                T entity = MapperHelperForEntity<Tmodel, T>.Map(entityDTO);
                await _repository.DeleteAsync(entity);

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

        public async Task<IBaseResponse<bool>> DeleteByIdAsync(Guid id)
        {
            try
            {
                ObjectValidator<Guid>.CheckIsNotNullObject(id);

                await _repository.DeleteByIdAsync(id);

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
    }
}
