using ProtoBuf.Meta;

namespace Generators
{
    public partial class ProtoFileExporters
    {
        public static void ExportProtoFile(Type type, string exportPath)
        {
            var model = RuntimeTypeModel.Create();
            model.UseImplicitZeroDefaults = false;

            var proto = model.GetSchema(type, ProtoSyntax.Default);

            var file = Path.Combine(exportPath, $"{type.Name}.proto");
            File.WriteAllText(file, proto);
        }
    }
}
