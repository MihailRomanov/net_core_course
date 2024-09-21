using System.Xml.Serialization;
using System.Runtime.Serialization;
using ManageSerialization.Helpers;
using FluentAssertions;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ManageSerialization.BaseSamples
{

    public class GraphSerializationSample
    {
        public record A
        {
            public int P1 { get; set; }
        }

        public record B
        {
            public A A1 { get; set; }
            public A A2 { get; set; }
        }

        public B b;

        [SetUp]
        public void Initialize()
        {
            A a = new A { P1 = 1 };
            b = new B { A1 = a, A2 = a };
        }

        [Test]
        public void XmlSerializer()
        {
            b.A1.Should().BeSameAs(b.A2);

            var helper = new XmlSerializerHelper<B>(new XmlSerializer(typeof(B)));
            var result = helper.SerializeAndDeserialize(b)!;

            result.A1.Should().NotBeSameAs(result.A2);

        }

        [Test]
        public void DataContractSerializer_Tree()
        {
            b.A1.Should().BeSameAs(b.A2);

            var helper = new DataContractSerializerHelper<B>(new DataContractSerializer(typeof(B)));
            var result = helper.SerializeAndDeserialize(b)!;

            result.A1.Should().NotBeSameAs(result.A2);
        }


        [Test]
        public void DataContractSerializer_Graph()
        {
            b.A1.Should().BeSameAs(b.A2);

            var helper = new DataContractSerializerHelper<B>(new DataContractSerializer(typeof(B),
                new DataContractSerializerSettings { PreserveObjectReferences = true }));
            var result = helper.SerializeAndDeserialize(b)!;

            result.A1.Should().BeSameAs(result.A2);
        }


        [Test]
        public void JsonSerializer_Tree()
        {
            b.A1.Should().BeSameAs(b.A2);

            var helper = new JsonSerializerHelper<B>();
            var result = helper.SerializeAndDeserialize(b)!;

            result.A1.Should().NotBeSameAs(result.A2);
        }

        [Test]
        public void JsonSerializer_Graph()
        {
            b.A1.Should().BeSameAs(b.A2);

            var helper = new JsonSerializerHelper<B>(
                new JsonSerializerOptions
                {
                    ReferenceHandler = ReferenceHandler.Preserve
                });
            var result = helper.SerializeAndDeserialize(b)!;

            result.A1.Should().BeSameAs(result.A2);
        }

    }
}
