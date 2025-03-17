using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing.Template;
using Microsoft.Extensions.Primitives;
using System.Diagnostics.CodeAnalysis;

namespace Sample07_RequestDelegateFactory
{
    internal class CustomControllerRouter2 : IRouter
    {
        public VirtualPathData? GetVirtualPath(VirtualPathContext context)
            => throw new NotImplementedException();

        public async Task RouteAsync(RouteContext context)
        {
            var httpContext = context.HttpContext;
            if (TryGetAction(httpContext, out var requestDelegate))
            {
                context.Handler = requestDelegate;
            }
        }

        private bool TryGetAction(
            HttpContext httpContext,
            [NotNullWhen(true)]
            out RequestDelegate? requestDelegate)
        {
            var template = TemplateParser.Parse("/{class}/{method}");
            var matcher = new TemplateMatcher(template,
                new RouteValueDictionary(new { @class = "Api2", method = "Three" }));

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

                var creationResult = RequestDelegateFactory.Create(
                    method,
                    context => {
                        return context.RequestServices
                        .GetRequiredService(method.DeclaringType);
                        },
                    new RequestDelegateFactoryOptions { }
                    );

                requestDelegate = creationResult.RequestDelegate;
                return true;
            }

            requestDelegate = null;
            return false;
        }
    }
}
