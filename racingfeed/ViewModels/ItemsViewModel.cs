using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;

using Xamarin.Forms;

using racingfeed.Models;
using racingfeed.Views;

namespace racingfeed.ViewModels
{
    public class ItemsViewModel : BaseViewModel
    {
        public ObservableCollection<FeedItem> Items { get; set; }
        public Command LoadItemsCommand { get; set; }

        /// <summary>
        /// Display username to load tweets.
        /// </summary>
        readonly string username = "";

        public ItemsViewModel()
        {
            Title = "Browse";
            Items = new ObservableCollection<FeedItem>();
            LoadItemsCommand = new Command(async () => ExecuteLoadItemsCommand());

			MessagingCenter.Subscribe<NewItemPage, FeedItem>(this, "SomeCommand", (obj, item) =>
            {
                // do some work
            });
        }

        public ItemsViewModel(string username) : this()
        {
            this.username = username;
        }

        async Task ExecuteLoadItemsCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                Items.Clear();
                var items = await DataStore.LoadItemsAsync(username, true);
                foreach (var item in items)
                {
                    Items.Add(item);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}