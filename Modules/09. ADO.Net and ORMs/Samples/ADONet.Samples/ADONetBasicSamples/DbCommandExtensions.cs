using System.Data.Common;

namespace ADONetBasicSamples
{
    internal static class DbCommandExtensions
    {
        public static void AddParamAndValue(this DbCommand command, string paramName, object value)
        {
            var param = command.CreateParameter();
            param.ParameterName = paramName;
            param.Value = value;
            command.Parameters.Add(param);
        }
    }
}