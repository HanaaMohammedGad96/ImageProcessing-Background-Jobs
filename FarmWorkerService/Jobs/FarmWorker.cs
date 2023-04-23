using FarmWorkerService.Interfaces;
using FarmWorkerService.Services;

namespace FarmWorkerService.Jobs
{
    public class FarmWorker : BackgroundService
    {
        private readonly IFarmingProcess _process;
        private readonly ILogger<FarmWorker> _logger;

        public FarmWorker(ILogger<FarmWorker> logger, IFarmingProcess process)
        {
            _logger  = logger;
            _process = process;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("FarmWorker running at: {time}", DateTimeOffset.Now);
                await _process.TillTheLand(stoppingToken);
                await _process.PlantTheCrop(stoppingToken);
                await _process.HarvestTheCrop(stoppingToken);
            }
        }
    }
}