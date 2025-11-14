using System.ComponentModel.DataAnnotations;

namespace TekusServices.Domain.Entities
{
    public class ProviderCustomField
    {
        public int ProviderCustomFieldId { get; set; }

        [Required]
        public int ProviderId { get; set; }

        public Provider? Provider { get; set; }

        [Required]
        [MaxLength(50)]
        public string FieldName { get; set; } = string.Empty;

        [Required]
        [MaxLength(150)]
        public string FieldValue { get; set; } = string.Empty;
    }
}
