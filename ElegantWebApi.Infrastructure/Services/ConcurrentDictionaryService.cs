using System.Collections.Concurrent;
using ElegantWebApi.Infrastructure.Contracts;

namespace ElegantWebApi.Infrastructure.Services
{
    public sealed class ConcurrentDictionaryService : IConcurrentDictionaryService
    {
        private ConcurrentDictionary<string, List<object>> _dictionary;
       
        public ConcurrentDictionaryService()
        {
            _dictionary = new ConcurrentDictionary<string, List<object>>();
        }

        public Task CreateAsync(string key, List<object> list)
        {
            if (!_dictionary.TryAdd(key, list))
            {
                _dictionary[key] = list;
            };
            return Task.CompletedTask;
        }

        public Task AppendAsync(string key, object value)
        {
            if (_dictionary.TryGetValue(key, out List<object>? list))
            {
                list.Add(value);
            }
            else
            {
                CreateAsync(key, new List<object>() { value });
            }
            return Task.CompletedTask;
        }

        public Task<List<object>> GetAsync(string key)
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

        public Task<List<object>> DeleteAsync(string id)
        {
            if (_dictionary.TryRemove(id, out List<object>? list))
            {
                return Task.FromResult(list);
            }
            return Task.FromResult(new List<object>());
        }
    }
}
