namespace Sample08_CustomBinding
{
    public record PagingParams(int Take, int Skip)
    {
        public static async ValueTask<PagingParams?> BindAsync(HttpContext context)
        {
            int take = 100, skip = 0;
            
            int.TryParse(context.Request.Query["take"], out take);
            int.TryParse(context.Request.Query["skip"], out skip);

            return new PagingParams(take, skip);
        }
    }

}
