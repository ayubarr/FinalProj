using FinalApp.Domain.Models.Abstractions.BaseEntities;

namespace FinalApp.Domain.Models.Abstractions.BaseUsers
{
    public abstract class BasePerson : BaseEntity
    {
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string? FullName
        {
            get
            {
                return $"{Name} {Surname}";
            }
        }


    }
}
