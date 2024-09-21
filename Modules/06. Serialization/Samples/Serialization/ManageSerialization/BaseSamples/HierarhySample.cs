using FluentAssertions;
using ManageSerialization.Helpers;
using ProtoBuf;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace ManageSerialization.BaseSamples
{
    public class HierarhySamples
    {
        [KnownType(typeof(B)), KnownType(typeof(C))]
        [XmlInclude(typeof(B)), XmlInclude(typeof(C))]
        [JsonDerivedType(typeof(B), "typeB"), JsonDerivedType(typeof(C), "typeC")]
        [ProtoInclude(200, typeof(B)), ProtoInclude(300, typeof(C))]
        [ProtoContract]
        public record A
        {
            [ProtoMember(1)]
            public string PropertyA { get; set; }
        }

        [ProtoContract]
        public record B : A
        {
            [ProtoMember(1)]
            public string PropertyB { get; set; }
        }

        [ProtoContract]
        public record C : A
        {
            [ProtoMember(1)]
            public string PropertyC { get; set; }
        }

        public A[] aArray =
                [
                    new A { PropertyA = "A" },
                    new B { PropertyA = "AA", PropertyB = "BB"},
                    new C { PropertyA = "AAA", PropertyC = "CCC"}
                ];

        [Test]
        public void XmlSerializer()
        {
            var helper = new XmlSerializerHelper<A[]>(
                new XmlSerializer(typeof(A[])));
            var result = helper.SerializeAndDeserialize(aArray);

            result.Should().Equal(aArray);
        }

        [Test]
        public void DataContractSerializer()
        {
            var helper = new DataContractSerializerHelper<A[]>(
                new DataContractSerializer(typeof(A[])));
            var result = helper.SerializeAndDeserialize(aArray);
            result.Should().Equal(aArray);
        }

        [Test]
        public void JsonSerializer()
        {
            var helper = new JsonSerializerHelper<A[]>();
            var result = helper.SerializeAndDeserialize(aArray);
            result.Should().Equal(aArray);
        }

        [Test]
        public void NewtonsoftJsonSerializer()
        {
            var serializer = new Newtonsoft.Json.JsonSerializer()
            {
                TypeNameHandling = Newtonsoft.Json.TypeNameHandling.Objects
            };
            var helper = new NewtonsoftJsonSerializerHelper<A[]>(serializer);
            var result = helper.SerializeAndDeserialize(aArray);
            result.Should().Equal(aArray);
        }

        [Test]
        public void ProtobufSerializer()
        {
            var helper = new ProtobufSerializerHelper<A[]>();
            var result = helper.SerializeAndDeserialize(aArray);
            result.Should().Equal(aArray);
        }

    }
}
