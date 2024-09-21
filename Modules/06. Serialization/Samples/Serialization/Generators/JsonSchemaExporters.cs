using Json.Schema;
using Json.Schema.Generation;
using NJsonSchema.CodeGeneration.CSharp;
using NJsonSchema.Generation;
using NJsonSchema.NewtonsoftJson.Generation;
using System.Text.Json;

namespace Generators
{
    public static class JsonSchemaExporters
    {
        public static void ExportJsonEverythingSchema(Type type, string exportPath)
        {
            var builder = new JsonSchemaBuilder();
            var jsonSchema = builder
                .Title(type.Name)
                .Schema(MetaSchemas.Draft6.BaseUri)
                .FromType(type)
                .Build();

            var file = Path.Combine(exportPath, $"{type.Name}.json");
            File.WriteAllText(file, JsonSerializer.Serialize(jsonSchema));
        }

        public static void ExportNJsonSchema(Type type, string exportPath,
            bool forNewtonsoft = false)
        {
            JsonSchemaGeneratorSettings settings =
                !forNewtonsoft
                ? new SystemTextJsonSchemaGeneratorSettings()
                : new NewtonsoftJsonSchemaGeneratorSettings();

            var generator = new JsonSchemaGenerator(settings);
            var jsonSchema = generator.Generate(type).ToJson();

            var file = Path.Combine(exportPath, $"{type.Name}.json");
            File.WriteAllText(file, jsonSchema);
        }

        public static string ImportNJsonSchema(string schemaString,
            bool forNewtonsoft = false)
        {
            var library =
                !forNewtonsoft
                ? CSharpJsonLibrary.SystemTextJson
                : CSharpJsonLibrary.NewtonsoftJson;

            var schema = NJsonSchema.JsonSchema
                .FromJsonAsync(schemaString)
                .Result;

            var generator = new CSharpGenerator(schema,
                new CSharpGeneratorSettings { JsonLibrary = library });
            return generator.GenerateFile();
        }
    }
}
