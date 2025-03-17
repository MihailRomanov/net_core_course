using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApp2
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private static int Counter = 0;

        [HttpGet]
        [ResponseCache(Duration = 5,
            Location = ResponseCacheLocation.Any)]
        public string Get() => Counter++.ToString();

        [HttpGet("list")]
        [ResponseCache(CacheProfileName = "Paging50")]
        public string GetList(int take = 100, int skip = 0)
        {
            return string.Join(", ", Enumerable.Range(skip, take));
        }
    }
}
