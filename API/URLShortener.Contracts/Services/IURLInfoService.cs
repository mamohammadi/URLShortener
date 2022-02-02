using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using URLShortener.Contracts.DTO;

namespace URLShortener.Contracts.Services
{
    public interface IURLInfoService
    {
        Task<string> GetLongURLVersionAsync(string shortUrl);

        Task<long> GetURLHitCountAsync(string shortUrl);

        Task<IEnumerable<URLInfoDTO>> GetAllURLsAsync();
    }
}
