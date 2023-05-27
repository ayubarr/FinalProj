using FinalApp.ApiModels.DTOs.CommonDTOs.BaseDTOs;
using FinalApp.ApiModels.Response.Interfaces;
using FinalApp.Domain.Models.Abstractions.BaseEntities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FinalApp.Services.Interfaces
{
    /// <summary>
    /// Represents a base service for managing entities of type T and their corresponding DTOs of type Tmodel.
    /// </summary>
    /// <typeparam name="T">The entity type.</typeparam>
    /// <typeparam name="Tmodel">The DTO type.</typeparam>
    public interface IBaseRequestService<T, Tmodel>
        where T : BaseEntity
        where Tmodel : BaseEntityDTO
    {
        /// <summary>
        /// Creates a new entity asynchronously.
        /// </summary>
        /// <param name="entityDTO">The entity to create.</param>
        /// <returns>An asynchronous operation that returns the created entity.</returns>
        Task<IBaseResponse<T>> CreateAsync(Tmodel entityDTO);

        /// <summary>
        /// Retrieves all entities.
        /// </summary>
        /// <returns>A response containing a collection of entities.</returns>
        IBaseResponse<IEnumerable<T>> ReadAll();

        /// <summary>
        /// Retrieves all entities asynchronously.
        /// </summary>
        /// <returns>An asynchronous operation that returns a response containing a collection of entities.</returns>
        Task<IBaseResponse<IEnumerable<T>>> ReadAllAsync();

        /// <summary>
        /// Retrieves an entity by its ID.
        /// </summary>
        /// <param name="id">The ID of the entity to retrieve.</param>
        /// <returns>A response containing the retrieved entity.</returns>
        IBaseResponse<T> ReadById(int id);

        /// <summary>
        /// Retrieves an entity by its ID asynchronously.
        /// </summary>
        /// <param name="id">The ID of the entity to retrieve.</param>
        /// <returns>An asynchronous operation that returns a response containing the retrieved entity.</returns>
        Task<IBaseResponse<T>> ReadByIdAsync(int id);

        /// <summary>
        /// Updates an existing entity asynchronously.
        /// </summary>
        /// <param name="entityDTO">The updated entity.</param>
        /// <returns>An asynchronous operation that returns the updated entity.</returns>
        Task<IBaseResponse<T>> UpdateAsync(Tmodel entityDTO);

        /// <summary>
        /// Deletes an entity asynchronously.
        /// </summary>
        /// <param name="entityDTO">The entity to delete.</param>
        /// <returns>An asynchronous operation that returns a response indicating the success of the deletion.</returns>
        Task<IBaseResponse<bool>> DeleteAsync(Tmodel entityDTO);

        /// <summary>
        /// Deletes an entity by its ID asynchronously.
        /// </summary>
        /// <param name="id">The ID of the entity to delete.</param>
        /// <returns>An asynchronous operation that returns a response indicating the success of the deletion.</returns>
        Task<IBaseResponse<bool>> DeleteByIdAsync(int id);
    }
}
