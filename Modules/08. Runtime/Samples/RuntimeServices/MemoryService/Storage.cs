using System.Collections.Concurrent;

namespace MemoryService
{
    internal class StoreItem
    {
        private readonly long[][] data;
        private readonly Guid id;
        public StoreItem(Guid id, int count)
        {
            this.id = id;
            data = new long[count][];
            for (int i = 1; i < count; i++)
            {
                var t = new long[2000];
                Array.Clear(t);
                data[i] = t;
            }
        }
    }

    internal class Storage
    {
        private readonly ConcurrentDictionary<Guid, StoreItem> store = new();
        private Random random = new Random();

        public void Add(int count)
        {
            for (var i = 0; i < count; i++)
            {
                var id = Guid.NewGuid();
                var iCount = random.Next(1000, 2000);

                store.TryAdd(id, new StoreItem(id, iCount));
            }
        }

        public void Remove(int count)
        {
            var forDelete = store.Keys.Order().Take(count).ToList();
            forDelete.ForEach(k => store.Remove(k, out var t));
        }

        public int Size => store.Count;
    }
}
