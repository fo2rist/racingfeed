using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace racingfeed.Repositories
{
    public interface IDataStore<T>
    {
        Task<IEnumerable<T>> LoadItemsAsync(string username, bool forceRefresh = false);
    }
}
