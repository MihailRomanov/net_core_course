using Newtonsoft.Json;

namespace Reflection.CommonSample;

public class ParticipantInfo
{
    [JsonProperty("ul")]
    public UlInfo Ul { get; set; }
    [JsonProperty("ip")]
    public IpInfo Ip { get; set; }
    [JsonProperty("fl")]
    public FlInfo Fl { get; set; }
    [JsonProperty("employee")]
    public EmployeeInfo Employee { get; set; }

}

public class FlInfo
{
    [JsonProperty("name")]
    public string Name { get; set; }
}

public class IpInfo
{
    [JsonProperty("name")]
    public string Name { get; set; }
}

public class UlInfo
{
    [JsonProperty("name")]
    public string Name { get; set; }
    [JsonProperty("inn")]
    public string Inn { get; set; }
    [JsonProperty("kpp")]
    public string Kpp { get; set; }
    [JsonProperty("head")]
    public string Head { get; set; }
}

public class EmployeeInfo
{
    [JsonProperty("name")]
    public string Name { get; set; }
    [JsonProperty("inn")]
    public string Inn { get; set; }
    [JsonProperty("kpp")]
    public string Kpp { get; set; }
    [JsonProperty("orgname")]
    public string OrgName { get; set; }
}