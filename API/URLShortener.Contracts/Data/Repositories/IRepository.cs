using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace URLShortener.Contracts.Data.Repositories
{
    public interface IRepository<T>
    {
        void Add(T item);
        Task<IEnumerable<T>> GetAllNoTrackingAsync();
        Task CommitAsync();
    }
}
