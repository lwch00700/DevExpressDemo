using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shell;
using DevExpress.Mvvm;
using DevExpress.Mvvm.POCO;
using DevExpress.Utils;
using DevExpress.Xpf.Core.Native;

namespace ControlsDemo {
    public class TaskbarServicesViewModel : IDisposable {
        public TaskbarServicesViewModel() {
            OverlayIcons = new NamedIcon[] {
                new NamedIcon () {
                    Caption = "Moon",
                    Icon = ImageSourceHelper.GetImageSource(AssemblyHelper.GetResourceUri(typeof(TaskbarServices).Assembly, "Images/Moon.png")) },
                new NamedIcon () {
                    Caption = "Sun",
                    Icon = ImageSourceHelper.GetImageSource(AssemblyHelper.GetResourceUri(typeof(TaskbarServices).Assembly, "Images/Sun.png")) }
            };
            ProgressStatesNames = Enum.GetNames(typeof(TaskbarItemProgressState));
            ButtonProperties = new ObservableCollection<bool> { true, true, true, false, true };
            ButtonProperties.CollectionChanged += ButtonPropertyChanged;
        }

        public void Dispose() {
            TaskbarButtonService.Description = null;
            TaskbarButtonService.OverlayIcon = null;
            TaskbarButtonService.ProgressState = TaskbarItemProgressState.None;
            ButtonProperties.CollectionChanged -= ButtonPropertyChanged;
            ButtonProperties = null;
            TaskbarButtonService.ThumbButtonInfos.Clear();
            TaskbarButtonService.ThumbnailClipMarginCallback = null;
            TaskbarButtonService.ThumbnailClipMargin = new Thickness();
            ApplicationJumpListService.Items.Clear();
            ApplicationJumpListService.Apply();
        }
        public IEnumerable<NamedIcon> OverlayIcons { get; set; }
        public virtual IEnumerable<string> ProgressStatesNames { get; protected set; }
        public virtual ObservableCollection<bool> ButtonProperties { get; protected set; }

        public virtual double ThumbnailClipMarginMultipliyer { get; set; }
        public virtual bool ThumbButtonsCreate { get; set; }

        public virtual Thickness ThumbnailClipMargin { get; protected set; }

        [Required]
        protected virtual ITaskbarButtonService TaskbarButtonService { get { return null; } }
        [Required]
        protected virtual IApplicationJumpListService ApplicationJumpListService { get { return null; } }
        [Required]
        protected virtual IDialogService DialogService { get { return null; } }
        [Required]
        protected virtual IMessageBoxService MessageBoxService { get { return null; } }

