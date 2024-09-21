using System.Xml.Serialization;
using Introduction.TestData;

namespace Introduction
{
    public class XmlSerializerSamples
    {
        const string FileName = "XmlSerializer.xml";

        [Test]
        public void Serialize()
        {
            var serializer = new XmlSerializer(typeof(List<Person>));
            using var stream = new FileStream(FileName, FileMode.Create);

            serializer.Serialize(stream, PersonGenerator.GenerateList(10));
        }

        [Test]
        public void Deserialize()
        {
            var serializer = new XmlSerializer(typeof(List<Person>));
            using var stream = new FileStream(FileName, FileMode.Open);

            var persons = (List<Person>)serializer.Deserialize(stream)!;
            persons.ForEach(Console.WriteLine);
        }
    }
}
