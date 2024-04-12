namespace ImagesApi.Models
{
    public class Asset
    {
        public long Id { get; set; }

        public string? Text { get; set; }

        public byte[]? ImageBytes { get; set; }
    }
}
