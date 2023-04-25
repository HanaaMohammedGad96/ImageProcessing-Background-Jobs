namespace ImageProcessingService.Core.Jobs;

public interface IBackgroundJob
{
    Task RunAsync();
}
