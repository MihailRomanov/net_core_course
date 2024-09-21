using System.Runtime.Serialization;
using Introduction.TestData;

namespace Introduction
{
    public class DataContractSerializerSamples
    {
        const string FileName = "DataContractSerializer.xml";

        [Test]
        public void Serialize()
        {
            var serializer = new DataContractSerializer(typeof(List<Person>));
            using var stream = new FileStream(FileName, FileMode.Create);

            serializer.WriteObject(stream, PersonGenerator.GenerateList(10));
        }

        [Test]
        public void Deserialize()
        {
            var serializer = new DataContractSerializer(typeof(List<Person>));
            using var stream = new FileStream(FileName, FileMode.Open);

            var persons = (List<Person>)serializer.ReadObject(stream)!;
            persons.ForEach(Console.WriteLine);
        }
    }
}
