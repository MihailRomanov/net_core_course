using Generators.Models;

namespace Generators
{
    public class XmlSchemaExportersTests : ExportersTestsBase
    {
        private const string xmlSerialserResultPath = "exports\\XmlSerializer";
        private const string dataContractSerializerResultPath = "exports\\DataContractSerializer";

        [Test]
        public void ExportXmlSerializerSchemasTest()
        {
            var exportDir = PrepareTestDir(xmlSerialserResultPath);
            XmlSchemaExporters.ExportXmlSerializerSchemas(typeof(Catalog), exportDir);
        }

        [Test]
        public void ExportDataContractSchemasTest()
        {
            var exportDir = PrepareTestDir(dataContractSerializerResultPath);
            XmlSchemaExporters.ExportDataContractSchemas(typeof(Catalog), exportDir);
        }

        [Test]
        public void ImportDataContractSchemasTest()
        {
            var import = Resources.Resource.XmlSchema;
            var code = XmlSchemaExporters.ImportDataContractSchemas(import);

            Console.WriteLine(code);
        }
    }
}
