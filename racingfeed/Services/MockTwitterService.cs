using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tweetinvi.Models;

namespace racingfeed.Services
{
    public class MockTwitterService : ITwitterService
    {
        public Task<IEnumerable<ITweet>> LoadTimelineAsync(string username)
        {
            throw new NotImplementedException();
        }
    }
}
