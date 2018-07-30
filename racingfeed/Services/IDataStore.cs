using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace racingfeed.Services
{
    public interface IDataStore<T>
    {
        Task<T> GetItemAsync(string id);
        Task<IEnumerable<T>> LoadItemsAsync(bool forceRefresh = false);
    }
}
