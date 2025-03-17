using Microsoft.AspNetCore.Mvc;

namespace Sample08_CustomBinding
{
    [Route("api")]
    public class ApiController : ControllerBase
    {
        [HttpGet("phones/{phone}")]
        public IResult Phone(NormaLizedPhone phone) 
            => Results.Text(phone.Number.ToString());

        [HttpGet("products")]
        public IResult Producst(PagingParams pagingParams) 
            => Results.Text(pagingParams.ToString());
    }
}
