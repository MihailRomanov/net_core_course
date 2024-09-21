using System.Xml.Serialization;
using System.Runtime.Serialization;
using ManageSerialization.Helpers;
using FluentAssertions;
using System.ComponentModel;
using System.Runtime.Serialization.DataContracts;
using System.Text.Json.Serialization;
using Nsoft = Newtonsoft.Json;
using ProtoBuf;

namespace ManageSerialization.BaseSamples
{
    public class VersioningSample_Serialization
    {
        public enum EnumOne
        {
            One,
            Two
        }

        [DataContract]
        [ProtoContract]
        public record A
        {
            [DataMember]
            [ProtoMember(1)]
            public int IntP { get; set; }
            [DataMember]
            [ProtoMember(2)]
            public string? StringP { get; set; }
            [DataMember]
            [ProtoMember(3)]
            public EnumOne EnumP { get; set; }
        }

        [Test]
        public void XmlSerializer_Default()
        {
            var helper = new XmlSerializerHelper<A>(new XmlSerializer(typeof(A)));
            var result = helper.SerializeAndDeserialize(new A())!;
            result.Should().Be(new A
            {
                EnumP = default,
                IntP = default,
                StringP = default,
            });
        }

        [Test]
        public void DataContractSerializer_Default()
        {
            var helper = new DataContractSerializerHelper<A>(new DataContractSerializer(typeof(A)));
            var result = helper.SerializeAndDeserialize(new A())!;
            result.Should().Be(new A
            {
                EnumP = default,
                IntP = default,
                StringP = default,
            });
        }


        [Test]
        public void JsonSerializer_Default()
        {
            var helper = new JsonSerializerHelper<A>();
            var result = helper.SerializeAndDeserialize(new A())!;
            result.Should().Be(new A
            {
                EnumP = default,
                IntP = default,
                StringP = default,
            });
        }

        [Test]
        public void NewtonsoftJsonSerializer_Default()
        {
            var helper = new NewtonsoftJsonSerializerHelper<A>(new Nsoft.JsonSerializer());
            var result = helper.SerializeAndDeserialize(new A())!;
            result.Should().Be(new A
            {
                EnumP = default,
                IntP = default,
                StringP = default,
            });
        }

        [Test]
        public void Protobuf_Default()
        {
            var helper = new ProtobufSerializerHelper<A>();
            var result = helper.SerializeAndDeserialize(new A())!;
            result.Should().Be(new A
            {
                EnumP = default,
                IntP = default,
                StringP = default,
            });
        }


        [DataContract]
        [ProtoContract]
        public record A_Ext
        {
            [DefaultValue(200)]
            [DataMember(EmitDefaultValue = false)]
            [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
            [Nsoft.JsonProperty(DefaultValueHandling =
                Nsoft.DefaultValueHandling.Ignore)]
            [ProtoMember(1, IsRequired = true)]
            public int IntP { get; set; }
            [DataMember(EmitDefaultValue = false)]
            [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
            [Nsoft.JsonProperty(DefaultValueHandling =
                Nsoft.DefaultValueHandling.Ignore)]
            [ProtoMember(2, IsRequired = true)]
            public string? StringP { get; set; }
            [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
            [Nsoft.JsonProperty(DefaultValueHandling =
                Nsoft.DefaultValueHandling.Ignore)]
            [DataMember(EmitDefaultValue = false)]
            [ProtoMember(3, IsRequired = true)]
            public EnumOne EnumP { get; set; }
        }

        [Test]
        public void XmlSerializer_Ext()
        {
            var helper = new XmlSerializerHelper<A_Ext>(new XmlSerializer(typeof(A_Ext)));
            var result = helper.SerializeAndDeserialize(new A_Ext() { IntP = 200 })!;
            result.Should().Be(new A_Ext
            {
                EnumP = default,
                IntP = default,
                StringP = default,
            });
        }

        [Test]
        public void DataContractSerializer_Ext()
        {
            var helper = new DataContractSerializerHelper<A_Ext>(new DataContractSerializer(typeof(A_Ext)));
            var result = helper.SerializeAndDeserialize(new A_Ext())!;
            result.Should().Be(new A_Ext
            {
                EnumP = default,
                IntP = default,
                StringP = default,
            });
        }


        [Test]
        public void JsonSerializer_Ext()
        {
            var helper = new JsonSerializerHelper<A_Ext>();
            var result = helper.SerializeAndDeserialize(new A_Ext())!;
            result.Should().Be(new A_Ext
            {
                EnumP = default,
                IntP = default,
                StringP = default,
            });
        }

        [Test]
        public void NewtonsoftJsonSerializer_Ext()
        {
            var helper = new NewtonsoftJsonSerializerHelper<A_Ext>(new Nsoft.JsonSerializer());
            var result = helper.SerializeAndDeserialize(new A_Ext() { IntP = 200 })!;
            result.Should().Be(new A_Ext
            {
                EnumP = default,
                IntP = default,
                StringP = default,
            });
        }

        [Test]
        public void Protobuf_Ext()
        {
            var helper = new ProtobufSerializerHelper<A_Ext>();
            var result = helper.SerializeAndDeserialize(new A_Ext())!;
            result.Should().Be(new A_Ext
            {
                EnumP = default,
                IntP = default,
                StringP = default,
            });
        }






        /*
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
                <VersioningSample.A 
                    xmlns="http://schemas.datacontract.org/2004/07/ManageSerialization" 
                    xmlns:i="http://www.w3.org/2001/XMLSchema-instance">
                </VersioningSample.A>
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

        [Test]
        public void DataContractSerializer_HandleDefault()
        {
            var helper = new DataContractSerializerHelper<ADef>(new DataContractSerializer(typeof(ADef)));
            var result = helper.Deserialization(dataContractStr)!;
            result.Should().Be(new ADef
            {
                EnumP = EnumOne.Two,
                IntP = 100,
                StringP = "sss",
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
        */
    }
}
