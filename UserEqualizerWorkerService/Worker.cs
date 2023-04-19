using UserEqualizerWorkerService.Services.v1;

namespace UserEqualizerWorkerService
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> logger;
        private readonly UserEqualizerService userService;

        public Worker(ILogger<Worker> logger, UserEqualizerService userService)
        {
            this.logger = logger;
            this.userService = userService;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            logger.LogInformation("Starting service...");

            while (!stoppingToken.IsCancellationRequested)
            {
                logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);

                var result = await userService.ExecuteService();

                string resultLogMessage = result ? "Successfully processed" : "Processed with failure";

                logger.LogInformation(resultLogMessage);

                logger.LogInformation("Stoping service...");

                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}