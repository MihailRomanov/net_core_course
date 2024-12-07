using MyIoC.Annotation;

namespace MyIoC.Tests.TestModel
{
    public class CustomerBLL2
    {
        [Import]
        private ICustomerDAL CustomerDAL { get; set; }
        [Import]
        private Logger Logger { get; set; }
    }
}
