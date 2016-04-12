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
using DevExpress.Xpf.Grid;
using System.Diagnostics;
using DevExpress.Xpf.Core;
using DevExpress.Xpf.Utils.Themes;
using DevExpress.Xpf.DemoBase;

namespace GridDemo {
    [CodeFile("ModuleResources/CardTemplatesClasses.(cs)")]
    [CodeFile("ModuleResources/CardTemplatesResources(.SL).xaml")]
    public partial class CardTemplates : GridDemoModule {
        public static readonly RoutedCommand SendMail = new RoutedCommand("SendMail", typeof(RowTemplate));

        public static readonly DependencyProperty SelectedTabIndexProperty;
        public static readonly DependencyProperty IsNotesExpandedProperty;
        static CardTemplates() {
            SelectedTabIndexProperty = DependencyProperty.RegisterAttached("SelectedTabIndex", typeof(int), typeof(CardTemplates), new PropertyMetadata(0, null, OnSelectedTabIndexCoerce));
            IsNotesExpandedProperty = DependencyProperty.RegisterAttached("IsNotesExpanded", typeof(bool), typeof(CardTemplates), new PropertyMetadata(false));
        }
        static object OnSelectedTabIndexCoerce(DependencyObject d, object baseValue) {
            if((int)baseValue == -1)
                return GetSelectedTabIndex(d);
            return baseValue;
        }

        public static void SetSelectedTabIndex(DependencyObject element, int value) {
            element.SetValue(SelectedTabIndexProperty, value);
        }
        public static int GetSelectedTabIndex(DependencyObject element) {
            return (int)element.GetValue(SelectedTabIndexProperty);
        }
        public static void SetIsNotesExpanded(DependencyObject element, bool value) {
            element.SetValue(IsNotesExpandedProperty, value);
        }
        public static bool GetIsNotesExpanded(DependencyObject element) {
            return (bool)element.GetValue(IsNotesExpandedProperty);
        }

        public CardTemplates() {
            InitializeComponent();
            CommandBindings.Add(new CommandBinding(SendMail, new ExecutedRoutedEventHandler(OnSendMail)));
        }
        void OnSendMail(object sender, ExecutedRoutedEventArgs e) {
            try {
                Process.Start("mailto:" + e.Parameter.ToString());
            } catch { }
        }
        private void cardHeaderTemplateListBox_SelectionChanged(object sender, DevExpress.Xpf.Editors.EditValueChangedEventArgs e) {
            if(grid == null)
                return;
            if(cardHeaderTemplateListBox.SelectedIndex == 0)
                view.ClearValue(DevExpress.Xpf.Grid.CardView.CardHeaderTemplateProperty);
            if(cardHeaderTemplateListBox.SelectedIndex == 1)
                view.CardHeaderTemplate = (DataTemplate)FindResource("headerTemplateFullName");
            if(cardHeaderTemplateListBox.SelectedIndex == 2)
                view.CardHeaderTemplate = (DataTemplate)FindResource("headerTemplateFullNameWithIcon");
        }

        private void eMailTemplateListBox_SelectionChanged(object sender, DevExpress.Xpf.Editors.EditValueChangedEventArgs e) {
            if(grid == null)
                return;
            if(eMailTemplateListBox.SelectedIndex == 0)
                colEMail.CellTemplate = null;
            if(eMailTemplateListBox.SelectedIndex == 1)
                colEMail.CellTemplate = (DataTemplate)FindResource("eMailTemplateCards");
            view.CardRowTemplateSelector = null;
            if(eMailTemplateListBox.SelectedIndex == 2)
                view.CardRowTemplateSelector = new EMailCardRowTemplateSelector((DataTemplate)FindResource("eMailTemplateCards"));
        }

        private void cardTemplateListBox_SelectionChanged(object sender, DevExpress.Xpf.Editors.EditValueChangedEventArgs e) {
            if(grid == null)
                return;
            if(cardTemplateListBox.SelectedIndex == 0) {
    view.ClearValue(CardsPanel.FixedSizeProperty);
                view.ClearValue(DevExpress.Xpf.Grid.CardView.CardTemplateProperty);
                eMailTemplateListBox.IsEnabled = true;
    return;
            }
   view.FixedSize = 300;
            if(cardTemplateListBox.SelectedIndex == 1) {
                view.CardTemplate = (DataTemplate)FindResource("cardTemplate1");
                eMailTemplateListBox.IsEnabled = false;
            }
            if(cardTemplateListBox.SelectedIndex == 2) {
                view.CardTemplate = (DataTemplate)FindResource("cardTemplate2");
                eMailTemplateListBox.IsEnabled = false;
            }
        }
    }
}
