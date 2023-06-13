using Microsoft.Extensions.Hosting;

namespace ElegantWebApi.Infrastructure
{
    public interface IConcurrentDictionaryHostedService : IHostedService
    {
        public Task Append(string key, object value);
        public Task Create(string key, List<object> list);
        public Task<List<object>> Delete(string id);
        public Task<List<object>> Get(string key);
        
    }
}