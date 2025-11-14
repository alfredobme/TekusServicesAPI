namespace TekusServices.Application.DTO
{
    public class ServiceCreateDTO
    {
        public required string Name { get; set; }
        public decimal HourlyRate { get; set; }
        public bool Active { get; set; }

        // Relación con Proveedores
        public int ProviderId { get; set; }

        // Países donde se ofrece el servicio
        public List<CountryDTO>? Countries { get; set; }
    }
}
