using ImageProcessingService.Core.Models.Image;

namespace ImageProcessingService.Data.Interfaces;

public interface IImageRepository
{
    Task<List<ImageVm>> GetAllImages(CancellationToken ct);
    Task<ImageDetailsVm> GetById(Guid id, CancellationToken ct);
    Task UploadImage(ImageDto model, CancellationToken ct);
    Task RemoveNonWebpImages();
}
