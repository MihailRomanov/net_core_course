using System.Xml.Serialization;
using ManageSerialization.Helpers;
using Newtonsoft.Json.Serialization;

namespace ManageSerialization.BaseSamples
{
    public class CustomizeOutputSample
    {
        [XmlType(TypeName = "ClassA")]
        public class XmlS
        {
            [XmlAttribute]
            public string PropertyA { get; set; }
            [XmlAttribute("B")]
            public string PropertyB { get; set; }
            [XmlElement("C")]
            public string PropertyC { get; set; }
        }

        [Test]
        public void XmlSerializer()
        {
            var helper = new XmlSerializerHelper<XmlS>(new XmlSerializer(typeof(XmlS)));
            helper.SerializeAndDeserialize(
                new XmlS
                {
                    PropertyA = "A",
                    PropertyB = "B",
                    PropertyC = "C"
                });
        }

        public class JsonS
        {
            public string PropertyA { get; set; }
            public string PropertyB { get; set; }
            public string PropertyC { get; set; }
        }

        [Test]
        [TestCase(TypeArgs = [typeof(CamelCaseNamingStrategy)])]
        [TestCase(TypeArgs = [typeof(DefaultNamingStrategy)])]
        [TestCase(TypeArgs = [typeof(SnakeCaseNamingStrategy)])]
        public void NewtonsoftJsonSerializerNamingStrategy<T>() where T : NamingStrategy, new()
        {
            var serializer = new Newtonsoft.Json.JsonSerializer()
            {
                ContractResolver = new DefaultContractResolver
                {
                    NamingStrategy = new T()
                }
            };

            var helper = new NewtonsoftJsonSerializerHelper<JsonS>(serializer);
            helper.SerializeAndDeserialize(
                new JsonS
                {
                    PropertyA = "A",
                    PropertyB = "B",
                    PropertyC = "C"
                });
        }
    }
}
