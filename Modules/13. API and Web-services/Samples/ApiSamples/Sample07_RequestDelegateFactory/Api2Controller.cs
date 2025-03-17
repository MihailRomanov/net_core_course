using Microsoft.AspNetCore.Mvc;

namespace Sample07_RequestDelegateFactory
{
    public class Api2Controller : ControllerBase
    {
        public IResult One([FromHeader] int? p) => Results.Text("1" + p);

        public IResult Two([FromHeader] int? p) => Results.Text("2" + p);

        public IResult Three([FromHeader] int? p) => Results.Text("3" + p);
    }
}
