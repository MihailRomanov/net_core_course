using System.Text.Json;

namespace ManageSerialization.Helpers
{
    public class JsonSerializerHelper<T> : BaseSerializationHelper<T>
    {
        private readonly JsonSerializerOptions? options;

        public JsonSerializerHelper(JsonSerializerOptions? options = null) : base(ShowResultFormat.String)
        {
            this.options = options;
        }

        public override T? Deserialization(Stream stream)
        {
            return JsonSerializer.Deserialize<T>(stream, options);
        }

        public override void Serialization(T data, Stream stream)
        {
            JsonSerializer.Serialize(stream, data, options);
        }
    }
}
