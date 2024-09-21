using Generators.Models;

namespace Generators
{
    public partial class ProtoFileExporters
    {
        public class ProtoFileExportersTests : ExportersTestsBase
        {
            private const string ProtoBufNetResultPath = "exports\\protobuf-net";

            [Test]
            public void ExportJsonEverythingSchema()
            {
                var exportDir = PrepareTestDir(ProtoBufNetResultPath);
                ProtoFileExporters.ExportProtoFile(typeof(Catalog), exportDir);
            }
        }
    }
}
