using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Collections.Concurrent;

namespace ElegantWebApi.Infrastructure
{
    public class ConcurrentDictionaryHostedService : BackgroundService
    {
        private ConcurrentDictionary<string, List<object>> _dictionary;
        private readonly IConfiguration _configuration;
        private readonly ILogger _logger;
        public ConcurrentDictionaryHostedService(ILogger<ConcurrentDictionaryHostedService> logger, IConfiguration configuration)
        {
            _dictionary = new ConcurrentDictionary<string, List<object>>();
            _logger = logger;
            _configuration = configuration;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            try
            {
                while (!stoppingToken.IsCancellationRequested)
                {

                    await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken);
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


        public Task Create(string key, List<object> list)
        {
            if (!_dictionary.TryAdd(key, list))
            {
                _dictionary[key] = list;
            };
            return Task.CompletedTask;
        }

        public Task Append(string key, object value)
        {
            if (_dictionary.TryGetValue(key, out List<object>? list))
            {
                list.Add(value);
            }
            else
            {

                Create(key, new List<object>() { value });
            }
            return Task.CompletedTask;
        }

        public Task<List<object>> Get(string key)
        {
            if (_dictionary.TryGetValue(key, out List<object>? list))
            {
                if (list.Count >= 0)
                {
                    return Task.FromResult(list);
                }
            }
            return Task.FromResult(new List<object>());

        }

        public Task<List<object>> Delete(string id)
        {
            if (_dictionary.TryRemove(id, out List<object>? list))
            {
                return Task.FromResult(list);
            }
            return Task.FromResult(new List<object>());
        }


    }
}
