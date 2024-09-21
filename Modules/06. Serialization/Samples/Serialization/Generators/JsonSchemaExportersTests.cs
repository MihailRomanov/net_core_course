using Generators.Models;

namespace Generators
{
    public class JsonSchemaExportersTests : ExportersTestsBase
    {
        private const string JsonEverythingResultPath = "exports\\json-everything";
        private const string NJsonSchemaForTextJsonResultPath = "exports\\NJsonSchema\\SystemTextJson";
        private const string NJsonSchemaForNewtonsoftJsonResultPath = "exports\\NJsonSchema\\NewtonsoftJson";

        [Test]
        public void ExportJsonEverythingSchema()
        {
            var exportDir = PrepareTestDir(JsonEverythingResultPath);
            JsonSchemaExporters.ExportJsonEverythingSchema(typeof(Catalog), exportDir);
        }
        
        [Test]
        public void ExportNJsonSchemaForTextJson()
        {
            var exportDir = PrepareTestDir(NJsonSchemaForTextJsonResultPath);
            JsonSchemaExporters.ExportNJsonSchema(typeof(Catalog), exportDir);
        }

        [Test]
        public void ExportNJsonSchemaForNewtonsoftJson()
        {
            var exportDir = PrepareTestDir(NJsonSchemaForNewtonsoftJsonResultPath);
            JsonSchemaExporters.ExportNJsonSchema(typeof(Catalog), exportDir, true);
        }

        [Test]
        public void ImportNJsonSchemaForTextJson()
        {
            var schema = Resources.Resource.JsonSchema;
            var code = JsonSchemaExporters.ImportNJsonSchema(schema);
            Console.WriteLine(code);
        }

        [Test]
        public void ImportNJsonSchemaForNewtonsoftJson()
        {
            var schema = Resources.Resource.JsonSchema;
            var code = JsonSchemaExporters.ImportNJsonSchema(schema, true);
            Console.WriteLine(code);
        }

    }
}
