using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;

namespace WebApp3
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private static int Counter = 0;

        [HttpGet]
        [OutputCache(Duration = 5)]
        public string Get() => Counter++.ToString();

        [HttpGet("{id}")]
        [OutputCache(Duration = 5, VaryByRouteValueNames = ["id"])]
        public string Get(int id) => (id + Counter++).ToString();

        [HttpGet("list")]
        [OutputCache(PolicyName = "Paging50")]
        public string GetList(int take = 100, int skip = 0)
        {
            return
                string.Join(", ", Enumerable.Range(skip, take))
                + "\n" +
                DateTime.Now.ToLongTimeString();
        }
    }
}
