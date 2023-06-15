﻿namespace ElegantWebApi.Domain.Entities
{
    public sealed class DataListModel
    {
        public Guid Id { get; set; }
        public List<object>? Values { get; set; }
        public DateTime ExpirationTime { get; set; }

    }
}
