using ElegantWebApi.Infrastructure.Contracts;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace ElegantWebApi.Infrastructure.Services
{
    /// <summary>
    /// Background service to work with <see cref="IExpirationDataService"/> and  <see cref="IConcurrentDictionaryService"/>;
    /// </summary>
    public sealed class DictionaryHostedMasterService : BackgroundService
    {
        private readonly ILogger _logger;
        private readonly IConcurrentDictionaryService _dictionaryService;
        private readonly IExpirationDataService _expirationDataService;
        private int _expiredCounter = 0;
        private readonly int _checkIntervalInSeconds;

        public DictionaryHostedMasterService(
            ILogger<DictionaryHostedMasterService> logger,
            IConfiguration configuration,
            IExpirationDataService expirationDataService,
            IConcurrentDictionaryService dictionaryService)
        {
            _logger = logger;
            _checkIntervalInSeconds = Convert.ToInt32(configuration
                .GetSection("DefaultCheckIntervalTime")["DefaultCheckIntervalTimeInSeconds"]);
            _expirationDataService = expirationDataService;
            _dictionaryService = dictionaryService;
        }

        /// <summary>
        /// Checks if the data has expired, if it has expired then deletes
        /// </summary>
        /// <remarks>
        /// When the stopping token is canceled, for example, a call made from services.msc,
        /// we shouldn't exit with a non-zero exit code. In other words, this is expected...</remarks>
        /// <param name="stoppingToken"></param>
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            try
            {
                _logger.LogInformation("ConcurrentDictionaryHostedService started!");

                while (!stoppingToken.IsCancellationRequested)
                {
                    var expirationInfo = await _expirationDataService.GetAllAsync();
                    if (expirationInfo.Count <= 0)
                    {
                        await Task.Delay(TimeSpan.FromSeconds(_checkIntervalInSeconds), stoppingToken);
                    }

                    foreach (var dateTimePair in expirationInfo)
                    {
                        if (dateTimePair.Value <= DateTime.Now)
                        {
                            await DoWorkToEraseAsync(dateTimePair);
                        }
                    }

                    await LogInfoAboutExpiration(stoppingToken);
                    await Task.Delay(TimeSpan.FromSeconds(_checkIntervalInSeconds), stoppingToken);
                    _expiredCounter = 0;
                }
            }
            catch (TaskCanceledException)
            {
                
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

        /// <summary>
        /// Erases data from dictionary
        /// </summary>
        /// <param name="DateTimePair"></param>
        private async Task DoWorkToEraseAsync(KeyValuePair<string, DateTime> DateTimePair)
        {
            var result = await _dictionaryService.DeleteAsync(DateTimePair.Key);
            if (result.Count > 0)
            {
                _expiredCounter++;
                await _expirationDataService.RemoveAsync(DateTimePair.Key);
                _logger.LogInformation("Record with id: {Key} erased. ",
                    DateTimePair.Key.ToString());
            }
            else
            {
                _logger.LogError("Error while deleting: {Key}", DateTimePair.Key.ToString());
            }
        }

        /// <summary>
        /// Logs information about expiration 
        /// </summary>
        /// <param name="stoppingToken"></param>
        private async Task LogInfoAboutExpiration(CancellationToken stoppingToken)
        {
            _logger.LogInformation("{ShortTimeString}: Expired: {ExpiredCounter}", DateTime.Now.ToShortTimeString(),
                Convert.ToString(_expiredCounter));
            List<string> info = new();
            foreach (var item in await _expirationDataService.GetAllAsync())
            {
                info.Add($"{item.Key} \nExpires: {item.Value}");
            }

            _logger.LogInformation("Expiration info: \n{Join}", string.Join(", \n", info));
        }
    }
}