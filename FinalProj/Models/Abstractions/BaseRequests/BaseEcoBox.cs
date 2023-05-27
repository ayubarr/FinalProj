using FinalApp.Domain.Models.Abstractions.BaseEntities;

namespace FinalApp.Domain.Models.Abstractions.BaseRequests
{
    public abstract class BaseEcoBox : BaseEntity
    {
        public int ProductPrice { get; set; }
    }
}
