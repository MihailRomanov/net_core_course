using Microsoft.AspNetCore.Mvc;

namespace Sample11_EndpointMetadata
{
    [Route("api")]
    [ApiController]
    public class ApiController : ControllerBase
    {
        [ValidationOptionMetadata(ValidationOption.NotNullAndEmpty)]
        public IResult One([FromQuery]string p1 = "", string p2 = "") 
            => Results.Text($"{p1} - {p2}");
    }
}
