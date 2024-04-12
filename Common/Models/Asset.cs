namespace Common.Models
{
    public class Asset
    {
        public long Id { get; set; }

        public string? Text { get; set; }

        public byte[]? ImageBytes { get; set; }

        public Asset() {}

        public Asset(long id, string? text, byte[]? imageBytes)
        {
            Id = id;
            Text = text;
            ImageBytes = imageBytes;
        }
    }
}
