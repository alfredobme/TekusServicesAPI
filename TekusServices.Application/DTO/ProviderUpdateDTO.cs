namespace TekusServices.Application.DTO
{
    public class ProviderUpdateDTO
    {
        public int ProviderId { get; set; }
        public required string Nit { get; set; }
        public required string Name { get; set; }
        public required string Email { get; set; }
        public required bool Active { get; set; }
    }
}

