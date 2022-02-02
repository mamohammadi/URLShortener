using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using URLShortener.Contracts.Data.Repositories;
using URLShortener.Contracts.DTO;
using URLShortener.Contracts.Services;

namespace URLShortener.Core.Services
{
    public class URLInfoService : IURLInfoService
    {
        private IURLRepository _urlRepository;

        public URLInfoService(IURLRepository urlRepository)
        {
            _urlRepository = urlRepository;
        }

        public async Task<IEnumerable<URLInfoDTO>> GetAllURLsAsync()
        {
            try
            {
                var items = await _urlRepository.GetAllNoTrackingAsync();

                // Map (Note we can use auto mapper)
                var result = new List<URLInfoDTO>();
                if(items != null)
                {
                    foreach(var item in items)
                    {
                        result.Add(new URLInfoDTO()
                        {
                            ShortURL = item.ShortURLVersion,
                            LongURL = item.LongURLVersion,
                            HitCount = item.HitCount
                        }) ;
                    }
                }
                return result;
            }
            catch(Exception)
            {
                // log
                return null;
            }
        }

        public async Task<string> GetLongURLVersionAsync(string shortUrl)
        {
            try
            {
                var result = await _urlRepository.FindByShortVersionAsync(shortUrl);
                if (result != null)
                {
                    result.HitCount++;
                    await _urlRepository.CommitAsync();
                    return result.LongURLVersion;
                }
            }
            catch(Exception)
            {
                // Log
                
            }
            return null;
        }

        public Task<long> GetURLHitCountAsync(string baseAddress, string shortUrl)
        {
            try
            {
                shortUrl = shortUrl.Replace(baseAddress, string.Empty);
                return _urlRepository.GetHitCountAsync(shortUrl);
            }
            catch(Exception)
            {
                // Log
                return null;
            }
        }
    }
}
