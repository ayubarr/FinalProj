using FinalProj.Domain.Models.Enums;

namespace FinalProj.Domain.Models.Abstractions.BaseUsers
{
    public class User : PersonalContactInfo
    {
        public virtual Roles UserType { get; set; }
        public DateTime CreatedTime { get; set; } = DateTime.UtcNow;

    }
}
