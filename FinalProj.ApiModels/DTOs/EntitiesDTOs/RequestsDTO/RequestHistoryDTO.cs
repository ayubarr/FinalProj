using FinalApp.ApiModels.DTOs.CommonDTOs.BaseDTOs;
using FinalApp.Domain.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalApp.ApiModels.DTOs.EntitiesDTOs.RequestsDTO
{
    public class RequestStatusHistoryDTO : BaseEntityDTO
    {

        /// <summary>
        /// The ID of the request associated with the status history.
        /// </summary>
        public int RequestId { get; set; }

        /// <summary>
        /// The ID of the user who made the status change.
        /// </summary>
        public int UserId { get; set; }

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
