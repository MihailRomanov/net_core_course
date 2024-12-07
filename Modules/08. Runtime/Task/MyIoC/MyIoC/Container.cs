using System.Reflection;

namespace MyIoC
{
    /// <summary>
    /// IoC контейнер
    /// </summary>
    public class Container
    {
        /// <summary>
        /// Регистрирует все типы из сборки, 
        /// помеченные <see cref="Annotation.ExportAttribute"/> 
        /// </summary>
        public void AddAssembly(Assembly assembly)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Регистрирует тип, который сервис и его реализация
        /// </summary>
        public void AddType(Type type)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Регистрирует тип, который сервис и его реализация
        /// </summary>
        public void AddType<T>()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Регистрирует сервис и тип который этот сервис реализует
        /// </summary>
        public void AddType(Type service, Type implementation)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Создает полностью настроенный тип
        /// </summary>
        public object CreateInstance(Type type)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Создает запрошенный тип
        /// </summary>
        public T CreateInstance<T>()
        {
            throw new NotImplementedException();
        }
    }
}
