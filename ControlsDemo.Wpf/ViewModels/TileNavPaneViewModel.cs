using DevExpress.Mvvm;
using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Mvvm.Native;
using DevExpress.Mvvm.POCO;
using DevExpress.Mvvm.UI;
using DevExpress.Utils;
using DevExpress.Xpf.Core.Native;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Reflection;
using System.Windows;
using System.Windows.Input;
using System.Windows.Controls;
using DevExpress.Xpf.Navigation.Internal;

namespace ControlsDemo {
    public class TileNavPaneViewModel : TileNavBaseViewModel {
        IMessageBoxService MessageBoxService { get { return ServiceContainer.GetService<IMessageBoxService>(); } }
        public TileNavPaneViewModel() {
            Messenger.Default.Register<NotificationMessage>(this, OnNotificationMessage);
            ShowNotificationCommand = DelegateCommandFactory.Create<string>(OnShowNotificationCommand);
        }
        private System.Windows.Input.ICommand _ShowNotificationCommand;
        public System.Windows.Input.ICommand ShowNotificationCommand {
            get { return _ShowNotificationCommand; }
            set { _ShowNotificationCommand = value; }
        }
        void OnShowNotificationCommand(string message) {
            MessageBoxService.ShowMessage(message);
        }
        protected virtual void OnNotificationMessage(NotificationMessage message) {
            OnShowNotificationCommand(message.Source);
        }
        protected override void OnViewUnloaded() {
            base.OnViewUnloaded();
            Messenger.Default.Unregister<NotificationMessage>(this, OnNotificationMessage);
        }
    }
}
