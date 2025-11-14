namespace TekusServices.Application.DTO
{
    public class ServiceDTO
    {
        public int ServiceId { get; set; }
        public required string Name { get; set; }
        public decimal HourlyRate { get; set; }
        public bool Active { get; set; }

        // Nombre del proveedor
        public int ProviderId { get; set; }
        public string? ProviderName { get; set; }

        // Países donde se ofrece el servicio
        public List<CountryDTO>? Countries { get; set; }
    }
}
