using MyIoC.Annotation;

namespace MyIoC.Tests.TestModel
{
    [Export(typeof(ICustomerDAL))]
    public class CustomerDAL : ICustomerDAL
    { }
}