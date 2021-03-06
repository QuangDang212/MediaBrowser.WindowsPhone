﻿using System.Threading;
using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Messaging;
using MediaBrowser.Model;
using MediaBrowser.Model.ApiClient;
using MediaBrowser.WindowsPhone.Services;
using MediaBrowser.WindowsPhone.ViewModel;
using MediaBrowser.WindowsPhone.ViewModel.Remote;

namespace MediaBrowser.WindowsPhone.Views.Remote
{
    /// <summary>
    /// Description for RemoteView.
    /// </summary>
    public partial class RemoteView
    {
        /// <summary>
        /// Initializes a new instance of the RemoteView class.
        /// </summary>
        public RemoteView()
        {
            InitializeComponent(); 
        }

        protected override void InitialiseOnBack()
        {
            ((RemoteViewModel) DataContext).IsPinned = TileService.Current.TileExists(Constants.Pages.Remote.RemoteView);

            Messenger.Default.Send(new NotificationMessage(Constants.Messages.ReconnectToWebSocketMsg));
        }
    }
}