using SixLabors.ImageSharp.Formats.Webp;

namespace ImageProcessingService.Core.Jobs;

public class ImageProcessingJob
{
    public static void ProcessImage(string imagePath, int width = Constants.Constants.DefaultWidth, int height = Constants.Constants.DefaultHeight)
    {
        using (var image = Image.Load(imagePath))
        {
            // Resize the image while preserving the aspect ratio
            image.Mutate(x => x.Resize(new ResizeOptions
            {
                Size = new Size(width, height),
                Mode = ResizeMode.Max,
                Position = AnchorPositionMode.Center,
                Compand = true
            }));

            //Convert the image to Webp format and save it to disk
            using (var outPutStream = new MemoryStream())
            {
                var webpOutputPath = Path.ChangeExtension(imagePath, "webp");

                using (var webpStream = new FileStream(webpOutputPath, FileMode.Create))
                {
                    image.SaveAsWebp(webpStream, new WebpEncoder()
                    {
                        Method = WebpEncodingMethod.BestQuality
                    });
                }
            }
        }
    }
}
