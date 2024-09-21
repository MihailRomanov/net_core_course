using ProtoBuf;

namespace ManageSerialization.Helpers
{
    public class ProtobufSerializerHelper<T> : BaseSerializationHelper<T>
    {
        public ProtobufSerializerHelper() : base(ShowResultFormat.HexArray)
        { }

        public override T? Deserialization(Stream stream)
        {
            return Serializer.Deserialize<T>(stream);
        }

        public override void Serialization(T data, Stream stream)
        {
            Serializer.Serialize(stream, data);
        }
    }
}
