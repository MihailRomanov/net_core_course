using Newtonsoft.Json;

namespace ManageSerialization.Helpers
{
    public class NewtonsoftJsonSerializerHelper<T> : BaseSerializationHelper<T, JsonSerializer>
    {
        public NewtonsoftJsonSerializerHelper(JsonSerializer jsonSerializer) 
            : base(jsonSerializer, ShowResultFormat.String)
        { }

        public override T? Deserialization(Stream stream)
        {
            var reader = new JsonTextReader(new StreamReader(stream));
            return serializer.Deserialize<T>(reader);
        }

        public override void Serialization(T data, Stream stream)
        {
            using var writer = new StreamWriter(stream);
            serializer.Serialize(writer, data);
        }
    }
}
