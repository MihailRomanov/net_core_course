using Newtonsoft.Json;

namespace LoadSampleLib
{
    public static class JsonHelper
    {
        public static string GetJson(object o)
        {
            return JsonConvert.SerializeObject(o, Formatting.Indented);
        }
    }
}
