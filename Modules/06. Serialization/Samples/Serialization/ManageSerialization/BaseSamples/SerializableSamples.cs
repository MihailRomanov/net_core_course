using FluentAssertions;
using ManageSerialization.Helpers;
using ProtoBuf;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace ManageSerialization.BaseSamples
{
    public class SerializableSamples
    {
        public record AForXml
        {
            public string? PropertyA { get; set; }
            [XmlIgnore]
            public string? PropertyB { get; set; }
        }

        [Test]
        public void XmlSerializer()
        {
            var data = new AForXml { PropertyA = "A", PropertyB = "B" };

            var helper = new XmlSerializerHelper<AForXml>(new XmlSerializer(typeof(AForXml)));
            var result = helper.SerializeAndDeserialize(data);

            result.Should().Be(data with { PropertyB = null });
        }


        public record AForJson
        {
            private string? fieldA;
            [JsonInclude]
            private string? fieldB;
            [JsonIgnore]
            public string? PropertyA { get => fieldA; set => fieldA = value; }
            [JsonIgnore]
            public string? PropertyB { get => fieldB; set => fieldB = value; }
        }

        [Test]
        public void JsonSerializer()
        {
            var data = new AForJson { PropertyA = "A", PropertyB = "B" };

            var helper = new JsonSerializerHelper<AForJson>();
            var result = helper.SerializeAndDeserialize(data);

            result.Should().Be(data with { PropertyA = null });
        }

        [ProtoContract(ImplicitFields = ImplicitFields.AllPublic)]
        public record AForProto1
        {
            public string? PropertyA { get; set; }
            public string? PropertyB { get; set; }
        }

        [Test]
        public void ProtobufSerializer1()
        {
            var data = new AForProto1 { PropertyA = "AAAAA", PropertyB = "BBBBB" };

            var helper = new ProtobufSerializerHelper<AForProto1>();
            var result = helper.SerializeAndDeserialize(data);

            result.Should().Be(data);
        }


        [ProtoContract]
        public record AForProto2
        {
            [ProtoMember(1)]
            public string? PropertyA { get; set; }
            [ProtoMember(2)]
            public string? PropertyB { get; set; }
        }

        [Test]
        public void ProtobufSerializer2()
        {
            var data = new AForProto2 { PropertyA = "AAAAA", PropertyB = "BBBBB" };

            var helper = new ProtobufSerializerHelper<AForProto2>();
            var result = helper.SerializeAndDeserialize(data);

            result.Should().Be(data);
        }
    }
}
