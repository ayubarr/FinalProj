using FinalProj.Domain.Models.Enums;

namespace FinalProj.Domain.Models.Abstractions.BaseRequests
{
    public abstract class RequestInfo : BaseRequest
    {
        public  Status RequestStatus { get; set; }
        public  Types RequestType { get; set; } = Types.RequestExecution;
        public bool StatusClientInfo { get; set; }
        public bool StatusTeamInfo { get; set; }
    }
}
