namespace ImageProcessingService.ImageDb
{
    public class Image
    {
        public int Id { get; set; }
        public required string FileName { get; set; }
        public string? Content { get; set; }

        public DateTimeOffset AddedAt { get; set; }
    }
}
