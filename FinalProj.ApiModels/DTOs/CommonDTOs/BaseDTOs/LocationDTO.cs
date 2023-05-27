namespace FinalApp.ApiModels.DTOs.CommonDTOs.BaseDTOs
{
    public class LocationDTO : BaseEntityDTO
    {
        public string City { get; set; }
        public string Street { get; set; }
        public string HouseNumber { get; set; }
        public string? ApartmentNumber { get; set; }
        public string ZipCode { get; set; }
    }
}
