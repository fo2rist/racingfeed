﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using racingfeed.Models;
using Tweetinvi;
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
			items = Timeline.GetUserTimeline("alo_oficial")
					.Select(it => new FeedItem
					{
						Id = it.Id.ToString(),
						Text = it.CreatedBy.Name,
						Description = it.Text,
				        Urls = it.Urls
				                 .Select(url => url.ToString())
				                 .ToList(),
				        ImageUrls = it.Media
				                  .Where(media => media.MediaType == "photo")
				                  .Select(photo => photo.MediaURL)
				                  .ToList(),
                    }).ToList();

            return await Task.FromResult(items);
        }
    }
}
