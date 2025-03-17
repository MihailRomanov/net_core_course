namespace Sample11_EndpointMetadata
{
    public class NoEmptyFilter : IEndpointFilter
    {
        public async ValueTask<object?> InvokeAsync(
            EndpointFilterInvocationContext context, EndpointFilterDelegate next)
        {
            var endpoint = context.HttpContext.GetEndpoint()!;

            var validationOptions = endpoint.Metadata
                .OfType<IValidationOptionMetadata>().SingleOrDefault();

            if (validationOptions != null)
            {
                foreach (var arg in context.Arguments)
                {
                    switch (validationOptions.Option) 
                    {
                        case ValidationOption.NotNull:
                            if (arg == null) return Results.BadRequest();
                            break;
                        case ValidationOption.NotNullAndEmpty:
                            if (arg is string s && string.IsNullOrEmpty(s))
                                return Results.BadRequest();
                            break;
                    }
                }
            }

            return await next.Invoke(context);
        }
    }
}
