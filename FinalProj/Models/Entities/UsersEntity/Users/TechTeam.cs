using FinalApp.Domain.Models.Abstractions.BaseUsers;
using FinalApp.Domain.Models.Entities.Persons.WorkTeams;
using FinalApp.Domain.Models.Entities.Requests.RequestsInfo;
using FinalApp.Domain.Models.Enums;

namespace FinalApp.Domain.Models.Entities.Persons.Users
{
    public class TechTeam : User
    {
        public override Roles UserType { get; set; } = Roles.TechnicalSpecialist;

        public Request? Request { get; set; }

        public ICollection<TechnicalTeamWorker> Workers { get; set; }
        public int? WorkerId { get; set; }

    }
}
