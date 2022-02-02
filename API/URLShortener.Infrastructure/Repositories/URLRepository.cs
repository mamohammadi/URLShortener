using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using URLShortener.Contracts.Data.Entities;
using URLShortener.Contracts.Data.Repositories;

namespace URLShortener.Infrastructure.Repositories
{
    public class URLRepository : IURLRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public URLRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Add(URL item)
        {
            _dbContext.URLs.Add(item);
        }

        public Task CommitAsync()
        {
            return _dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<URL>> GetAllNoTrackingAsync()
        {
            return (await _dbContext.URLs.ToListAsync()).AsEnumerable();
        }

        public async Task<long> GetHitCountAsync(string shortUrl)
        {
            return await _dbContext.URLs
                .LongCountAsync(u => u.ShortURLVersion == shortUrl);
        }

        public Task<string> GetLongUrlByShortVersionAsync(string shortVersion)
        {
            return _dbContext.URLs
                .Where(u => u.ShortURLVersion == shortVersion)
                .Select(u => u.LongURLVersion)
                .FirstOrDefaultAsync();
        }

        public Task<string> GetShortURLByLongVersionAsync(string longVersion)
        {
            return _dbContext.URLs
                .Where(u => u.LongURLVersion == longVersion)
                .Select(u => u.ShortURLVersion)
                .FirstOrDefaultAsync();
        }
    }
}
