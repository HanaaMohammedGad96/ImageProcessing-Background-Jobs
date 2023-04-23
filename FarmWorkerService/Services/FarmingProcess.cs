using FarmWorkerService.Config;
using FarmWorkerService.Interfaces;
using Microsoft.Extensions.Options;

namespace FarmWorkerService.Services;

public class FarmingProcess : IFarmingProcess
{
    private readonly FarmConfig config;
    private readonly ILogger<FarmingProcess> logger;

    public FarmingProcess(IOptions<FarmConfig> config, ILogger<FarmingProcess> logger)
    {
        this.config = config.Value;
        this.logger = logger;
    }

    public async Task TillTheLand(CancellationToken ct)
    {
        logger.LogInformation($"Tilling {config.FertileLandInMetersSquared} m^2 for {config.Crop} crop!!!");
        await Task.Delay(TimeSpan.FromMinutes(config.TillTimeInMinutes),ct);
    }

    public async Task PlantTheCrop(CancellationToken ct)
    {
        logger.LogInformation($"Planting {config.FertileLandInMetersSquared} m^2 for {config.Crop} crop!!!");
        await Task.Delay(TimeSpan.FromMinutes(config.PlantTimeInMinutes),ct);
    }

    public async Task HarvestTheCrop(CancellationToken ct)
    {
        logger.LogInformation($"Harvesting {config.FertileLandInMetersSquared} m^2 for {config.Crop} crop!!!");
        await Task.Delay(TimeSpan.FromMinutes(config.HarvestTimeInMinutes),ct);
    }
}
