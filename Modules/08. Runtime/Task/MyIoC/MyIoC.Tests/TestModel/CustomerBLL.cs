using MyIoC.Annotation;

namespace MyIoC.Tests.TestModel
{
    [Export]
    [ImportConstructor]
    public class CustomerBLL(ICustomerDAL customerDAL, Logger logger)
    {
    }
}
