using System.Reflection;
using MoreLinq.Extensions;
using Newtonsoft.Json;

namespace Reflection.CommonSample;

public static class EmailTemplateVariableHelper
{
    public static Dictionary<string, string> ToTemplateVariables(object value, string prefix, string delim)
    {
        if (value == null)
            return new Dictionary<string, string>();

        var type = value.GetType();
        if (type.IsValueType || type == typeof(string))
            return new Dictionary<string, string>
            {
                [prefix] = value.ToString() ?? "",
            };

        var properties = type.GetProperties(
            BindingFlags.Public | BindingFlags.Instance);
        var result = new Dictionary<string, string>();
        foreach (var property in properties)
        {
            var name = property
                .GetCustomAttribute<JsonPropertyAttribute>()?.PropertyName
                ?? property.Name;
            var propValue = property.GetValue(value) ?? "";

            var fullName = prefix + delim + name;
            var internalVariables = ToTemplateVariables(propValue, fullName, delim);
            internalVariables.ForEach(t => result.Add(t.Key, t.Value));
        }
        if (result.Count > 0)
            result.Add(prefix, "{}");

        return result;
    }
}