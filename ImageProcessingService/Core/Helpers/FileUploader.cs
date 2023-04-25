using ImageProcessingService.Core.Constants;

namespace ImageProcessingService.Core.Helpers;

public class FileUploader
{
    public static async Task<string> Upload(IFormFile file, string webRootPath)
    {
        if (file == null) throw new ArgumentNullException();

        if (file.FileName == null || file.FileName.Length == 0) throw new ArgumentNullException();

        var path = Path.Combine(webRootPath, Constants.Constants.ImageFolderName, file.FileName);

        using (FileStream stream = new FileStream(path, FileMode.Create))
        {
            await file.CopyToAsync(stream);
            stream.Close();
        }

        return path;
    }
}
