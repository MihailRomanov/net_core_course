using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing.Template;
using Microsoft.Extensions.Primitives;
using System.Reflection;

namespace RouterResearchApplication
{
    internal class CustomControllerRouter : IRouter
    {
        public VirtualPathData? GetVirtualPath(VirtualPathContext context)
            => throw new NotImplementedException();

        public async Task RouteAsync(RouteContext context)
        {
            var httpContext = context.HttpContext;
            if (TryGetAction(httpContext, out var controllerAndAction))
            {
                var (controller, action) = controllerAndAction;

                context.Handler = async (httpContext) =>
                {

                    var result = action.Invoke(controller, null) ?? "";

                    switch (result)
                    {
                        case IResult iResult:
                            await iResult.ExecuteAsync(httpContext);
                            break;
                        default:
                            await httpContext.Response.WriteAsync(result.ToString());
                            break;
                    }
                };
            }
        }

        private bool TryGetAction(
            HttpContext httpContext,
            out (ControllerBase, MethodInfo) controllerAndAction)
        {
            var template = TemplateParser.Parse("/{class}/{method}");
            var matcher = new TemplateMatcher(template,
                new RouteValueDictionary(new { @class = "Api", method = "Three" }));

            var headers = httpContext.Request.Headers;

            var values = new RouteValueDictionary();
            if (headers.TryGetValue("XXX", out StringValues value) &&
                matcher.TryMatch(new PathString(value), values))
            {
                var controllerName = values["class"]?.ToString() + "Controller";
                var methodName = values["method"]?.ToString() ?? "";

                var controllers = httpContext.RequestServices
                    .GetServices<ControllerBase>();

                var controller = controllers.First(
                    c => c.GetType().Name
                        .Equals(controllerName, StringComparison.OrdinalIgnoreCase));
                var method = controller.GetType().GetMethod(methodName)!;

                controllerAndAction = (controller, method);
                return true;
            }

            controllerAndAction = (null, null);
            return false;
        }
    }
}
