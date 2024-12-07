namespace MyIoC.Annotation
{
    /// <summary>
    /// Помечает, что в данный класс нужно делать инъекции через конструктор
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class ImportConstructorAttribute : Attribute
    {
    }
}
