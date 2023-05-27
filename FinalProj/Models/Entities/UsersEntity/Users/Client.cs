using FinalApp.Domain.Models.Abstractions.BaseUsers;
using FinalApp.Domain.Models.Entities.Requests.RequestsInfo;
using FinalApp.Domain.Models.Enums;

namespace FinalApp.Domain.Models.Entities.Persons.Users
{
    public class Client : User
    {
        public override Roles UserType { get; set; } = Roles.Client;

        public ICollection<Request>? Requests { get; set; }
    }
}
