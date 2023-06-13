﻿using Microsoft.Extensions.Hosting;
using System.Collections.Concurrent;

namespace ElegantWebApi.Infrastructure
{
    public class ConcurrentDictionaryHostedService : IConcurrentDictionaryHostedService
    {
        private ConcurrentDictionary<string, List<object>> _dictionary;

        public ConcurrentDictionaryHostedService()
        {
            _dictionary = new ConcurrentDictionary<string, List<object>>();
        }


        public Task StartAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
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
