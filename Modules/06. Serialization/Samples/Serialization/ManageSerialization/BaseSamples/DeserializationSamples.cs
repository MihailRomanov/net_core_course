using FluentAssertions;
using ManageSerialization.Helpers;
using Newtonsoft.Json;
using ProtoBuf;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace ManageSerialization.BaseSamples
{
    public class DeserializationSamples
    {
        [ProtoContract(ImplicitFields = ImplicitFields.AllPublic)]
        public record A_PublicGetterSetter
        {
            public string? PropertyA { get; set; }
            public string? PropertyB { get; set; }
        }

        private static IEnumerable<TestCaseData> PublicGetterSetterTestCases()
        {
            var dataFunc = () => new A_PublicGetterSetter { PropertyA = "A", PropertyB = "B" };
            return GenerateTestCaseS(dataFunc, "Public Getter/Setter");
        }

        [ProtoContract(ImplicitFields = ImplicitFields.AllPublic)]
        public record A_PublicGetterPrivateSetter
        {
            public string? PropertyA { get; internal set; }
            public string? PropertyB { get; internal set; }
        }

        private static IEnumerable<TestCaseData> PublicGetterPrivateSetterTestCases()
        {
            var dataFunc = () => new A_PublicGetterPrivateSetter { PropertyA = "A", PropertyB = "B" };
            return GenerateTestCaseS(dataFunc, "Public Getter/Private Setter");
        }


        [ProtoContract(ImplicitFields = ImplicitFields.AllPublic)]
        public record A_PublicGetterAndInit
        {
            public string? PropertyA { get; init; }
            public string? PropertyB { get; init; }
        }

        private static IEnumerable<TestCaseData> PublicGetterAndInitTestCases()
        {
            var dataFunc = () => new A_PublicGetterAndInit { PropertyA = "A", PropertyB = "B" };
            return GenerateTestCaseS(dataFunc, "Public Getter and Init");
        }


        [ProtoContract(ImplicitFields = ImplicitFields.AllPublic)]
        public record A_PrimaryConstructorOnly(string? PropertyA, string? PropertyB)
        {
            public string PropertyC { get; set; } = "";
        };


        private static IEnumerable<TestCaseData> PrimaryConstructorTestCases()
        {
            var dataFunc = () => new A_PrimaryConstructorOnly("A", "B") { PropertyC = "CCCC" };
            return GenerateTestCaseS(dataFunc, "PrimaryConstructorOnly");
        }

        [ProtoContract(ImplicitFields = ImplicitFields.AllPublic)]
        public record A_PrimaryAndDefaultConstructor(
            string? PropertyA,
            string? PropertyB)
        {
            public string? PropertyC { get; set; }
            public A_PrimaryAndDefaultConstructor() : this("z", "z")
            {
                PropertyC = "z";
            }
        };


        private static IEnumerable<TestCaseData> PrimaryAndDefaultConstructorTestCases()
        {
            var dataFunc = () =>
                new A_PrimaryAndDefaultConstructor("AAA", "BBB") { PropertyC = "" };
            return GenerateTestCaseS(dataFunc, "PrimaryAndDefaultConstructor");
        }


        [TestCaseSource(nameof(PublicGetterSetterTestCases))]
        [TestCaseSource(nameof(PublicGetterPrivateSetterTestCases))]
        [TestCaseSource(nameof(PublicGetterAndInitTestCases))]
        [TestCaseSource(nameof(PrimaryConstructorTestCases))]
        [TestCaseSource(nameof(PrimaryAndDefaultConstructorTestCases))]

        public void SerializeDeserializeSampele<T>(Func<T> dataFunc, Func<BaseSerializationHelper<T>> helperFunc)
        {
            var data = dataFunc();
            var helper = helperFunc();
            var result = helper.SerializeAndDeserialize(data);

            result.Should().Be(data);
        }

        private static IEnumerable<TestCaseData> GenerateTestCaseS<T>(
            Func<T> dataFunc, string prefix)
        {
            Type type = typeof(T);

            yield return
                new TestCaseData(
                    dataFunc,
                    () => new XmlSerializerHelper<T>(new XmlSerializer(type)))
                { TypeArgs = [type] }
                .SetName($"{prefix} - XmlSerializer");
            yield return
                new TestCaseData(
                    dataFunc,
                    () => new JsonSerializerHelper<T>())
                { TypeArgs = [type] }
                .SetName($"{prefix} - JsonSerializer");
            yield return
                new TestCaseData(
                    dataFunc,
                    () => new NewtonsoftJsonSerializerHelper<T>(new JsonSerializer()))
                { TypeArgs = [type] }
                .SetName($"{prefix} - NewtonsoftJsonSerialize");
            yield return
                new TestCaseData(
                    dataFunc,
                    () => new DataContractSerializerHelper<T>(new DataContractSerializer(type)))
                { TypeArgs = [type] }
                .SetName($"{prefix} - DataContractSerializer");
            yield return
                new TestCaseData(
                    dataFunc,
                    () => new ProtobufSerializerHelper<T>())
                { TypeArgs = [type] }
                .SetName($"{prefix} - ProtobufSerializer");
        }

    }

}
