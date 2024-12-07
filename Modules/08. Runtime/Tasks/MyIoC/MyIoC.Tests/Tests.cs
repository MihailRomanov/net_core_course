using MyIoC.Tests.TestModel;

namespace MyIoC.Tests
{
    public class Tests
    {
        [Test]
        public void RegisterAssembly()
        {
            var container = new Container();
            container.AddAssembly(typeof(ICustomerDAL).Assembly);

            var customerBLL = (CustomerBLL)container.CreateInstance(typeof(CustomerBLL));
            var customerBLL2 = container.CreateInstance<CustomerBLL2>();
        }

        [Test]
        public void RegisterByTypes()
        {
            var container = new Container();
            container.AddType(typeof(CustomerBLL));
            container.AddType(typeof(Logger));
            container.AddType(typeof(ICustomerDAL), typeof(CustomerDAL));
            container.AddType<CustomerBLL2>();

            var customerBLL = (CustomerBLL)container.CreateInstance(typeof(CustomerBLL));
            var customerBLL2 = container.CreateInstance<CustomerBLL2>();
        }
    }
}