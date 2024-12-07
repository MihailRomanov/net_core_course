# Задание "Reflection"

## Общая часть
Наша задача: разработать IoC-контейнер (следуя принципу «Каждый программист должен разработать свой IoC/DI контейнер» ©).

В качестве примера мы возьмем Managed Extensibility Framework (MEF), в котором основная настройка контейнера происходит за счет расстановки атрибутов. 

Но (!) весь код, включая объявление атрибутов у нас будет свой.

**Наброски кода можно взять в папке [MyIoC](./MyIoC)**


## Задание 1
Используя механизмы Reflection, создайте простейший IoC-контейнер, который позволяет следующее:

- Разметить классы, требующие внедрения зависимостей одним из следующих способов (конкретный класс, размечается только одним способом):
    - Через конструктор (тогда класс размечается атрибутом `[ImportConstructor]`)
    - Через публичные свойства (тогда каждое свойство, требующее инициализации,  размечается атрибутом `[Import]`)

``` CSharp
[ImportConstructor]
public class CustomerBLL
{
    public CustomerBLL(ICustomerDAL dal, Logger logger)
    { }
}
```
``` CSharp
public class CustomerBLL
{
    [Import]
    public ICustomerDAL CustomerDAL { get; set; }
    [Import]
    public Logger logger { get; set; }
}
```
   
- Разметить зависимые классы
    - Когда класс используется непосредственно (т.е. он является сразу и сервисом и его реализацией)
    - Когда в классах, требующих реализации зависимости используется интерфейс или базовый класс

``` CSharp
[Export]
public class Logger
{ }
```

``` CSharp
[Export(typeof(ICustomerDAL))]
public class CustomerDAL : ICustomerDAL
{ }
```

- Явно регистрировать классы, от которых могут зависеть другие (т.е. явно регистрировать сервисы) + классы которые можно будет получать через контейнер
``` CSharp
var container = new Container();
container.AddType(typeof(CustomerBLL));
container.AddType(typeof(Logger));
container.AddType(typeof(CustomerDAL), typeof(ICustomerDAL));
```

- Добавить в контейнер все размеченные атрибутами [ImportConstructor], [Import] и [Export], указав сборку
``` CSharp
var container = new Container();
container.AddAssembly(Assembly.GetExecutingAssembly());
```

- Получить экземпляр ранее зарегистрированного класса со всеми зависимостями 
``` CSharp
var customerBLL = (CustomerBLL)container.CreateInstance(
                typeof(CustomerBLL));
var customerBLL = container.CreateInstance<CustomerBLL>();
```

## Задание 2 (необязательное!!!) 
Доработайте контейнер из Задания 1, так чтобы для создания экземпляра использовался сгенерированный кода на основе механизм System.Reflection.Emit (можно использовать сторонние библиотеки для удобства)