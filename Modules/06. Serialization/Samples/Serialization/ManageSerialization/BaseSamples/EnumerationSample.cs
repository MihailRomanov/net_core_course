using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using System.Xml.Serialization;
using ManageSerialization.Helpers;
using NSoft = Newtonsoft.Json;

namespace ManageSerialization.BaseSamples
{
    public class EnumerationSample
    {
        public enum EnumOne
        {
            One,
            Two
        }

        [DataContract]
        public enum EnumTwo
        {
            [XmlEnum("1")]
            [EnumMember(Value = "D1")]
            One,
            [XmlEnum("2")]
            [EnumMember(Value = "D2")]
            Two
        }

        public record A
        {
            public EnumOne EnumOne { get; set; }
            public EnumTwo EnumTwo { get; set; }
        }


        [Test]
        public void XmlSerializer()
        {
            var helper = new XmlSerializerHelper<A>(new XmlSerializer(typeof(A)));
            helper.SerializeAndDeserialize(
                new A
                {
                    EnumOne = EnumOne.One,
                    EnumTwo = EnumTwo.Two
                });
        }

        [Test]
        public void DataContractSerializer()
        {
            var helper = new DataContractSerializerHelper<A>(new DataContractSerializer(typeof(A)));
            helper.SerializeAndDeserialize(
                new A
                {
                    EnumOne = EnumOne.One,
                    EnumTwo = EnumTwo.Two
                });
        }

        public record B
        {
            public EnumOne EnumOne { get; set; }
            [JsonConverter(typeof(JsonStringEnumConverter))]
            [NSoft.JsonConverter(typeof(NSoft.Converters.StringEnumConverter))]
            public EnumOne EnumOne2 { get; set; }
        }


        [Test]
        public void JsonSerializer()
        {
            var helper = new JsonSerializerHelper<B>();
            helper.SerializeAndDeserialize(
                new B
                {
                    EnumOne = EnumOne.One,
                    EnumOne2 = EnumOne.Two
                });
        }

        [Test]
        public void NewtonsoftJsonSerializer()
        {
            var helper = new NewtonsoftJsonSerializerHelper<B>(new NSoft.JsonSerializer());
            helper.SerializeAndDeserialize(
                new B
                {
                    EnumOne = EnumOne.One,
                    EnumOne2 = EnumOne.Two
                });
        }

    }
}
