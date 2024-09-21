using System.Runtime.Serialization;
using ManageSerialization.Helpers;

namespace ManageSerialization.CustomizationSamples
{
    public class Person
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public Credentials Credentials { get; set; }
    }

    public class Credentials
    {
        public string Login { get; set; }
        public string Password { get; set; }
    }

    public class PersonSurrogate : ISerializationSurrogateProvider
    {
        public class SecurityCredentials
        {
            public string SecurityString { get; set; }
        }

        public Type GetSurrogateType(Type type)
        {
            if (type == typeof(Credentials))
                return typeof(SecurityCredentials);
            return type;
        }

        public object GetObjectToSerialize(object obj, Type targetType)
        {
            Credentials credentials = obj as Credentials;
            if (credentials != null)
            {
                var result = new SecurityCredentials();
                result.SecurityString = credentials.Login + ":" + credentials.Password;
                return result;
            }
            return obj;
        }

        public object GetDeserializedObject(object obj, Type targetType)
        {
            SecurityCredentials scred = obj as SecurityCredentials;
            if (scred != null)
            {
                var creds = scred.SecurityString.Split(':');
                return new Credentials { Login = creds[0], Password = creds[1] };
            }
            return obj;
        }

    }

    public class DataContractSurrogateSamples
    {
        Person person = new Person
        {
            Name = "Peter Abel",
            Age = 32,
            Credentials = new Credentials
            {
                Login = "peterab",
                Password = "123"
            }
        };

        [Test]
        public void NoSurrogateSample()
        {
            var helper = new DataContractSerializerHelper<Person>(
                new DataContractSerializer(typeof(Person)));
            helper.SerializeAndDeserialize(person);
        }

        [Test]
        public void SurrogateSample()
        {
            var serializer = new DataContractSerializer(typeof(Person));
            serializer.SetSerializationSurrogateProvider(new PersonSurrogate());

            var helper = new DataContractSerializerHelper<Person>(serializer);

            var person2 = helper.SerializeAndDeserialize(person);
            Console.WriteLine($"{person2.Credentials.Login} : {person2.Credentials.Password}");
        }
    }
}
