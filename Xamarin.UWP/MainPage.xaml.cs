using System;
using Windows.UI.ViewManagement;

namespace Xamarin.UWP
{
    public sealed partial class MainPage
    {
        public MainPage()
        {
            this.InitializeComponent();
            var win = ApplicationView.GetForCurrentView();
            win.Title = "Главное окно программы";
            LoadApplication(new Xamarin.App());
        }
    }
}
