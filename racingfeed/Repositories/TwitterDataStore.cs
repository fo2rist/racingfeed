using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using racingfeed.Models;
using racingfeed.Services;
using Tweetinvi.Models;
using Tweetinvi.Models.Entities;
using Xamarin.Forms;

[assembly: Dependency(typeof(racingfeed.Repositories.TwitterDataStore))]
namespace racingfeed.Repositories
{
    public class TwitterDataStore : IDataStore<FeedItem>
    {
        public ITwitterService WebService => DependencyService.Get<ITwitterService>() ?? new MockTwitterService();

        List<FeedItem> itemsCache = new List<FeedItem>();

        public TwitterDataStore() { }

        public async Task<FeedItem> GetItemAsync(string id)
        {
            return await Task.FromResult(itemsCache.FirstOrDefault(it => it.Id == id));
        }

        public async Task<IEnumerable<FeedItem>> LoadItemsAsync(bool forceRefresh = false)
        {
            itemsCache.Clear();
            itemsCache.AddRange(
                from tweet in await WebService.LoadTimelineAsync("alo_oficial")
                select tweet.ToFeedItem()
            );
            
            return await Task.FromResult(itemsCache);
        }
    }

    static class Ext
    {
        private static string MEDIA_TYPE_PHOTO = "photo";

        public static FeedItem ToFeedItem(this ITweet tweet)
        {
            return new FeedItem
            {
                Id = tweet.Id.ToString(),
                Text = tweet.CreatedBy.Name,
                Description = tweet.Text,
                Urls = tweet.Urls.ToUrlsList(),
                ImageUrls = tweet.Media.ToUrlsList(),
                IsRetweet = tweet.IsRetweet,
                RetweetImageUrls = tweet.RetweetedTweet?.Media.ToUrlsList(),
            };
        }

        public static List<string> ToUrlsList(this List<IUrlEntity> urlsList)
        {
            return (from url in urlsList
                select url.ToString()
            ).ToList();
        }

        public static List<string> ToUrlsList(this List<IMediaEntity> mediaList)
        {
            return (from media in mediaList
                where media.MediaType == MEDIA_TYPE_PHOTO
                select media.MediaURL
            ).ToList();
        }
    }
}
