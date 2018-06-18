using System;

using racingfeed.Models;

namespace racingfeed.ViewModels
{
    public class ItemDetailViewModel : BaseViewModel
    {
        public FeedItem Item { get; set; }
        public ItemDetailViewModel(FeedItem item = null)
        {
            Title = item?.Text;
            Item = item;
        }
    }
}
