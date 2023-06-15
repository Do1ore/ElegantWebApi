using ElegantWebApi.Infrastructure.Contracts;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace ElegantWebApi.Infrastructure.Services
{
    public sealed class ConcurrentDictionaryHostedService : BackgroundService
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger _logger;
        private readonly int _checkIntervalInSeconds;
        private readonly IConcurrentDictionaryService _dictionaryService;
        private readonly IExprirationDataService _exprirationDataService;
        public ConcurrentDictionaryHostedService(
            ILogger<ConcurrentDictionaryHostedService> logger,
            IConfiguration configuration,
            IExprirationDataService exprirationDataService,
            IConcurrentDictionaryService dictionaryService)
        {
            _logger = logger;
            _configuration = configuration;
            _checkIntervalInSeconds = Convert.ToInt32(_configuration
                .GetSection("DefaultCheckIntervalTime")["DefaultCheckIntervalTimeInSeconds"]);
            _exprirationDataService = exprirationDataService;
            _dictionaryService = dictionaryService;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            try
            {
                _logger.LogInformation("ConcurrentDictionaryHostedService started!");

                while (!stoppingToken.IsCancellationRequested)
                {
                    foreach (var keyDateTiemePair in await _exprirationDataService.GetAllAsync())
                    {
                        if (keyDateTiemePair.Value <= DateTime.Now)
                        {
                            await _dictionaryService.Delete(keyDateTiemePair.Key);
                            _logger.LogInformation($"Record with id: {keyDateTiemePair.Key} erased. Expired");
                        }
                    }
                    await Task.Delay(TimeSpan.FromSeconds(_checkIntervalInSeconds), stoppingToken);

                }
            }
            catch (TaskCanceledException)
            {
                // When the stopping token is canceled, for example, a call made from services.msc,
                // we shouldn't exit with a non-zero exit code. In other words, this is expected...
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Message}", ex.Message);

                // Terminates this process and returns an exit code to the operating system.
                // This is required to avoid the 'BackgroundServiceExceptionBehavior', which
                // performs one of two scenarios:
                // 1. When set to "Ignore": will do nothing at all, errors cause zombie services.
                // 2. When set to "StopHost": will cleanly stop the host, and log errors.
                //
                // In order for the Windows Service Management system to leverage configured
                // recovery options, we need to terminate the process with a non-zero exit code.
                Environment.Exit(1);
            }
        }

    }
}
