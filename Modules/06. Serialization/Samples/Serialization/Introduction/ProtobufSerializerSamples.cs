using Introduction.TestData;
using ProtoBuf;

namespace Introduction
{
    public class ProtobufSerializerSamples
    {
        const string FileName = "ProtobufSerializer.dat";


        [Test]
        public void Serialize()
        {
            using var stream = new FileStream(FileName, FileMode.Create);

            Serializer.Serialize(stream, PersonGenerator.GenerateList(10));
        }

        [Test]
        public void Deserialize()
        {
            using var stream = new FileStream(FileName, FileMode.Open);

            var persons = Serializer.Deserialize<List<Person>>(stream);
            persons.ForEach(Console.WriteLine);
        }
    }
}
