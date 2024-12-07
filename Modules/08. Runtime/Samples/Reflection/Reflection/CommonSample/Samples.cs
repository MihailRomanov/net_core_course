using MoreLinq.Extensions;


namespace Reflection.CommonSample
{
    internal class Samples
    {
        [Test]
        public void ShowResultSample()
        {
            var Principal = new ParticipantInfo
            {
                Ul = new UlInfo
                {
                    Head = "Иван Иванович",
                    Inn = "11121321212",
                    Kpp = "22222",
                    Name = "Роги и ноги"
                }
            };

            var principalTemplateVariables =
                EmailTemplateVariableHelper.ToTemplateVariables(
                    Principal,
                    "principal",
                    "_");

            principalTemplateVariables.ForEach(
                i => Console.WriteLine($"[{i.Key}] = {i.Value}"));
        }
    }
}
