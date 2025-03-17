namespace Sample08_CustomBinding
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddControllers();
            var app = builder.Build();

            // /map/phones/+7(3412)43-56-89
            app.MapGet("/map/phones/{phone}",
                (
                    NormaLizedPhone phone
                ) => $"{phone.Number}");

            // /map/products?take=200&skip=300
            app.MapGet("/map/products",
                (
                    PagingParams paging
                ) => $"{paging}");

            app.MapControllers();

            app.Run();
        }
    }
}
