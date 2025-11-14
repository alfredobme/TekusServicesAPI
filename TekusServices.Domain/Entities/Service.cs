using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;

namespace TekusServices.Domain.Entities
{
    public class Service
    {
        public int ServiceId { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [Column(TypeName = "decimal(8,2)")]
        public decimal HourlyRate { get; set; }
       
        [Required]
        public bool Active { get; set; }

        // Relación con Proveedor
        public int ProviderId { get; set; }
        public Provider? Provider { get; set; }

        // Lista de países donde se ofrece el servicio (Texto JSON)
        public string CountriesJson
        {
            get => JsonSerializer.Serialize(Countries);
            set => Countries = string.IsNullOrWhiteSpace(value)
                ? []
                : JsonSerializer.Deserialize<List<Country>>(value) ?? [];
        }

        // Propiedad no mapeada (solo para usar en código)
        [System.ComponentModel.DataAnnotations.Schema.NotMapped]
        public List<Country> Countries { get; set; } = [];
    }
}
