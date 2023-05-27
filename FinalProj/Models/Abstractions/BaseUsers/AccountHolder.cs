namespace FinalApp.Domain.Models.Abstractions.BaseUsers
{
    public class AccountHolder : ApplicationUser
    {
        public string Login { get; set; }
        public string Password { get; set; }
 
    }
}
