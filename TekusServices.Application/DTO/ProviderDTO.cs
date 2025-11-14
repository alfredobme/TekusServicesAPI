namespace TekusServices.Application.DTO
{
    public class ProviderDTO
    {
        public int ProviderId { get; set; }
        public required string Nit { get; set; }
        public required string Name { get; set; }
        public required string Email { get; set; }
        public required bool Active { get; set; }

        // Lista de servicios
        public List<ServiceDTO>? Services { get; set; }
    }
}
