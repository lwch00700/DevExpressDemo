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
using DevExpress.Xpf.Editors;
using DevExpress.Xpf.Core;
using System.Windows.Threading;
using DevExpress.Xpf.Core.Native;
using System.Globalization;

namespace EditorsDemo {
    public partial class DateNavigatorModule : EditorsDemoModule {
        public DateNavigatorModule() {
            InitializeComponent();
            foreach (DayOfWeek day in DevExpress.Utils.EnumExtensions.GetValues(typeof(DayOfWeek)))
                ((CheckEdit)FindName("chk" + day.ToString())).IsChecked = navigator.Workdays.Contains(day);
            UpdateButtonsEnabledState();
        }

        void WeekDaysCheckEditChecked(object sender, RoutedEventArgs e) {
            CheckEdit edit = (CheckEdit)sender;
            DayOfWeek day = GetDay((string)edit.Content);
            if (!navigator.Workdays.Contains(day))
                navigator.Workdays.Add(day);
        }
        void WeekDaysCheckEditUnchecked(object sender, RoutedEventArgs e) {
            CheckEdit edit = (CheckEdit)sender;
            DayOfWeek day = GetDay((string)edit.Content);
            navigator.Workdays.Remove(day);
        }
        DayOfWeek GetDay(string day) {
            return (DayOfWeek)Enum.Parse(typeof(DayOfWeek), day, false);
        }

        void AddSpecialDate(object sender, RoutedEventArgs e) {
            navigator.SpecialDates.Add(deSpecialDate.DateTime);
            UpdateButtonsEnabledState();
        }
        void DeleteSpecialDate(object sender, RoutedEventArgs e) {
            List<object> selectedDates = new List<object>();
            foreach (DateTime date in lbSpecialDates.SelectedItems)
                selectedDates.Add(date);
            foreach (DateTime date in selectedDates)
                navigator.SpecialDates.Remove(date);
            UpdateButtonsEnabledState();
        }
        void DeleteAllSpecialDates(object sender, RoutedEventArgs e) {
            navigator.SpecialDates.Clear();
            UpdateButtonsEnabledState();
        }
        void UpdateButtonsEnabledState() {
            btnAddSpecialDate.IsEnabled = deSpecialDate.EditValue != null && !navigator.SpecialDates.Contains(deSpecialDate.DateTime);
            btnDeleteSpecialDate.IsEnabled = lbSpecialDates.SelectedItems.Count != 0;
            btnDeleteAllSpecialDates.IsEnabled = navigator.SpecialDates.Count != 0;
        }
        void deSpecialDate_EditValueChanged(object sender, EditValueChangedEventArgs e) {
            UpdateButtonsEnabledState();
        }
        void lbSpecialDates_SelectedIndexChanged(object sender, RoutedEventArgs e) {
            UpdateButtonsEnabledState();
        }
    }
}
