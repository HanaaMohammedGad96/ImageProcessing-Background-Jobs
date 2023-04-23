namespace FarmWorkerService.Interfaces;

public interface IFarmingProcess
{
    Task TillTheLand(CancellationToken stoppingToken);
    Task PlantTheCrop(CancellationToken stoppingToken);
    Task HarvestTheCrop(CancellationToken stoppingToken);
}
