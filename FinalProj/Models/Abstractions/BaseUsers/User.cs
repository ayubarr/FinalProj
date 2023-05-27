using FinalApp.Domain.Models.Enums;

namespace FinalApp.Domain.Models.Abstractions.BaseUsers
{
    public class User : PersonalContactInfo
    {
        public virtual Roles UserType { get; set; }
        public DateTime CreatedTime { get; set; } = DateTime.UtcNow;

    }
}
