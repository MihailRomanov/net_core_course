namespace TestingAndDebuggingSamples.TestDataGenerators
{
    public interface ITestDataGenerator<T> where T : class
    {
        T Generate();
        IEnumerable<T> Generate(int count);
    }
}
