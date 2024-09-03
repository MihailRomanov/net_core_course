using Sample03.OpenLibraryClient;

namespace Sample03
{
    public class BookSearchClientTest
    {
        [Test]
        public void RawBookSerachTest()
        {
            var client = new BookSearchClient();

            var query = "title:\"the lord of the rings\" AND author_name:\"Patrick Gardner\" AND publisher:\"SparkNotes\"";
            var result = client.Search(query);
            Console.WriteLine(result.Found);
        }

        [Test]
        public void QueryableBookSerachTest()
        {
            var bookSet = new BookSet();

            var result = bookSet
                .Where(b => b.Title == "the lord of the rings")
                .ToList();

            foreach (var book in result)
            {
                Console.WriteLine($"{book.Title} | {book.Author.FirstOrDefault()}");
            }
        }

    }
}