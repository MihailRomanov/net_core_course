using System.Xml.Serialization;

namespace ManageSerialization.Helpers
{
    public class XmlSerializerHelper<T> : BaseSerializationHelper<T, XmlSerializer>
    {
        public XmlSerializerHelper(XmlSerializer serializer)
            : base(serializer, ShowResultFormat.String)
        { }

        public override T? Deserialization(Stream stream)
        {
            return (T?)serializer.Deserialize(stream);
        }

        public override void Serialization(T data, Stream stream)
        {
            serializer.Serialize(stream, data);
        }
    }
}
