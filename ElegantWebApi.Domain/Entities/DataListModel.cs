using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElegantWebApi.Domain.Entities
{
    public class DataListModel
    {
        public Guid Id { get; set; }
        public List<object>? Values { get; set; }
        public DateTime ExpirationTime { get; set; }

    }
}
