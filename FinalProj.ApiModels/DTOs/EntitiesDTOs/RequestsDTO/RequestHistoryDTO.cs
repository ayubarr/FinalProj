using FinalProj.ApiModels.DTOs.CommonDTOs.BaseDTOs;
using FinalProj.Domain.Models.Enums;

namespace FinalProj.ApiModels.DTOs.EntitiesDTOs.RequestsDTO
{
    public class RequestStatusHistoryDTO : BaseEntityDTO
    {

        /// <summary>
        /// The ID of the request associated with the status history.
        /// </summary>
        public Guid RequestId { get; set; }

        /// <summary>
        /// The ID of the user who made the status change.
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// The timestamp when the status change occurred.
        /// </summary>
        public DateTime Timestamp { get; set; }

        /// <summary>
        /// The previous status of the request.
        /// </summary>
        public Status PreviousStatus { get; set; }

        /// <summary>
        /// The new status of the request.
        /// </summary>
        public Status NewStatus { get; set; }
    }
}
