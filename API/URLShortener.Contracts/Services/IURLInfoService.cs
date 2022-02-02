using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using URLShortener.Contracts.DTO;

namespace URLShortener.Contracts.Services
{
    interface IURLInfoService
    {
        string GetLongURLVersion(string shortUrl);

        long GetURLHitCount(string shortUrl);

        IEnumerable<URLInfoDTO> GetAllURLs();
    }
}
