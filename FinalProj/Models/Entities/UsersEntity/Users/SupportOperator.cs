using FinalProj.Domain.Models.Abstractions.BaseUsers;
using FinalProj.Domain.Models.Entities.Requests.RequestsInfo;
using FinalProj.Domain.Models.Enums;

namespace FinalProj.Domain.Models.Entities.Persons.Users
{
    public class SupportOperator : User
    {
        public override Roles UserType { get; set; } = Roles.TechnicalSupportOperator;

        public ICollection<Request>?  Requests { get; set; }
    }
}
