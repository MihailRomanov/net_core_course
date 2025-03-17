
namespace Sample10EndpointFilters
{
    public class NoEmptyFilter : IEndpointFilter
    {
        public async ValueTask<object?> InvokeAsync(
            EndpointFilterInvocationContext context, EndpointFilterDelegate next)
        {
            foreach (var arg in context.Arguments)
            {
                if (arg is string s && string.IsNullOrEmpty(s))
                    return Results.BadRequest();
            }
            return await next.Invoke(context);
        }
    }
}
