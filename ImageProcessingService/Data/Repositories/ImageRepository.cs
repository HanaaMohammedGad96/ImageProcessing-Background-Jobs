using Hangfire;
using ImageProcessingService.Core.Entities;
using ImageProcessingService.Core.Helpers;
using ImageProcessingService.Core.Jobs;
using ImageProcessingService.Core.Models.Image;
using ImageProcessingService.Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ImageProcessingService.Data.Repositories;
public class ImageRepository : IImageRepository
{
    private readonly ApplicationDbContext _context;
    private readonly IWebHostEnvironment _environment;
    private readonly IBackgroundJob _deleteNonWebpImagesJob;
    public ImageRepository(ApplicationDbContext context, IWebHostEnvironment environment, IBackgroundJob deleteNonWebpImagesJob)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _environment = environment ?? throw new ArgumentNullException(nameof(environment));
        _deleteNonWebpImagesJob = deleteNonWebpImagesJob ?? throw new ArgumentNullException(nameof(deleteNonWebpImagesJob));
    }

    public async Task UploadImage(ImageDto model, CancellationToken ct)
    {
        string wwwPath = _environment.WebRootPath;

        if (string.IsNullOrEmpty(wwwPath)) throw new ArgumentNullException("An error occured");

        string imagePath = await FileUploader.Upload(model.Image, wwwPath);

        Thumbnail image = new Thumbnail
        {
            Path = Path.ChangeExtension(imagePath, "webp"),
            Width = model.Width,
            Height = model.Height,
            CreatedAt = DateTime.UtcNow,
        };

        await _context.Thumbnails.AddAsync(image);
        await _context.SaveChangesAsync(ct);

        // Queue a background job to resize and convert the image
        BackgroundJob.Enqueue(() => ImageProcessingJob.ProcessImage(imagePath, model.Width, model.Height));
    }

    public Task RemoveNonWebpImages()
    {
        RecurringJob.AddOrUpdate(() => _deleteNonWebpImagesJob.RunAsync(),
                    Cron.Daily(10, 0)); // Run at 10:00 AM every day

        return Task.CompletedTask;
    }

    public async Task<List<ImageVm>> GetAllImages(CancellationToken ct)
    => await _context.Thumbnails.OrderByDescending(c => c.CreatedAt)
                                .Select(c => new ImageVm { Id = c.Id, Path = c.Path})
                                .ToListAsync(ct);

    public async Task<ImageDetailsVm> GetById(Guid id, CancellationToken ct)
    => await _context.Thumbnails.Select(c => new ImageDetailsVm
    {
        Id        = c.Id, 
        Path      = c.Path, 
        Width     = c.Width, 
        Height    = c.Height, 
        CreatedAt = c.CreatedAt 
    }).FirstOrDefaultAsync(c => c.Id == id, ct);
}
