namespace ImageProcessingService.Core.Models.Image
{
    public class ImageDetailsVm
    {
        public Guid Id { get; set; }
        public string Path { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
