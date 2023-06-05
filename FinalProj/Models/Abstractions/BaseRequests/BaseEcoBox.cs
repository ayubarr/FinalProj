using FinalProj.Domain.Models.Abstractions.BaseEntities;

namespace FinalProj.Domain.Models.Abstractions.BaseRequests
{
    public abstract class BaseEcoBox : BaseEntity
    {
        public int ProductPrice { get; set; }
    }
}
