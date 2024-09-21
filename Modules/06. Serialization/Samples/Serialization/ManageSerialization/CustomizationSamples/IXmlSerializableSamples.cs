using System.Xml.Serialization;
using System.Runtime.Serialization;
using System.Xml;
using System.Xml.Schema;
using ManageSerialization.Helpers;
using FluentAssertions;

namespace ManageSerialization.CustomizationSamples
{
    public class IXmlSerializableSamples
    {
        [Serializable]
        public class E : IXmlSerializable
        {
            public string PropertyA { get; set; }

            public int PropertyB { get; set; }

            public E()
            {
                Console.WriteLine("Call E constructor");
            }

            public XmlSchema GetSchema()
            {
                return null;
            }

            public void ReadXml(XmlReader reader)
            {
                reader.ReadStartElement();
                PropertyA = reader.ReadElementContentAsString();
            }

            public void WriteXml(XmlWriter writer)
            {
                writer.WriteStartElement("PropertyA");
                writer.WriteValue(PropertyA);
                writer.WriteEndElement();
            }
        }

        [Test]
        public void XmlSerializer()
        {
            var tester = new XmlSerializerHelper<E>(
                new XmlSerializer(typeof(E)));
            var e1 = new E() { PropertyA = "EEEEE" };
            var e2 = tester.SerializeAndDeserialize(e1);

            e1.Should().BeEquivalentTo(e2);
        }

        [Test]
        public void DataContractSerializer()
        {
            var tester = new DataContractSerializerHelper<E>(
                new DataContractSerializer(typeof(E)));
            var e1 = new E() { PropertyA = "EEEEE" };
            var e2 = tester.SerializeAndDeserialize(e1);

            e1.Should().BeEquivalentTo(e2);
        }
    }
}
