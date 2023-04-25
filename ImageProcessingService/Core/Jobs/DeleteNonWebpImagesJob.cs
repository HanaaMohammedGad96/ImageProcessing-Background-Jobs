namespace ImageProcessingService.Core.Jobs;

public class DeleteNonWebpImagesJob : IBackgroundJob
{
    private readonly IWebHostEnvironment _environment;

    public DeleteNonWebpImagesJob(IWebHostEnvironment environment)
    => _environment = environment;

    public Task RunAsync()
    {
        string directory = _environment.WebRootPath + "\\Images";

        DirectoryInfo di = new DirectoryInfo(directory);

        foreach (var file in di.GetFiles().Where(f => !f.Name.EndsWith(".webp")))
        {
            file.Delete();
        }

        return Task.CompletedTask;
    }
}
