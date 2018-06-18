using System;
using System.Collections.Generic;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using racingfeed.Models;

namespace racingfeed.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NewItemPage : ContentPage
    {
        public FeedItem Item { get; set; }

        public NewItemPage()
        {
            InitializeComponent();

            Item = new FeedItem
            {
                Text = "Item name",
                Description = "This is an item description."
            };

            BindingContext = this;
        }

        async void Save_Clicked(object sender, EventArgs e)
        {
			MessagingCenter.Send(this, "SomeCommand", Item);
            await Navigation.PopModalAsync();
        }
    }
}