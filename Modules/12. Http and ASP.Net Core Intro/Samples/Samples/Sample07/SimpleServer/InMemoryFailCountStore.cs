using AuthLib;

namespace SimpleServer
{
    public class InMemoryFailCountStore : IFailCountStore
    {
        public int Fails { get; private set; } = 0;

        public void AddFail()
        {
            Fails++;
        }

        public void Reset()
        {
            Fails = 0;
        }
    }
}
