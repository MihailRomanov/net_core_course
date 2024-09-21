using Generators.Models;
using System.Text.Json;

namespace Generators
{
    public class ModelSamples
    {
        [Test]
        public void SerializeModel()
        {
            var catalog = CatalogGenerator.GenerateCatalog();
            var str = JsonSerializer.Serialize(catalog);

            Console.WriteLine(str);
        }

    }
}
