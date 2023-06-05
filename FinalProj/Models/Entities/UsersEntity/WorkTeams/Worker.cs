using FinalProj.Domain.Models.Abstractions.BaseUsers;
using FinalProj.Domain.Models.Enums;

namespace FinalProj.Domain.Models.Entities.Persons.WorkTeams
{
    public class Worker : BasePerson
    {
        public string Salary { get; set; }
        public DateTime HireTime { get; set; }
        public Roles Position { get; set; } 

        public ICollection<TechnicalTeamWorker> TechnicalTeams { get; set; }
        public string? TechTeamId { get; set; }
    }
}
