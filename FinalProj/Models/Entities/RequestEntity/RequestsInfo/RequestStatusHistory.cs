using FinalApp.Domain.Models.Abstractions.BaseEntities;
using FinalApp.Domain.Models.Enums;

namespace FinalApp.Domain.Models.Entities.Requests.RequestsInfo
{
    public class RequestStatusHistory : BaseEntity
    {
        public int RequestId { get; set; }
        public int UserId { get; set; }
        public DateTime Timestamp { get; set; }
        public Status PreviousStatus { get; set; }
        public Status NewStatus { get; set; }

        public Request Request { get; set; }
    }
}
