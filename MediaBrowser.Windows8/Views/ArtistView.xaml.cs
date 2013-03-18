﻿using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Messaging;
using MediaBrowser.Windows8.ViewModel;
using Windows.UI.Xaml.Controls;

// The Basic Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234237

namespace MediaBrowser.Windows8.Views
{
    /// <summary>
    /// A basic page that provides characteristics common to most applications.
    /// </summary>
    public sealed partial class ArtistView : MediaBrowser.Windows8.Common.LayoutAwarePage
    {
        public ArtistView()
        {
            this.InitializeComponent();
            Loaded += (sender, args) => Messenger.Default.Send(new NotificationMessage(Constants.ArtistViewLoadedMsg));
        }

        private void ListViewBase_OnItemClick(object sender, ItemClickEventArgs e)
        {
            SimpleIoc.Default.GetInstance<MainViewModel>().ItemClicked.Execute(e);
        }
    }
}