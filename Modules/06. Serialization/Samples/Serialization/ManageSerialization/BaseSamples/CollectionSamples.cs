using System.Xml.Serialization;
using System.Runtime.Serialization;
using ManageSerialization.Helpers;

namespace ManageSerialization.BaseSamples
{
    public class CollectionSamples
    {
        public class AXml
        {
            [XmlArray("Cities"), XmlArrayItem("City")]
            public List<string> C { get; set; }
        }


        [Test]
        public void XmlSerializer()
        {
            var instance = new AXml
            {
                C = ["Moscow", "Izhevsk", "Minsk"]
            };

            var tester = new XmlSerializerHelper<AXml>(new XmlSerializer(typeof(AXml)));
            tester.SerializeAndDeserialize(instance);
        }


        [CollectionDataContract(Name = "Cities", ItemName = "City")]
        public class Cities : List<string> { };

        public class ADataContract
        {
            public Cities C { get; set; }
        }

        [Test]
        public void DataContractSerializer()
        {
            var instance = new ADataContract
            {
                C = ["Moscow", "Izhevsk", "Minsk"]
            };

            var tester = new DataContractSerializerHelper<ADataContract>(new DataContractSerializer(typeof(ADataContract)));
            tester.SerializeAndDeserialize(instance);
        }

    }
}
