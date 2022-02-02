using System;
using System.Threading.Tasks;

namespace URLShortener.Contracts.Services
{
    public interface IURLShortenerService
    {
        Task<string> GetShortURLVersionAsync(string url);
        bool IsValidUrl(string url);
        Uri GetUri(string url);
    }
}
