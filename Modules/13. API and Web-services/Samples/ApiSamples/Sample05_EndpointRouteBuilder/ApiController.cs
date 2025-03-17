using Microsoft.AspNetCore.Mvc;

namespace Sample05_EndpointRouteBuilder
{
    public class ApiController : ControllerBase
    {
        public IResult One() => Results.Text("1");

        public IResult Two() => Results.Text("2");

        public IResult Three() => Results.Text("3");
    }
}
