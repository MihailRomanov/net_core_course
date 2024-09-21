using System.Text.Json;
using Introduction.TestData;

namespace Introduction
{
    public class JsonSerializerSamples
    {
        const string FileName = "JsonSerializer.json";

        [Test]
        public void Serialize()
        {
            using var stream = new FileStream(FileName, FileMode.Create);

            JsonSerializer.Serialize(stream, PersonGenerator.GenerateList(10));
        }

        [Test]
        public void Deserialize()
        {
            using var stream = new FileStream(FileName, FileMode.Open);

            var persons = JsonSerializer.Deserialize<List<Person>>(stream)!;
            persons.ForEach(Console.WriteLine);
        }
    }
}
