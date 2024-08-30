using System.IO.Abstractions;

namespace BookStoreLibrary.Implementation
{
    public abstract class RepositoryBase<TEntity> where TEntity : class
    {
        private readonly IFileSystem fileSystem;
        private string bookStoreFileName;
        protected string delim = "|||";

        public RepositoryBase(string bookStoreFileName, IFileSystem fileSystem)
        {
            this.bookStoreFileName = bookStoreFileName;
            this.fileSystem = fileSystem;
        }


        protected IEnumerable<TEntity> Load()
        {
            foreach (var line in fileSystem.File.ReadAllLines(bookStoreFileName))
                yield return ToEntity(line);
        }

        protected abstract TEntity ToEntity(string line);

    }
}
