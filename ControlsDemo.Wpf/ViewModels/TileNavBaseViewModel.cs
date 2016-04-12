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
    public class TileNavViewLocator : ViewLocator {
        public TileNavViewLocator()
            : base(typeof(TileNavViewLocator).Assembly) {
        }
    }

    public class TileNavBaseViewModel : ViewModelBase {
        public TileNavBaseViewModel() {
            Messenger.Default.Register<NavigationMessage>(this, OnNavigationMessage);
            _Categories = new ObservableCollection<TileNavBaseItemViewModel>(TileNavBaseViewModelDataProvider.CreateCategories());
            _Actions = new ObservableCollection<TileNavBaseItemViewModel>(TileNavBaseViewModelDataProvider.CreateActions());
            BackCommand = DelegateCommandFactory.Create(OnBackCommand, CanBackCommand);
        }
        void OnBackCommand() {
            if(NavigationService.CanGoBack) {
                NavigationService.GoBack();
                if(Selected != null)
                    Selected.IsSelected = false;
                Selected = null;
            }
        }
        bool CanBackCommand() {
            return NavigationService.CanGoBack;
        }
        protected virtual void OnNavigationMessage(NavigationMessage message) {
            switch(message.MessageType) {
                case NavigationMessageType.New:
                    TileNavBaseItemViewModel vm = message.Source as TileNavBaseItemViewModel;
                    if(vm != null && vm.IsSelected) {
                        NavigationService.Navigate("TileNavDetailsView", vm);
                        if(Selected != null) Selected.IsSelected = false;
                        Selected = vm;
                    }
                    break;
                case NavigationMessageType.Back:
                    OnBackCommand();
                    break;
            }
        }
        INavigationService NavigationService { get { return ServiceContainer.GetService<INavigationService>(); } }
        TileNavBaseItemViewModel Selected;
        private System.Windows.Input.ICommand _BackCommand;
        private ObservableCollection<TileNavBaseItemViewModel> _Categories;
        private ObservableCollection<TileNavBaseItemViewModel> _Actions;
        public ObservableCollection<TileNavBaseItemViewModel> Categories {
            get { return _Categories; }
        }
        public ObservableCollection<TileNavBaseItemViewModel> Actions {
            get { return _Actions; }
        }
        public System.Windows.Input.ICommand BackCommand {
            get { return _BackCommand; }
            set { _BackCommand = value; }
        }
        ICommand onViewUnloadedCommand;
        public ICommand OnViewUnloadedCommand {
            get {
                if(onViewUnloadedCommand == null)
                    onViewUnloadedCommand = new DelegateCommand(OnViewUnloaded);
                return onViewUnloadedCommand;
            }
        }
        protected virtual void OnViewUnloaded() {
            Messenger.Default.Unregister<NavigationMessage>(this, OnNavigationMessage);
        }
    }
    public class TileNavBaseItemViewModel : BindableBase {
        public TileNavBaseItemViewModel() {
            Items = new ObservableCollection<TileNavBaseItemViewModel>();
            BackCommand = DelegateCommandFactory.Create(OnBackCommand);
            ShowNotificationCommand = DelegateCommandFactory.Create<string>(OnShowNotificationCommand);
        }
        void OnBackCommand() {
            Messenger.Default.Send(new NavigationMessage(NavigationMessageType.Back));
        }
        void OnShowNotificationCommand(string message) {
            Messenger.Default.Send(new NotificationMessage(DisplayName + " clicked"));
        }
        void OnIsSelectedChanged() {
            if(IsSelected)
                Messenger.Default.Send(new NavigationMessage(this));
        }
        public ObservableCollection<TileNavBaseItemViewModel> Items { get; private set; }
        public string DisplayName {
            get { return GetProperty(() => DisplayName); }
            set { SetProperty(() => DisplayName, value); }
        }
        public bool IsSelected {
            get { return GetProperty(() => IsSelected); }
            set { SetProperty(() => IsSelected, value, OnIsSelectedChanged); }
        }
        public Color Color {
            get { return GetProperty(() => Color); }
            set { SetProperty(() => Color, value); }
        }
        public ImageSource Image {
            get { return GetProperty(() => Image); }
            set { SetProperty(() => Image, value); }
        }
        public ImageSource ContentImage {
            get { return GetProperty(() => ContentImage); }
            set { SetProperty(() => ContentImage, value); }
        }
        public ICommand BackCommand {
            get { return GetProperty(() => BackCommand); }
            set { SetProperty(() => BackCommand, value); }
        }
        public ICommand ShowNotificationCommand {
            get { return GetProperty(() => ShowNotificationCommand); }
            set { SetProperty(() => ShowNotificationCommand, value); }
        }
        public TileSize Size {
            get { return GetProperty(() => Size); }
            set { SetProperty(() => Size, value); }
        }
    }
    static class TileNavBaseViewModelDataProvider {
        static string[] ContentImages = new string[] { "/ControlsDemo;component/Images/Calc.png", "/ControlsDemo;component/Images/Rates.png", "/ControlsDemo;component/Images/Research.png",
            "/ControlsDemo;component/Images/Statistics.png","/ControlsDemo;component/Images/System.png","/ControlsDemo;component/Images/UserManagment.png","/ControlsDemo;component/Images/ZillowLogo.png",};
        static string[] ItemImages = new string[] {
            "/ControlsDemo;component/Images/TileNavPane/itemElement01.Glyph.png"
            ,"/ControlsDemo;component/Images/TileNavPane/itemElement02.Glyph.png"
            ,"/ControlsDemo;component/Images/TileNavPane/itemElement03.Glyph.png"
            ,"/ControlsDemo;component/Images/TileNavPane/itemElement04.Glyph.png"
            ,"/ControlsDemo;component/Images/TileNavPane/itemElement05.Glyph.png"
            ,"/ControlsDemo;component/Images/TileNavPane/itemElement06.Glyph.png"
            ,"/ControlsDemo;component/Images/TileNavPane/itemElement07.Glyph.png"
            ,"/ControlsDemo;component/Images/TileNavPane/itemElement08.Glyph.png"
            ,"/ControlsDemo;component/Images/TileNavPane/itemElement09.Glyph.png"
            ,"/ControlsDemo;component/Images/TileNavPane/itemElement10.Glyph.png"
            ,"/ControlsDemo;component/Images/TileNavPane/itemElement11.Glyph.png"
            ,"/ControlsDemo;component/Images/TileNavPane/itemElement12.Glyph.png"
            ,"/ControlsDemo;component/Images/TileNavPane/itemElement13.Glyph.png"
            ,"/ControlsDemo;component/Images/TileNavPane/itemElement14.Glyph.png"
            ,"/ControlsDemo;component/Images/TileNavPane/itemElement15.Glyph.png"
            ,"/ControlsDemo;component/Images/TileNavPane/itemElement16.Glyph.png"
            ,"/ControlsDemo;component/Images/TileNavPane/itemElement17.Glyph.png"
            ,"/ControlsDemo;component/Images/TileNavPane/itemElement18.Glyph.png"
            ,"/ControlsDemo;component/Images/TileNavPane/itemElement19.Glyph.png"
            ,"/ControlsDemo;component/Images/TileNavPane/itemElement20.Glyph.png"
            ,"/ControlsDemo;component/Images/TileNavPane/itemElement21.Glyph.png"
            ,"/ControlsDemo;component/Images/TileNavPane/itemElement22.Glyph.png"
            ,"/ControlsDemo;component/Images/TileNavPane/itemElement23.Glyph.png"
        };
        static string[] SubItemImages = new string[] {
            "/ControlsDemo;component/Images/TileNavPane/subItemElement01.Glyph.png"
            ,"/ControlsDemo;component/Images/TileNavPane/subItemElement02.Glyph.png"
            ,"/ControlsDemo;component/Images/TileNavPane/subItemElement03.Glyph.png"
            ,"/ControlsDemo;component/Images/TileNavPane/subItemElement04.Glyph.png"
            ,"/ControlsDemo;component/Images/TileNavPane/subItemElement05.Glyph.png"
            ,"/ControlsDemo;component/Images/TileNavPane/subItemElement06.Glyph.png"
            ,"/ControlsDemo;component/Images/TileNavPane/subItemElement07.Glyph.png"
            ,"/ControlsDemo;component/Images/TileNavPane/subItemElement08.Glyph.png"
            ,"/ControlsDemo;component/Images/TileNavPane/subItemElement09.Glyph.png"
            ,"/ControlsDemo;component/Images/TileNavPane/subItemElement10.Glyph.png"
            ,"/ControlsDemo;component/Images/TileNavPane/subItemElement11.Glyph.png"
            ,"/ControlsDemo;component/Images/TileNavPane/subItemElement12.Glyph.png"
            ,"/ControlsDemo;component/Images/TileNavPane/subItemElement13.Glyph.png"
            ,"/ControlsDemo;component/Images/TileNavPane/subItemElement14.Glyph.png"
            ,"/ControlsDemo;component/Images/TileNavPane/subItemElement15.Glyph.png"
            ,"/ControlsDemo;component/Images/TileNavPane/subItemElement16.Glyph.png"
            ,"/ControlsDemo;component/Images/TileNavPane/subItemElement17.Glyph.png"
        };
        static Color[] Colors = new Color[] { (Color)ColorConverter.ConvertFromString("#FFC3213F"), (Color)ColorConverter.ConvertFromString("#FFE65E20"),
            (Color)ColorConverter.ConvertFromString("#FFD4AF00"), (Color)ColorConverter.ConvertFromString("#FF6652A2"), (Color)ColorConverter.ConvertFromString("#FF54AF0E"),
            (Color)ColorConverter.ConvertFromString("#FF00ABDC"), (Color)ColorConverter.ConvertFromString("#FFDA8515") };
        static Random random = new Random(DateTime.Now.Millisecond);
        static TileNavBaseItemViewModel CreateItem(string displayName, string[] Images) {
            TileNavBaseItemViewModel vm = new TileNavBaseItemViewModel()
            {
                DisplayName = displayName,
                Color = Colors[random.Next(Colors.Length)],
                Image = new BitmapImage(new Uri(Images[random.Next(Images.Length)], UriKind.RelativeOrAbsolute)),
                ContentImage = new BitmapImage(new Uri(ContentImages[random.Next(ContentImages.Length)], UriKind.RelativeOrAbsolute))
            };
            return vm;
        }
        public static IEnumerable<TileNavBaseItemViewModel> CreateCategories() {
            var categories = new List<TileNavBaseItemViewModel>();
            for(int i = 1; i < 6; i++) {
                TileNavBaseItemViewModel category = CreateItem(String.Format("Category {0}", i), ItemImages);
                for(int j = 1; j < 5; j++) {
                    TileNavBaseItemViewModel item = CreateItem(String.Format("Item {0}.{1}", i, j), ItemImages);
                    for(int k = 1; k < 4; k++) {
                        TileNavBaseItemViewModel subItem = CreateItem(String.Format("SubItem {0}.{1}.{2}", i, j, k), SubItemImages);
                        item.Items.Add(subItem);
                    }
                    category.Items.Add(item);
                }
                categories.Add(category);
            }
            return categories;
        }
        public static IEnumerable<TileNavBaseItemViewModel> CreateActions() {
            var actions = new List<TileNavBaseItemViewModel>();
            for(int i = 1; i < 5; i++) {
                TileNavBaseItemViewModel action = CreateItem(String.Format("Action {0}", i), ItemImages);
                if(i < 3) action.Size = TileSize.Small;
                actions.Add(action);
            }
            return actions;
        }
    }
    public enum NavigationMessageType { New, Back }
    public sealed class NavigationMessage {
        public NavigationMessage(object source) {
            Source = source;
        }
        public NavigationMessage(NavigationMessageType messageType, object source = null)
            : this(source) {
                MessageType = messageType;
        }
        public object Source { get; private set; }
        public NavigationMessageType MessageType { get; private set; }
    }
    public sealed class NotificationMessage {
        public NotificationMessage(string source) {
            Source = source;
        }
        public string Source { get; private set; }
    }
}
