namespace MyIoC.Annotation
{
    /// <summary>
    /// Помечает поле или свойство как точку инъекции
    /// </summary>
    [AttributeUsage(AttributeTargets.Property 
        | AttributeTargets.Field)]
    public class ImportAttribute : Attribute
    {
    }
}
