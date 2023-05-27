namespace FinalApp.Domain.Models.Abstractions.BaseRequests
{
    public abstract class CompanyContactInfo : BaseLocation
    {
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
    }
}
