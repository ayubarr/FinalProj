using FinalProj.Domain.Models.Abstractions.BaseEntities;

namespace FinalProj.Domain.Models.Abstractions.BaseRequests
{
    public abstract class BaseRequest : BaseEntity
    {
        public DateTime RequestCreatedTime { get; set; } = DateTime.UtcNow;
    }
}
