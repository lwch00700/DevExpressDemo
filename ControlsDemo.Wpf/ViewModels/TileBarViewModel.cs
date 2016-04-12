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
    public class TileBarViewModel : TileNavBaseViewModel {
        ITileBarService TileBarService { get { return ServiceContainer.GetService<ITileBarService>(); } }
        protected override void OnNavigationMessage(NavigationMessage message) {
            base.OnNavigationMessage(message);
            TileBarService.CloseFlyout();
        }
    }
}
