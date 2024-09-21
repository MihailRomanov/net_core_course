using Microsoft.CSharp;
using System.Runtime.Serialization;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Generators
{
    public static class XmlSchemaExporters
    {
        public static bool ExportXmlSerializerSchemas(Type type, string exportPath)
        {
            var importer = new XmlReflectionImporter();
            var schemas = new XmlSchemas();
            var exporter = new XmlSchemaExporter(schemas);

            var xmlTypeMapping = importer.ImportTypeMapping(type);
            exporter.ExportTypeMapping(xmlTypeMapping);

            var hasError = false;

            schemas.Compile(new ValidationEventHandler(
                (object _, ValidationEventArgs args) =>
                {
                    Console.WriteLine($"{args.Exception.LineNumber}:{args.Exception.LinePosition} - {args.Message}");
                    if (args.Severity == XmlSeverityType.Error)
                        hasError = true;
                }), true);

            if (hasError)
                return false;

            SaveSchemas(type, exportPath, schemas);

            return true;
        }

        public static bool ExportDataContractSchemas(Type type, string exportPath)
        {
            var exporter = new XsdDataContractExporter();
            if (!exporter.CanExport(type))
                return false;

            exporter.Export(type);
            var xsdNamespace = exporter.GetRootElementName(type)!.Namespace;

            var schemas = exporter.Schemas.Schemas(xsdNamespace).Cast<XmlSchema>();
            SaveSchemas(type, exportPath, schemas);

            return true;
        }

        public static string? ImportDataContractSchemas(string xsdSchema)
        {
            var schema = XmlSchema.Read(new StringReader(xsdSchema), null)!;
            var schemaSet = new XmlSchemaSet();
            schemaSet.Add(schema);
            schemaSet.Compile();

            var importer = new XsdDataContractImporter();

            if (!importer.CanImport(schemaSet))
                return null;

            importer.Import(schemaSet);

            var provider = CSharpCodeProvider.CreateProvider("CSharp");
            var stringWriter = new StringWriter();
            var generator = provider.CreateGenerator(stringWriter);

            generator.GenerateCodeFromCompileUnit(importer.CodeCompileUnit, 
                stringWriter, new System.CodeDom.Compiler.CodeGeneratorOptions());

            stringWriter.Close();

            return stringWriter.ToString();
        }

        private static void SaveSchemas(Type type, string exportPath, IEnumerable<XmlSchema> schemas)
        {
            int i = 0;
            foreach (XmlSchema s in schemas)
            {
                var file = Path.Combine(exportPath, $"{type.Name}_{i:d2}.xsd");
                using var writer = File.CreateText(file);
                s.Write(writer);
                i++;
            }
        }
    }
}
