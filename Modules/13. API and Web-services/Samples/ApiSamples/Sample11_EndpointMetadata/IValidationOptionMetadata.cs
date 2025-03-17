namespace Sample11_EndpointMetadata
{
    public enum ValidationOption
    { 
        NotNull,
        NotNullAndEmpty
    }
    public interface IValidationOptionMetadata
    {
        ValidationOption Option { get; }
    }

    public class ValidationOptionMetadata : IValidationOptionMetadata
    {
        public ValidationOptionMetadata(ValidationOption option)
        {
            Option = option;
        }
        public ValidationOption Option { get; }
    }

    [AttributeUsage(AttributeTargets.All)]
    public class ValidationOptionMetadataAttribute : 
        Attribute, IValidationOptionMetadata
    {
        public ValidationOptionMetadataAttribute(ValidationOption option)
        {
            Option = option;
        }
        public ValidationOption Option { get; }
    }
}
