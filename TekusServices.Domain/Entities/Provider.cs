using System.ComponentModel.DataAnnotations;

namespace TekusServices.Domain.Entities
{
    public class Provider
    {
        public int ProviderId { get; set; }

        [Required]
        [MaxLength(20)]
        public string Nit { get; set; } = string.Empty;

        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [MaxLength(50)]
        public string Email { get; set; } = string.Empty;

        [Required]
        public bool Active { get; set; }

        // Relaciones
        public ICollection<Service> Services { get; set; } = [];
        public ICollection<ProviderCustomField>? CustomFields { get; set; }
    }
}
