using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using racingfeed.Models;
using Tweetinvi;
using Tweetinvi.Models;
using Tweetinvi.Models.Entities;

[assembly: Xamarin.Forms.Dependency(typeof(racingfeed.Services.TwitterDataStore))]
namespace racingfeed.Services
{
    public class TwitterDataStore : IDataStore<FeedItem>
    {
        List<FeedItem> items;

        public TwitterDataStore()
        {
            items = new List<FeedItem>();
        }

        public async Task<FeedItem> GetItemAsync(string id)
        {
            return await Task.FromResult(items.FirstOrDefault(it => it.Id == id));
        }

        public async Task<IEnumerable<FeedItem>> LoadItemsAsync(bool forceRefresh = false)
        {
            var config = Config.Config.Read();
            Auth.SetUserCredentials(config.TwitterConsumerKey,
                                    config.TwitterConsumerSecret,
                                    config.TwitterUserAccessToken,
                                    config.TwitterUserAccessSercret);
            
            items = (from tweet in Timeline.GetUserTimeline("alo_oficial")
                     select tweet.ToFeedItem()
            ).ToList();
            
            return await Task.FromResult(items);
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
