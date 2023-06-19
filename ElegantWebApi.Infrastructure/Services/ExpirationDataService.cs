using ElegantWebApi.Infrastructure.Contracts;
using Microsoft.Extensions.Logging;
using System.Collections.Concurrent;

namespace ElegantWebApi.Infrastructure.Services
{
    /// <summary>
    /// Service to work with expiration data from <see cref="ConcurrentDictionaryService"/>
    /// </summary>
    public sealed class ExpirationDataService : IExpirationDataService
    {
        private readonly ConcurrentDictionary<string, DateTime> _expirationValuesTimeDictionary;
        private readonly ILogger<IExpirationDataService> _logger;

        public ExpirationDataService(ILogger<ExpirationDataService> logger)
        {
            _expirationValuesTimeDictionary = new ConcurrentDictionary<string, DateTime>();
            _logger = logger;
        }

        public Task AddExpirationTimeAsync(string key, DateTime expirationTime)
        {
            if (!_expirationValuesTimeDictionary.TryAdd(key, expirationTime))
            {
                _expirationValuesTimeDictionary[key] = expirationTime;
            }

            _logger.LogInformation(
                "Added expiration time for record with id: {Key}. New expiration time: {ExpirationTime}", key,
                expirationTime.ToLongTimeString());

            return Task.CompletedTask;
        }

        public Task<List<KeyValuePair<string, DateTime>>> GetAllAsync()
        {
            if (_expirationValuesTimeDictionary.Count <= 0)
            {
                return Task.FromResult(new List<KeyValuePair<string, DateTime>>());
            }

            return Task.FromResult(_expirationValuesTimeDictionary.ToList());
        }

        public Task RemoveAsync(string id)
        {
            if (_expirationValuesTimeDictionary.TryRemove(id, out _))
            {
                return Task.CompletedTask;
            }
            return Task.FromException(new InvalidOperationException());
        }

        public Task<DateTime> GetExpirationTimeAsync(string key)
        {
            if (_expirationValuesTimeDictionary.TryGetValue(key, out DateTime expirationTime))
            {
                return Task.FromResult(expirationTime);
            }

            return Task.FromResult(DateTime.MinValue);
        }

        public Task UpdateExpirationTimeAsync(string key, DateTime expirationTime)
        {
            _expirationValuesTimeDictionary.AddOrUpdate(key, expirationTime, (k, _) => expirationTime);
            _logger.LogInformation(
                "Added expiration time for record with id: {Key}. New expiration time: {ExpirationTime}", key,
                expirationTime.ToLongTimeString());

            return Task.CompletedTask;
        }
    }
}