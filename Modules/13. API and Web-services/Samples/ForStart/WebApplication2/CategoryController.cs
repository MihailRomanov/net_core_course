using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NorthwindDb;
using NorthwindDb.Models;

namespace WebApplication2
{
    [ApiController]
    [Produces("application/json")]
    [Route("/categories")]
    public class CategoryController(NorthwindContext northwindContext) 
        : ControllerBase
    {
        [HttpGet()]
        public async Task<IEnumerable<Category>> GetAll()
        {
            return await northwindContext.Categories.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Category>> Get(int id)
        {
            var category = await northwindContext.Categories.FindAsync(id);
            return category != null ? Ok(category) : NotFound();
        }

        [HttpGet("{id}/image")]
        public async Task<ActionResult<byte[]>> GetImage(int id)
        {
            var category = await northwindContext.Categories.FindAsync(id);
            return category != null
                ? File(category.Picture ?? [], "images/png", $"category_{id}.png")
                : NotFound();
        }

        [HttpPut]
        public async Task<ActionResult> Update([FromBody] Category category)
        {
            northwindContext.Categories.Update(category);
            await northwindContext.SaveChangesAsync();

            return Ok();
        }
    }
}
