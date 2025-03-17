using Microsoft.AspNetCore.Session;

namespace WebApp1
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllersWithViews()
                .AddCookieTempDataProvider();
            builder.Services.AddRazorPages()
                .AddSessionStateTempDataProvider();

            builder.Services.AddDistributedMemoryCache();
            builder.Services.AddSession(op => 
            {
                op.IdleTimeout = TimeSpan.FromMinutes(100);
                op.IOTimeout = TimeSpan.FromMinutes(1);

                op.Cookie.Name = SessionDefaults.CookieName;
                op.Cookie.Path = SessionDefaults.CookiePath;
                op.Cookie.MaxAge = TimeSpan.FromMinutes(100);
            });
            
            var app = builder.Build();

            app.UseSession();
            
            app.Use((context, next) =>
            {
                var session = context.Session;
                if (!session.Keys.Contains(SessionVariables.SessionStart))
                {
                    session.SetString(
                        SessionVariables.SessionStart, 
                        DateTimeOffset.Now.ToString());
                }
                
                return next(context);
            });
            
            app.MapDefaultControllerRoute();
            app.MapRazorPages();

            app.Run();
        }
    }
}
