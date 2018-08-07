using System.Collections.Generic;
using System.Threading.Tasks;
using Tweetinvi;
using Tweetinvi.Models;

[assembly: Xamarin.Forms.Dependency(typeof(racingfeed.Services.TwitterService))]
namespace racingfeed.Services
{
	public class TwitterService : ITwitterService
    {
        public TwitterService()
        {
			var config = Config.Config.Read();
            Auth.SetUserCredentials(config.TwitterConsumerKey,
                                    config.TwitterConsumerSecret,
                                    config.TwitterUserAccessToken,
                                    config.TwitterUserAccessSercret);
        }

		public async Task<IEnumerable<ITweet>> LoadTimelineAsync(string username)
		{
			return await TimelineAsync.GetUserTimeline(username);
        }
    }
}
