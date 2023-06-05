using FinalProj.Domain.Models.Abstractions.BaseUsers;
using FinalProj.Domain.Models.Entities.Persons.WorkTeams;
using FinalProj.Domain.Models.Entities.Requests.RequestsInfo;
using FinalProj.Domain.Models.Enums;

namespace FinalProj.Domain.Models.Entities.Persons.Users
{
    public class TechTeam : User
    {
        public override Roles UserType { get; set; } = Roles.TechnicalSpecialist;

        public Request? Request { get; set; }

        public ICollection<TechnicalTeamWorker> Workers { get; set; }
        public Guid? WorkerId { get; set; }

    }
}
