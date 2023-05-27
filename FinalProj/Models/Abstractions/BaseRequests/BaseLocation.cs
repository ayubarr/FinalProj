using FinalApp.Domain.Models.Abstractions.BaseEntities;

namespace FinalApp.Domain.Models.Abstractions.BaseRequests
{
    public abstract class BaseLocation : BaseEntity
    {
        public string City { get; set; }
        public string Street { get; set; }
        public string HouseNumber { get; set; }
        public string? ApartmentNumber { get; set; }
        public string ZipCode { get; set; }
        public string Address
        {
            get
            {
                return 
                    $"City: {City}\n" +
                    $"Street: {Street}\n" +
                    $"HouseNumber: {HouseNumber}\n" +
                    $"ApartmentNumber: {ApartmentNumber}\n" +
                    $"ZipCode {ZipCode}"
                    ;
            }
        }
    }
}

