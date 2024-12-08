using System;

namespace PackageSample
{
    public static class JsonHelper
    {
        public static string ToJson(object obj)
        {
            if (obj is string)
                return $"\"{obj}\"";

            throw new NotSupportedException("Type is not supported!!!");
        }
    }
}
