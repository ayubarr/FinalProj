using FinalProj.Domain.Models.Entities.Persons.Users;

namespace FinalProj.Domain.Models.Entities.Persons.WorkTeams
{
    public class TechnicalTeamWorker
    {
        public string? TechnicalTeamId { get; set; }
        public TechTeam TechnicalTeam { get; set; }

        public Guid? WorkerId { get; set; }
        public Worker Worker { get; set; }
    }
}
