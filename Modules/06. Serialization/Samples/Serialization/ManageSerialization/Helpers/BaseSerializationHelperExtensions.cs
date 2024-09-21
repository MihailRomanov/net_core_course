using System.Text;

namespace ManageSerialization.Helpers
{
    public static class BaseSerializationHelperExtensions
    {
        public static TData? Deserialization<TData>(
            this BaseSerializationHelper<TData> helper, string data)
        {
            var buffer = Encoding.UTF8.GetBytes(data);
            return helper.Deserialization(new MemoryStream(buffer));
        }

        public static TData? Deserialization<TData>(
            this BaseSerializationHelper<TData> helper, byte[] data)
        {
            return helper.Deserialization(new MemoryStream(data));
        }

        public static void SerializationAndShow<TData>(
            this BaseSerializationHelper<TData> helper, TData data)
        {
            var stream = new MemoryStream();
            helper.Serialization(data, stream);

            var serializedContent = stream.ToArray();
            helper.ShowSerializedContent(serializedContent);
        }
    }
}
