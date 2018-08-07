using System.Collections.Generic;
using System.Threading.Tasks;
using Tweetinvi;
using Tweetinvi.Models;
using Tweetinvi.Models.Entities;

namespace racingfeed.Services
{
    public interface ITwitterService
    {
        Task<IEnumerable<ITweet>> LoadTimelineAsync(string username);
    }
}
