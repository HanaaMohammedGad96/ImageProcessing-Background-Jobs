namespace ImageProcessingService.Core.Models.Image;

public class ImageDto
{
    public IFormFile Image { get; set; }
    public int Width { get; set; }
    public int Height { get; set; }
}
