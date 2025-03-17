using FluentValidation;
using Sample12_Validation.Custom;

namespace Sample12_Validation
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddControllers();
            builder.Services.AddTransient<IValidator<Person>, PersonValidator>();
            var app = builder.Build();

            app.MapControllers();

            app.Run();
        }
    }
}
