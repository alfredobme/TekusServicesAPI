namespace TekusServices.Application.DTO
{
    public class ProviderCreateDTO
    {
        public required string Nit { get; set; }
        public required string Name { get; set; }
        public required string Email { get; set; }
        public required bool Active { get; set; }
    }
}
