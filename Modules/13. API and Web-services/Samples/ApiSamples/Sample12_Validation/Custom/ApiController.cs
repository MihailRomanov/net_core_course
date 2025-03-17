using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace Sample12_Validation.Custom
{
    [ApiController]
    [Route("api2")]
    public class ApiController : ControllerBase
    {
        [HttpPost("check")]
        public IResult Check(
            [FromBody] Person person,
            [FromServices] IValidator<Person> validator)
        {
            var validationResult = validator.Validate(person);

            return validationResult.IsValid 
                ? Results.Text($"{person.Name}")
                : Results.ValidationProblem(validationResult.ToDictionary());
        }
    }
}
