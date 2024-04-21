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
            //Windows.Storage.StorageFolder localFolder = Windows.Storage.ApplicationData.Current.LocalFolder;
            LoadApplication(new Xamarin.App());
        }
    }
}
