using System.Xml.Serialization;

namespace Sample09_CustomResult
{
    public class XmlOkHttpResult<TValue> : IResult, IValueHttpResult
    {
        public TValue? Value { get; }

        object? IValueHttpResult.Value => Value;

        internal XmlOkHttpResult(TValue? value)
        {
            Value = value;
        }

        public async Task ExecuteAsync(HttpContext httpContext)
        {
            var serializer = new XmlSerializer(typeof(TValue));
            httpContext.Response.StatusCode = StatusCodes.Status200OK;

            var writer = new StringWriter();
            serializer.Serialize(writer, Value);

            await httpContext.Response.WriteAsync(writer.ToString());
        }
    }
}
