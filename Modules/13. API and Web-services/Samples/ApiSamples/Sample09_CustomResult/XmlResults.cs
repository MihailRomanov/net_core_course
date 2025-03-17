namespace Sample09_CustomResult
{
    public static class XmlResults
    {
        public static IResult Ok(object? value = null)
            => new XmlOkHttpResult<object>(value);

        public static IResult Ok<TValue>(TValue? value)
            => new XmlOkHttpResult<TValue>(value);

    }
}
