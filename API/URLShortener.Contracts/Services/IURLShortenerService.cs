namespace URLShortener.Contracts.Services
{
    public interface IURLShortenerService
    {
        string GetShortURLVersion(string url);
    }
}
