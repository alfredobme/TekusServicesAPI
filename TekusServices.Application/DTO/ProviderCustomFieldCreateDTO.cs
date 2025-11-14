namespace TekusServices.Application.DTO
{
    public class ProviderCustomFieldCreateDTO
    {
        public required int ProviderId { get; set; }
        public required string FieldName { get; set; }
        public required string FieldValue { get; set; }
    }
}
