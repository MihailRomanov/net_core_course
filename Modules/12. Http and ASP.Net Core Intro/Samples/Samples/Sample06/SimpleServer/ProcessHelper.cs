namespace SimpleServer
{
    public static class ProcessHelper
    {
        public static async Task ProcessLogin(this HttpContext context)
        {
            var request = context.Request;

            if (request.HasFormContentType)
            {
                var login = request.Form["login"].ToString();
                var password = request.Form["password"].ToString();
                var rememberMe = request.Form["remember_me"];

                if (!(login.Length > 3 && password.StartsWith("123")))
                {
                    context.Response.StatusCode = StatusCodes.Status403Forbidden;
                    await context.Response.WriteAsync($"User {login} with password {password} not correct");
                }
                else
                {
                    if (rememberMe == "true")
                    {
                        SetAuthCookie(context, login, password);
                    }
                    await context.Response.WriteAsync($"User {login} with password {password} are login");
                }
            }
            else
            {
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
            }
        }

        private static void SetAuthCookie(HttpContext context, string login, string password)
        {
            var request = context.Request;

            var options = new CookieOptions()
            {
                Expires = DateTimeOffset.Now.AddDays(15),
                Secure = false,
            };

            context.Response.Cookies.Append("auth", $"{login}|||{password}", options);
        }

    }
}
