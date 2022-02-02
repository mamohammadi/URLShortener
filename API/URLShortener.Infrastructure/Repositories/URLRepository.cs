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
            var item = await _dbContext.URLs
                .SingleOrDefaultAsync(u => u.ShortURLVersion == shortUrl);
            if (item != null)
                return item.HitCount;
            return 0;
        }

        public Task<URL> FindByShortVersionAsync(string shortVersion)
        {
            return _dbContext.URLs
                .SingleOrDefaultAsync(u => u.ShortURLVersion == shortVersion);
        }

        public Task<string> GetShortURLByLongVersionAsync(string longVersion)
        {
            return _dbContext.URLs
                .Where(u => u.LongURLVersion == longVersion)
                .Select(u => u.ShortURLVersion)
                .FirstOrDefaultAsync();
        }

        public Task<bool> ShortURLExists(string shortUrl)
        {
            return _dbContext.URLs
                .AnyAsync(u => u.ShortURLVersion == shortUrl);
        }
    }
}
