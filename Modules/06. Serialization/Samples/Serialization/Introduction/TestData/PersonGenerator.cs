using Bogus;

namespace Introduction.TestData
{
    internal static class PersonGenerator
    {
        private static Faker<Person> personFaker;

        static PersonGenerator()
        {
            personFaker = new Faker<Person>("ru")
                .StrictMode(false)
                .RuleFor(p => p.Name, f => f.Name.FullName())
                .RuleFor(p => p.Age, f => f.Random.Int(0, 100))
                .RuleFor(p => p.Gender, f => f.PickRandom<Gender>());
        }

        public static List<Person> GenerateList(int count)
        {
            return personFaker.Generate(count);
        }
    }
}
