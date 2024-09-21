using System.Runtime.Serialization;

namespace ManageSerialization.Helpers
{
    public class DataContractSerializerHelper<T> : BaseSerializationHelper<T, DataContractSerializer>
    {
        public DataContractSerializerHelper(
            DataContractSerializer serializer) : base(serializer, ShowResultFormat.String)
        { }

        public override T? Deserialization(Stream stream)
        {
            return (T?)serializer.ReadObject(stream);
        }

        public override void Serialization(T data, Stream stream)
        {
            serializer.WriteObject(stream, data);
        }
    }
}
