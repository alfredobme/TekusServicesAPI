namespace TekusServices.Application.DTO
{
    public class ProviderCustomFieldUpdateDTO
    {
        public required int ProviderCustomFieldId { get; set; }
        public required int ProviderId { get; set; }
        public required string FieldName { get; set; }
        public required string FieldValue { get; set; }
    }
}
