namespace MyIoC.Annotation
{
    /// <summary>
    /// Помечает, классы, как реализацию сервиса
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class ExportAttribute : Attribute
    {
        public ExportAttribute()
        { }

        public ExportAttribute(Type contract)
        {
            Contract = contract;
        }

        /// <summary>
        /// Если сервисом является не сам тип, 
        /// а один из реализуемых интерфейсов, нужно это указать тут
        /// </summary>
        public Type? Contract { get; private set; }
    }
}
