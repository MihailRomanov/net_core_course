namespace AuthLib
{
    public interface IFailCountStore
    {
        int Fails { get; }
        void AddFail();
        void Reset();
    }
}
