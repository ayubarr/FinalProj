using FinalApp.Domain.Models.Enums;

namespace FinalApp.Domain.Models.Abstractions.BaseRequests
{
    public abstract class RequestInfo : BaseRequest
    {
        public  Status RequestStatus { get; set; }
        public  Types RequestType { get; set; } = Types.RequestExecution;
        public bool StatusClientInfo { get; set; }
        public bool StatusTeamInfo { get; set; }
    }
}
