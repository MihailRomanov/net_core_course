using Microsoft.AspNetCore.Mvc;

namespace Sample12_Validation.DataAnnotation
{
    [ApiController]
    [Route("api")]
    public class ApiController : ControllerBase
    {
        [HttpPost("check")]
        public IResult Check([FromBody]Person person) 
            => Results.Text($"{person.Name}");
    }
}
