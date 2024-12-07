using System.Reflection;

namespace Reflection
{
    public class TestClass
    {
        private int Field;
        public int IntProp { get; set; }

        public TestClass() { }
        public TestClass(int a) { IntProp = Field = a; }

        public string Method(int a)
        {
            return $"{Field} {IntProp + a}";
        }

        public string Method(double a)
        {
            return $"{Field} {IntProp + a}";
        }
    }

    public class DerivedClass<T> : TestClass where T : struct
    {
        private T AdditionalField;
    }

    public class ReflectionSamples
    {
        [Test]
        public void GetTypeSamples()
        {
            object obj = new TestClass();

            // Для любого экземпляра System.Object
            var typeFromGetType = obj.GetType();

            // Для известного на этапе компиляции типа
            var typeFromTypeOf = typeof(TestClass);

            // Получение всех типов в сборке и поиск нужного
            var typeFromAssemblyTypes =
                typeof(TestClass).Assembly.GetTypes()
                .SingleOrDefault(t => t.FullName == "Reflection.TestClass");

            Console.WriteLine(typeFromGetType.FullName);
            Console.WriteLine(typeFromTypeOf.FullName);
            Console.WriteLine(typeFromAssemblyTypes?.FullName);

        }

        [Test]
        public void GetTypeInfoSamples()
        {
            object obj = new TestClass();

            // Получаем через GetType
            var type = obj.GetType().GetTypeInfo();

            // У сборки можно напрямую
            var typeFromAssemblyTypes =
                typeof(TestClass).Assembly
                .DefinedTypes
                .Single(t => t.FullName == "Reflection.TestClass");

            Console.WriteLine(type.FullName);
            Console.WriteLine(string.Join("\n",
                type
                .DeclaredMembers
                .GroupBy(
                    t => t.Name,
                    (k, e) => (k, e.Count()))));
        }


        [Test]
        public void GetTypeMembers()
        {
            object obj = new TestClass();

            var type = obj.GetType();

            // Все публичные члены
            foreach (var m in type.GetMembers())
            {
                Console.WriteLine($"{m.Name} : {m.DeclaringType}");
            }
            Console.WriteLine();

            // Все непубличные экземплярные  члены
            foreach (var m in type.GetMembers(
                BindingFlags.NonPublic | BindingFlags.Instance))
            {
                Console.WriteLine($"{m.Name} : {m.DeclaringType}");
            }
            Console.WriteLine();

            // Метод с именем «Method», если такой есть
            // Падает из-за перегрузки Method
            //var method = type.GetMethod("Method");
            //Console.WriteLine($"{method.Name} : {method.DeclaringType}");
            Console.WriteLine();

            // Метод с сигнатурой Method(int)
            var method2 = type.GetMethod("Method", [typeof(int)]);
            Console.WriteLine($"{method2.Name} : {method2.DeclaringType}");
        }


        [Test]
        public void GetGenericType()
        {
            var genericType = typeof(DerivedClass<>);
            PrintTypeInfo(genericType);

            var genericType2 = typeof(DerivedClass<int>);
            PrintTypeInfo(genericType2);

            var genericType3 = genericType.MakeGenericType([typeof(double)]);
            PrintTypeInfo(genericType3);

            static void PrintTypeInfo(Type type)
            {
                Console.WriteLine(
                    $"{type.Name} {type.IsGenericType} {type.IsGenericTypeDefinition}");
            }
        }


        [Test]
        public void CreateInstance()
        {
            var type = typeof(TestClass);

            // С конструктором по умолчанию
            var i1 = Activator.CreateInstance<TestClass>();

            // С конструктором по умолчанию, но из типа
            var i2 = Activator.CreateInstance(type);

            // Через конструктор TestClass(int)
            var i3 = Activator.CreateInstance(type, [5]);


            var genericDefinitionType = typeof(DerivedClass<>);

            // Указываем конкретный generic тип
            var i4 = Activator.CreateInstance<DerivedClass<int>>();

            // Если у нас только definition - конкретизируем
            var i5 = Activator.CreateInstance(
                genericDefinitionType
                .MakeGenericType([typeof(double)]));
        }

        [Test]
        public void AccessToMembers()
        {
            var testClass = new TestClass() { IntProp = 3 };
            var type = testClass.GetType();

            // Получаем свойство IntProperty
            var intProp = type.GetProperty("IntProp")!;

            // Получаем текущее значение
            int currentValue = (int)intProp.GetValue(testClass)!;
            Console.WriteLine(currentValue);

            // Меняем текущее значение
            intProp.SetValue(testClass, currentValue + 5);
            Console.WriteLine(testClass.IntProp);

            // Получаем метод Method(int) и вызываем его
            var intMethod = type.GetMethod("Method", [typeof(int)])!;
            var result = (string)intMethod.Invoke(testClass, [6])!;

            Console.WriteLine(result);
        }
    }
}