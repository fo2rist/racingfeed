using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using racingfeed.Models;

namespace racingfeed.Services
{
    public class MockDataStore : IDataStore<FeedItem>
    {
        List<FeedItem> items;

        public MockDataStore()
        {
            items = new List<FeedItem>();
            var mockItems = new List<FeedItem>
            {
                new FeedItem { Id = Guid.NewGuid().ToString(), Text = "First item", Description="This is an item description." },
                new FeedItem { Id = Guid.NewGuid().ToString(), Text = "Second item", Description="This is an item description." },
                new FeedItem { Id = Guid.NewGuid().ToString(), Text = "Third item", Description="This is an item description." },
                new FeedItem { Id = Guid.NewGuid().ToString(), Text = "Fourth item", Description="This is an item description." },
                new FeedItem { Id = Guid.NewGuid().ToString(), Text = "Fifth item", Description="This is an item description." },
                new FeedItem { Id = Guid.NewGuid().ToString(), Text = "Sixth item", Description="This is an item description." },
            };

            foreach (var item in mockItems)
            {
                items.Add(item);
            }
        }

        public async Task<FeedItem> GetItemAsync(string id)
        {
            return await Task.FromResult(items.FirstOrDefault(s => s.Id == id));
        }

        public async Task<IEnumerable<FeedItem>> LoadItemsAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(items);
        }
    }
}