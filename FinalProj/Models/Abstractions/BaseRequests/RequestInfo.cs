using FinalApp.Domain.Models.Enums;

namespace FinalApp.Domain.Models.Abstractions.BaseRequests
{
    public abstract class RequestInfo : BaseRequest
    {
        public virtual Status RequestStatus { get; set; }
        public virtual Types RequestType { get; set; }

        public bool StatusClientInfo { get; set; }
        public bool StatusTeamInfo { get; set; }
    }
}
