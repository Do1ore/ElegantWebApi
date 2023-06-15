using System.Collections.Concurrent;

namespace ElegantWebApi.Infrastructure.Contracts
{
    public interface IConcurrentDictionaryService
    {

        Task AppendAsync(string key, object value);
        Task CreateAsync(string key, List<object> list);
        Task<List<object>> DeleteAsync(string id);
        Task<List<object>> GetAsync(string key);
    }
}