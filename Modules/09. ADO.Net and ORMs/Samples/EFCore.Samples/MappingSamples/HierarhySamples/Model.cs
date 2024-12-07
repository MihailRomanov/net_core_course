using Microsoft.EntityFrameworkCore;

namespace MappingSamples.HierarhySamples
{
    public class Person
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }

    public class Employee : Person
    {
        public string Position { get; set; }
    }

    public class ContactPerson : Person
    {
        public string Organization { get; set; }
    }

    public class SampleDB : DbContext
    {
        public SampleDB(DbContextOptions options) : base(options) { }
        public DbSet<Person> People { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<ContactPerson> ContactPersons { get; set; }
    }
}
