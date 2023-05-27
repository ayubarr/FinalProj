using FinalApp.Domain.Models.Abstractions.BaseUsers;
using FinalApp.Domain.Models.Enums;

namespace FinalApp.Domain.Models.Entities.Persons.WorkTeams
{
    public class Worker : BasePerson
    {
        public string Salary { get; set; }
        public DateTime HireTime { get; set; }
        public Roles Position { get; set; } = Roles.TechnicalWorker;

        public ICollection<TechnicalTeamWorker> TechnicalTeams { get; set; }
        public int? TechTeamId { get; set; }
    }
}
