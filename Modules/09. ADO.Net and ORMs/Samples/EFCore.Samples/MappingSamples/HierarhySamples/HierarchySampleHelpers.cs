using FluentAssertions;

namespace MappingSamples.HierarhySamples
{
    internal static class HierarchySamplesHelper
    {
        public static void WriteAndRead(SampleDB db)
        {
            Person[] people = [
                new Person { FirstName = "FirstName1", LastName = "LastName1" },
                new ContactPerson { FirstName = "FirstName2", LastName = "LastName2", Organization = "Org2" },
                new Employee{ FirstName = "FirstName3", LastName = "LastName3", Position="Pos3" },
                ];

            db.People.AddRange(people);
            db.SaveChanges();

            var people2 = db.People.ToArray();
            people2.Should().BeEquivalentTo(people);
        }
    }
}