using FinalApp.Domain.Models.Entities.Persons.Users;

namespace FinalApp.Domain.Models.Entities.Persons.WorkTeams
{
    public class TechnicalTeamWorker
    {
        public int? TechnicalTeamId { get; set; }
        public TechTeam TechnicalTeam { get; set; }

        public int? WorkerId { get; set; }
        public Worker Worker { get; set; }
    }
}
