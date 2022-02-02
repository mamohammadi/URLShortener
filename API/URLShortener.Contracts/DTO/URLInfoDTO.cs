using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace URLShortener.Contracts.DTO
{
    public class URLInfoDTO
    {
        public string ShortURL { get; set; }
        public string LongURL { get; set; }
        public long HitCount { get; set; }
    }
}
