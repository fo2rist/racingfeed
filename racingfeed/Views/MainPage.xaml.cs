using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace racingfeed.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : TabbedPage
    {
        public MainPage()
        {
            InitializeComponent();
            Children.Add(new NavigationPage(new ItemsPage("mclarenf1"))
            {
                Title = "McLaren"
            });
        }
    }
}