        protected virtual void OnThumbnailClipMarginMultipliyerChanged() {
            TaskbarButtonService.UpdateThumbnailClipMargin();
        }
        protected virtual void OnThumbButtonsCreateChanged() {
            if(ThumbButtonsCreate) {
                TaskbarButtonService.ThumbButtonInfos.Add(new TaskbarThumbButtonInfo {
                    Description = "Zoom out",
                    ImageSource = ImageSourceHelper.GetImageSource(AssemblyHelper.GetResourceUri(typeof(TaskbarServices).Assembly, "/Images/TaskbarScreenshots/ZoomOut_32x32.png")),
                    Action = () => DecreaseThumbnailClipMarginMultipliyer()
                });
                TaskbarButtonService.ThumbButtonInfos.Add(new TaskbarThumbButtonInfo {
                    Description = "Zoom in",
                    ImageSource = ImageSourceHelper.GetImageSource(AssemblyHelper.GetResourceUri(typeof(TaskbarServices).Assembly, "/Images/TaskbarScreenshots/ZoomIn_32x32.png")),
                    Action = () => IncreaseThumbnailClipMarginMultipliyer()
                });
                SetButtonsProperties();
            } else {
                TaskbarButtonService.ThumbButtonInfos.Clear();
            }
        }
        void ButtonPropertyChanged(object sender, NotifyCollectionChangedEventArgs e) {
            SetButtonsProperties();
        }
        void SetButtonsProperties() {
            foreach(TaskbarThumbButtonInfo item in TaskbarButtonService.ThumbButtonInfos) {
                item.IsEnabled = ButtonProperties[0];
                item.IsInteractive = ButtonProperties[1];
                item.IsBackgroundVisible = ButtonProperties[2];
                item.DismissWhenClicked = ButtonProperties[3];
                item.Visibility = (ButtonProperties[4]) ? Visibility.Visible : Visibility.Collapsed;
            }
        }
        bool DecreaseThumbnailClipMarginMultipliyer() {
            ThumbnailClipMarginMultipliyer = (ThumbnailClipMarginMultipliyer >= 10) ? (ThumbnailClipMarginMultipliyer - 10) : 0;
            TaskbarButtonService.UpdateThumbnailClipMargin();
            return true;
        }
        bool IncreaseThumbnailClipMarginMultipliyer() {
            ThumbnailClipMarginMultipliyer = (ThumbnailClipMarginMultipliyer <= 90) ? (ThumbnailClipMarginMultipliyer + 10) : 100;
            TaskbarButtonService.UpdateThumbnailClipMargin();
            return true;
        }
        Thickness GetThumbnailClipMargin(Size size) {
            return ThumbnailClipMargin = new Thickness {
                Left = 3.0 * (double)ThumbnailClipMarginMultipliyer * size.Width / (5.0 * 100.0),
                Top = 2.0 * (double)ThumbnailClipMarginMultipliyer * size.Height / (5.0 * 100.0),
                Right = 2.0 * (double)ThumbnailClipMarginMultipliyer * size.Width / (5.0 * 100.0),
                Bottom = 3.0 * (double)ThumbnailClipMarginMultipliyer * size.Height / (5.0 * 100.0)
            };
        }
        void AddTask(NewJumpTaskWindowViewModel task) {
            string customCategory = string.IsNullOrEmpty(task.CustomCategory) ? null : task.CustomCategory;
            ApplicationJumpListService.Items.AddOrReplace(customCategory, task.Title, task.Icon.Icon, task.Description,
                () => MessageBoxService.Show(task.MessageText));
            IEnumerable<RejectedApplicationJumpItem> rejectedItems = ApplicationJumpListService.Apply();
            foreach(var rejectedItem in rejectedItems) {
                var rejectedTask = (ApplicationJumpTaskInfo)rejectedItem.JumpItem;
                MessageBoxService.Show(string.Format("Error: {0}", rejectedItem.Reason), rejectedTask.Title, MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        public void OnLoaded() {
            ThumbButtonsCreate = true;
            ThumbnailClipMarginMultipliyer = 20;
            TaskbarButtonService.ThumbnailClipMarginCallback = GetThumbnailClipMargin;
        }
        public void OpenTaskWindow() {
            NewJumpTaskWindowViewModel taskAddition = ViewModelSource.Create(() => new NewJumpTaskWindowViewModel());
            IDataErrorInfo errorInfo = (IDataErrorInfo)taskAddition;
            UICommand okCommand = new UICommand() {
                Caption = "OK",
                IsCancel = false,
                IsDefault = true,
                Command = new DelegateCommand<CancelEventArgs>(x => { }, x => string.IsNullOrEmpty(errorInfo["Title"]))
            };
            UICommand cancelCommand = new UICommand() {
                Caption = "Cancel",
                IsCancel = true,
                IsDefault = false,
            };
            if(DialogService.ShowDialog(new List<UICommand>() { okCommand, cancelCommand }, "Add Jump List Task", "NewJumpTaskWindow", taskAddition) == okCommand)
                AddTask(taskAddition);
        }
    }
    public class NamedIcon {
        public string Caption { get; set; }
        public ImageSource Icon { get; set; }
    };
}
