using Bogus;

namespace Generators.Models
{
    internal static class CatalogGenerator
    {
        private static readonly Faker<Book> bookFaker;
        private static readonly Faker<Author> authorFaker;
        private static readonly Faker<Catalog> catalogFaker;

        static CatalogGenerator()
        {
            authorFaker = new Faker<Author>()
                .RuleFor(a => a.FirstName, f => f.Name.FirstName())
                .RuleFor(a => a.LastName, f => f.Name.LastName())
                .RuleFor(a => a.BirthYear, f => f.Random.Int(1100, 2000));

            bookFaker = new Faker<Book>()
                .RuleFor(b => b.Title, f => f.Lorem.Sentence())
                .RuleFor(b => b.Authors, f => authorFaker.Generate(f.Random.Int(1, 3)))
                .RuleFor(b => b.Pages, f => f.Random.Int(100, 2000));

            catalogFaker = new Faker<Catalog>()
                .RuleFor(c => c.Books, f => bookFaker.Generate(f.Random.Int(3, 10)));                            
        }

        public static Catalog GenerateCatalog()
        {
            return catalogFaker.Generate();
        }
    }
}
