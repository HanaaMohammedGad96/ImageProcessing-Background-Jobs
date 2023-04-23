using FarmWorkerService.Config;
using FarmWorkerService.Interfaces;
using FarmWorkerService.Jobs;
using FarmWorkerService.Services;

IHost host = Host.CreateDefaultBuilder(args)
                .ConfigureLogging(logging =>
                {
                    logging.ClearProviders();
                    logging.AddConsole();
                    logging.AddEventLog();
                })
                // Essential to run this as a window service
                .UseWindowsService()
                .ConfigureServices((hostContext, services) =>
                {
                    IConfiguration configuration = hostContext.Configuration;
                    services.AddSingleton<FarmConfig>(new FarmConfig
                    {
                        TillTimeInMinutes          = configuration.GetSection("FarmConfig").Get<FarmConfig>().TillTimeInMinutes,
                        PlantTimeInMinutes         = configuration.GetSection("FarmConfig").Get<FarmConfig>().PlantTimeInMinutes,
                        HarvestTimeInMinutes       = configuration.GetSection("FarmConfig").Get<FarmConfig>().HarvestTimeInMinutes,
                        FertileLandInMetersSquared = configuration.GetSection("FarmConfig").Get<FarmConfig>().FertileLandInMetersSquared,
                        Crop                       = configuration.GetSection("FarmConfig").Get<FarmConfig>().Crop,
                    });
                   // services.Configure<FarmConfig>(builder.Configuration.GetSection("FarmConfig").Get<FarmConfig>());
                    services.AddLogging();
                    services.AddSingleton<IFarmingProcess, FarmingProcess>();
                    services.AddHostedService<FarmWorker>();
                })
                .Build();

await host.RunAsync();
