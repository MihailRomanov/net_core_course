using System.Runtime.Serialization;
using System.Text;
using ManageSerialization.Helpers;

namespace ManageSerialization.CustomizationSamples
{
    public class SerializationEventSamples
    {

        [DataContract(Name = "C")]
        public class C
        {
            [DataMember]
            public string PropertyA { get; set; }

            [DataMember]
            public int PropertyB { get; set; }

            public C()
            {
                Console.WriteLine("Call C constructor");
            }

            [OnSerializing]
            public void OnSerializing(StreamingContext context)
            {
                Thread.SetData(Thread.AllocateNamedDataSlot("C"), PropertyA);
                PropertyA = Convert.ToBase64String(Encoding.UTF8.GetBytes(PropertyA));
            }

            [OnSerialized]
            public void OnSerialized(StreamingContext context)
            {
                PropertyA = (string)Thread.GetData(Thread.GetNamedDataSlot("C"))!;
            }


            [OnDeserialized]
            public void OnDeserialized(StreamingContext context)
            {
                PropertyA = Encoding.UTF8.GetString(Convert.FromBase64String(PropertyA));
            }
        }


        [Test]
        public void DataContractSerializer()
        {
            var tester = new DataContractSerializerHelper<C>(
                new DataContractSerializer(typeof(C)));
            var c1 = new C() { PropertyA = "11111111" };
            var c2 = tester.SerializeAndDeserialize(c1)!;

            Console.WriteLine(c1.PropertyA);
            Console.WriteLine(c2.PropertyA);
        }
    }
}
