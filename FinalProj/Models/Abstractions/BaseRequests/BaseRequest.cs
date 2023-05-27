using FinalApp.Domain.Models.Abstractions.BaseEntities;

namespace FinalApp.Domain.Models.Abstractions.BaseRequests
{
    public abstract class BaseRequest : BaseEntity
    {
        public DateTime RequestCreatedTime { get; set; } = DateTime.UtcNow;
    }
}
