﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using URLShortener.Contracts.Data.Entities;

namespace URLShortener.Contracts.Data.Repositories
{
    public interface IURLRepository: IRepository<URL>
    {
        public Task<long> GetHitCountAsync();
        public Task<string> GetLongUrlByShortVersionAsync(string shortVersion);
        public Task<string> GetShortURLByLongVersionAsync(string longVersion);
    }
}
