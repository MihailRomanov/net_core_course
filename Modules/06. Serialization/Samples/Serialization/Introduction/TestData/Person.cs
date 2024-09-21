using ProtoBuf;

namespace Introduction.TestData
{
    public enum Gender
    {
        Male,
        Female
    }

    [ProtoContract(ImplicitFields = ImplicitFields.AllPublic)]
    public record Person
    {
        public string Name { get; set; } = "";
        public int Age { get; set; }
        public Gender Gender { get; set; }
    }
}
