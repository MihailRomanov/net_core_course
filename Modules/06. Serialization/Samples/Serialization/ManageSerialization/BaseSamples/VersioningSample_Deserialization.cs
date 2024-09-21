using System.Xml.Serialization;
using System.Runtime.Serialization;
using ManageSerialization.Helpers;
using FluentAssertions;
using ProtoBuf;

namespace ManageSerialization.BaseSamples
{
    public class VersioningSample_Deserialization
    {
        public enum EnumOne
        {
            One,
            Two
        }

        [ProtoContract(ImplicitFields = ImplicitFields.AllPublic)]
        [DataContract(Name = "A")]
        public record A
        {
            [DataMember]
            public int IntP { get; set; }
            [DataMember]
            public string? StringP { get; set; }
            [DataMember]
            public EnumOne EnumP { get; set; }
        }

        [XmlRoot(ElementName = "A")]
        [ProtoContract(ImplicitFields = ImplicitFields.AllPublic)]
        [DataContract(Name = "A")]
        public record ADef
        {
            public ADef()
            {
                EnumP = EnumOne.Two;
                IntP = 100;
                StringP = "sss";
            }
            [DataMember]
            public int IntP { get; set; }
            [DataMember]
            public string? StringP { get; set; }
            [DataMember]
            public EnumOne EnumP { get; set; }
        }

        string XmlStr = @"<A></A>";

        [Test]
        public void XmlSerializer_Default()
        {
            var helper = new XmlSerializerHelper<A>(new XmlSerializer(typeof(A)));
            var result = helper.Deserialization(XmlStr)!;
            result.Should().Be(new A
            {
                EnumP = default,
                IntP = default,
                StringP = default,
            });
        }

        [Test]
        public void XmlSerializer_HandleDefault()
        {
            var helper = new XmlSerializerHelper<ADef>(new XmlSerializer(typeof(ADef)));
            var result = helper.Deserialization(XmlStr)!;
            result.Should().Be(new ADef
            {
                EnumP = EnumOne.Two,
                IntP = 100,
                StringP = "sss",
            });
        }

        string dataContractStr = """
                <A 
                    xmlns="http://schemas.datacontract.org/2004/07/ManageSerialization.BaseSamples" 
                    xmlns:i="http://www.w3.org/2001/XMLSchema-instance">
                </A>
                """;

        [Test]
        public void DataContractSerializer_Default()
        {
            var helper = new DataContractSerializerHelper<A>(new DataContractSerializer(typeof(A)));
            var result = helper.Deserialization(dataContractStr)!;
            result.Should().Be(new A
            {
                EnumP = default,
                IntP = default,
                StringP = default,
            });
        }

        string jsonStr = "{}";

        [Test]
        public void JsonSerializer_Default()
        {
            var helper = new JsonSerializerHelper<A>();
            var result = helper.Deserialization(jsonStr)!;
            result.Should().Be(new A
            {
                EnumP = default,
                IntP = default,
                StringP = default,
            });
        }

        [Test]
        public void JsonSerializer_HandleDefault()
        {
            var helper = new JsonSerializerHelper<ADef>();
            var result = helper.Deserialization(jsonStr)!;
            result.Should().Be(new ADef
            {
                EnumP = EnumOne.Two,
                IntP = 100,
                StringP = "sss",
            });
        }

        [Test]
        public void NewtonsoftJsonSerializer_Default()
        {
            var helper = new NewtonsoftJsonSerializerHelper<A>(new Newtonsoft.Json.JsonSerializer());
            var result = helper.Deserialization(jsonStr)!;
            result.Should().Be(new A
            {
                EnumP = default,
                IntP = default,
                StringP = default,
            });
        }

        [Test]
        public void NewtonsoftJsonSerializer_HandleDefault()
        {
            var helper = new NewtonsoftJsonSerializerHelper<ADef>(new Newtonsoft.Json.JsonSerializer());
            var result = helper.Deserialization(jsonStr)!;
            result.Should().Be(new ADef
            {
                EnumP = EnumOne.Two,
                IntP = 100,
                StringP = "sss",
            });
        }


        [Test]
        public void Protobuf_Default()
        {
            var helper = new ProtobufSerializerHelper<A>();
            var result = helper.Deserialization([])!;
            result.Should().Be(new A
            {
                EnumP = default,
                IntP = default,
                StringP = default,
            });
        }

        [Test]
        public void Protobuf_HandleDefault()
        {
            var helper = new ProtobufSerializerHelper<ADef>();
            var result = helper.Deserialization([])!;
            result.Should().Be(new ADef
            {
                EnumP = EnumOne.Two,
                IntP = 100,
                StringP = "sss",
            });
        }
    }
}
