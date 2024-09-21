namespace ManageSerialization.Helpers
{ 
    public enum ShowResultFormat
    {
        None,
        String,
        HexArray
    }

    public abstract class BaseSerializationHelper<TData>
    {
        private readonly ShowResultFormat showResult;

        protected BaseSerializationHelper(ShowResultFormat showResult = ShowResultFormat.None)
        {
            this.showResult = showResult;
        }

        public TData? SerializeAndDeserialize(TData data)
        {
            var stream = new MemoryStream();

            Console.WriteLine("Start serialization");
            Serialization(data, stream);
            Console.WriteLine("Serialization finished");

            var serializedContent = stream.ToArray();

            ShowSerializedContent(serializedContent);

            stream = new MemoryStream(serializedContent);
            Console.WriteLine("Start deserialization");
            TData? result = Deserialization(stream);
            Console.WriteLine("Deserialization finished");

            return result;
        }

        public void ShowSerializedContent(byte[] serializedContent)
        {
            if (showResult != ShowResultFormat.None)
            {
                Console.WriteLine("-------------------");
                string formatedString;

                if (showResult == ShowResultFormat.String)
                {
                    formatedString = Console.OutputEncoding.GetString(serializedContent);
                }
                else
                {
                    formatedString = HexDump.HexDump.Format(serializedContent, columnWidth: 4, columnCount: 1);
                }
                Console.WriteLine(formatedString);
                Console.WriteLine("-------------------");

            }
        }

        public abstract TData? Deserialization(Stream stream);
        public abstract void Serialization(TData data, Stream stream);
    }

    public abstract class BaseSerializationHelper<TData, TSerializer> 
        : BaseSerializationHelper<TData>
    {
        protected readonly TSerializer serializer;

        protected BaseSerializationHelper(TSerializer serializer, ShowResultFormat showResult = ShowResultFormat.None)
            : base(showResult)
        {
            this.serializer = serializer;
        }
    }
}