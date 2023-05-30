using FinalApp.Domain.Models.Abstractions.BaseRequests;
using FinalApp.Domain.Models.Entities.Persons.Users;
using FinalApp.Domain.Models.Enums;

namespace FinalApp.Domain.Models.Entities.Requests.RequestsInfo
{
    public class Request : RequestInfo
    {
        public string? Comment { get; set; }
        public int BoxQuantity { get; set; }
        public DateTime CompletedTime { get; set; }
        public WorkTypes WorkType { get; set; }


        public Client? Client { get; set; }
        public string? ClientId { get; set; }

        public RecyclingPlant? RecyclingPlant { get; set; }
        public Guid? PlantId { get; set; }

        public Location? Location { get; set; }
        public Guid? LocationId { get; set; }

        public SupportOperator? SupportOperator { get; set; }
        public string? OperatorId { get; set; }

        public Review? Review { get; set; }
        public Guid? ReviewId { get; set; }

        public TechTeam? TechnicalTeam { get; set; }
        public string? TechTeamId { get; set; }

        public List<RequestStatusHistory>? StatusHistory { get; set; }
    }
}
