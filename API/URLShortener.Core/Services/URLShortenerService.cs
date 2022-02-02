using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using URLShortener.Contracts;
using URLShortener.Contracts.Algorithms;
using URLShortener.Contracts.Data.Entities;
using URLShortener.Contracts.Data.Repositories;
using URLShortener.Contracts.Services;

namespace URLShortener.Core.Services
{
    public class URLShortenerService : IURLShortenerService
    {
        private IURLRepository _urlRepository;
        private IShortenAlgorithm _shortenAlgorithm;

        private Task<string> GetShortVersionFromDatabase(string longUrl)
        {
            try
            {
                return _urlRepository.GetShortURLByLongVersionAsync(longUrl);
            }
            catch (Exception)
            {
                /* Logging */
                return Task.FromResult(string.Empty);
            }
        }

        private string ApplyShortenAlgorithm(string url)
        {
            try
            {
                return $"{_shortenAlgorithm.Apply(url, DateTime.UtcNow.Ticks.ToString())}";
            }
            catch (Exception)
            {
                /* Log */
                return null;
            }
        }

        private async Task<bool> SaveToDatabase(string url, string shortVersion)
        {
            _urlRepository.Add(new URL()
            {
                LongURLVersion = url,
                ShortURLVersion = shortVersion,
                HitCount = 0
            });

            try
            {
                await _urlRepository.CommitAsync();
            }
            catch (Exception)
            {
                // Log
                return false;
            }
            return true;
        }

        private async Task<string> GenerateUniqueShortUrl(string url)
        {
            try
            {
                for (; ; )
                {
                    var result = ApplyShortenAlgorithm(url);
                    if (string.IsNullOrWhiteSpace(result))
                        return null;

                    if (!await _urlRepository.ShortURLExists(result))
                        return result;
                }
            }
            catch (Exception)
            {
                // Log
                return null;
            }
        }

        public URLShortenerService(IURLRepository urlRepository, IShortenAlgorithm shortenAlgorithm)
        {
            _urlRepository = urlRepository;
            _shortenAlgorithm = shortenAlgorithm;
        }

        public async Task<string> GetShortURLVersionAsync(string url)
        {
            // First check if exists in database
            string shortVersion = await GetShortVersionFromDatabase(url);
            if (shortVersion == null)
            {
                // Build new one
                shortVersion = await GenerateUniqueShortUrl(url);
                if (!string.IsNullOrWhiteSpace(shortVersion))
                {
                    // Persist to DB
                    if (!await SaveToDatabase(url, shortVersion))
                        shortVersion = null;
                }
            }

            return shortVersion;
        }

        public bool IsValidUrl(string url)
        {
            if (string.IsNullOrWhiteSpace(url))
                return false;

            Uri result = GetUri(url);
            return result != null
                && (result.Scheme == Uri.UriSchemeHttp || result.Scheme == Uri.UriSchemeHttps);
        }

        public Uri GetUri(string url)
        {
            Uri result;
            return Uri.TryCreate(url, UriKind.Absolute, out result) ? result : null;
        }
    }
}
