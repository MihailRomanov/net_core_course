using Microsoft.AspNetCore.Mvc;

namespace Sample10EndpointFilters
{
    [Route("api")]
    public class ApiController : ControllerBase
    {
        public IResult One(string p1 = "", string p2 = "") 
            => Results.Text($"{p1} - {p2}");
    }
}
