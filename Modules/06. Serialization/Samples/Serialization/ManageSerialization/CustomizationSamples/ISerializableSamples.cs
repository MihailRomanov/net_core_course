using System.Runtime.Serialization;
using FluentAssertions;
using ManageSerialization.Helpers;

namespace ManageSerialization.CustomizationSamples
{
    public class ISerializableSamples
    {
        [Serializable]
        public class D : ISerializable
        {
            public string PropertyA { get; set; }
            public int PropertyB { get; set; }

            public D()
            {
                Console.WriteLine("Call default D constructor");
            }

            private D(SerializationInfo information, StreamingContext context)
            {
                PropertyA = information.GetString("PropertyA")!;
                PropertyB = information.GetInt32("PropertyB")!;
            }

            public void GetObjectData(SerializationInfo info, StreamingContext context)
            {
                info.AddValue("PropertyA", PropertyA);
                info.AddValue("PropertyB", PropertyB);
                info.AddValue("TimeStamp", DateTime.UtcNow);
            }
        }


        [Test]
        public void DataContractSerializer()
        {
            var tester = new DataContractSerializerHelper<D>(
                new DataContractSerializer(typeof(D)));
            var d1 = new D() { PropertyA = "AAAAA" };
            var d2 = tester.SerializeAndDeserialize(d1);

            d2.Should().BeEquivalentTo(d1);
        }

        [Test]
        public void NewtonsoftJsonSerializer()
        {
            var tester = new NewtonsoftJsonSerializerHelper<D>(
                new Newtonsoft.Json.JsonSerializer());
            var d1 = new D() { PropertyA = "AAAAA" };
            var d2 = tester.SerializeAndDeserialize(d1);

            d2.Should().BeEquivalentTo(d1);
        }

    }
}
