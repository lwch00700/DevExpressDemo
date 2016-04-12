using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.ComponentModel;
using DevExpress.XtraScheduler;
using DevExpress.Xpf.Scheduler.UI;

namespace SchedulerDemo.Forms {
    public partial class HospitalRecurrenceTypeEditor : UserControl {
        public HospitalRecurrenceTypeEditor() {
            InitializeComponent();
        }

        #region ViewModel
        public RecurrenceDialogViewModel ViewModel {
            get { return (RecurrenceDialogViewModel)GetValue(ViewModelProperty); }
            set { SetValue(ViewModelProperty, value); }
        }
        public static readonly DependencyProperty ViewModelProperty = CreateViewModelProperty();
        static DependencyProperty CreateViewModelProperty() {
            return DependencyProperty.Register("ViewModel", typeof(RecurrenceDialogViewModel), typeof(HospitalRecurrenceTypeEditor), new PropertyMetadata(null, OnRecurrenceElementChangedProperty));
        }
        static void OnRecurrenceElementChangedProperty(DependencyObject o, DependencyPropertyChangedEventArgs e) {
            ((HospitalRecurrenceTypeEditor)o).OnRecurrenceElementChanged((RecurrenceDialogViewModel)e.OldValue, (RecurrenceDialogViewModel)e.NewValue);
        }
        void OnRecurrenceElementChanged(RecurrenceDialogViewModel oldValue, RecurrenceDialogViewModel newValue) {
            SetRecurrenceType(ViewModel.RecurrenceType);
        }
        #endregion
        #region IsReadOnly
        public bool IsReadOnly {
            get { return (bool)GetValue(IsReadOnlyProperty); }
            set { SetValue(IsReadOnlyProperty, value); }
        }
        public static readonly DependencyProperty IsReadOnlyProperty = DependencyProperty.Register("IsReadOnly", typeof(bool), typeof(HospitalRecurrenceTypeEditor), new PropertyMetadata(false, new PropertyChangedCallback(OnIsReadOnlyPropertyChanged)));
        protected static void OnIsReadOnlyPropertyChanged(DependencyObject o, DependencyPropertyChangedEventArgs e) {
            ((HospitalRecurrenceTypeEditor)o).OnIsReadOnlyChanged((bool)e.OldValue, (bool)e.NewValue);
        }
        protected virtual void OnIsReadOnlyChanged(bool oldValue, bool newValue) {
            brdRecurrenceType.IsHitTestVisible = !newValue;
        }
        #endregion

        protected internal virtual void SetRecurrenceType(Nullable<RecurrenceType> type) {
            switch (type) {
                case RecurrenceType.Daily:
                    DailyRadioButton.IsChecked = true;
                    break;
                case RecurrenceType.Weekly:
                    WeeklyRadioButton.IsChecked = true;
                    break;
                case RecurrenceType.Monthly:
                    MonthlyRadioButton.IsChecked = true;
                    break;
                default:
                    DailyRadioButton.IsChecked = true;
                    break;
            }
        }

        private void DailyRadioButton_Checked(object sender, RoutedEventArgs e) {
            ViewModel.RecurrenceType = RecurrenceType.Daily;
        }

        private void WeeklyRadioButton_Checked(object sender, RoutedEventArgs e) {
            ViewModel.RecurrenceType = RecurrenceType.Weekly;
        }

        private void MonthlyRadioButton_Checked(object sender, RoutedEventArgs e) {
            ViewModel.RecurrenceType = RecurrenceType.Monthly;
        }
    }
}
