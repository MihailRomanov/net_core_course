using Introduction.TestData;
using Newtonsoft.Json;

namespace Introduction
{
    public class NewtonsoftJsonSerializerSamples
    {
        const string FileName = "NewtonsoftJsonSerializer.json";

        [Test]
        public void Serialize()
        {
            var serializer = new JsonSerializer();
            using var textWriter = File.CreateText(FileName);

            serializer.Serialize(textWriter, PersonGenerator.GenerateList(10));
        }

        [Test]
        public void Deserialize()
        {
            var serializer = new JsonSerializer();
            using var textReader = File.OpenText(FileName);

            var persons = serializer.Deserialize<List<Person>>(new JsonTextReader(textReader))!;
            persons.ForEach(Console.WriteLine);
        }
    }
}
