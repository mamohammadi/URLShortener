using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace URLShortener.Contracts.Data.Entities
{
    public class URL
    {
        public Guid Id { get; set; }
        public string ShortURLVersion { get; set; }
        public string LongURLVersion { get; set; }
        public long HitCount { get; set; }
    }
}
