using System;
using System.Collections.Generic;
using System.Linq;
using Tweetinvi.Models.Entities;

namespace racingfeed.Models
{
    public class FeedItem
    {
        public string Id { get; set; }
        public string Text { get; set; }
        public string Description { get; set; }
        /// <summary>
        /// List of URLs assiciated with feed item.
        /// </summary>
		public List<String> Urls { get; set; }
        /// <summary>
        /// List of Image URLs attached to the feed item.
        /// </summary>
		public List<string> ImageUrls { get; set; }
        /// <summary>
        /// The first image if present or null.
        /// </summary>
		public string PrimaryImageUrl => ImageUrls.FirstOrDefault();
        public bool HasImage => PrimaryImageUrl != null;
	}
}