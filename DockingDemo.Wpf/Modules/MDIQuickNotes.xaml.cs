using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using DevExpress.Xpf.Bars;
using DevExpress.Xpf.Docking;
using DevExpress.Xpf.Docking.Base;
using DevExpress.Xpf.Editors.Settings;
using DevExpress.Xpf.NavBar;

namespace DockingDemo {
    public partial class MDIQuickNotes : DockingDemoModule {
        RichTextBox UnavailableEdit;
        public MDIQuickNotes() {
            DockLayoutManagerParameters.LayoutControlItemCaptionFormat = "{0}";
            InitializeComponent();
            UnavailableEdit = new RichTextBox();
            UnavailableEdit.IsEnabled = false;
            UnavailableEdit.Visibility = System.Windows.Visibility.Collapsed;
            dockManager.AllowCustomization = false;
            dockManager.AllowLayoutItemRename = true;
            AllNotes = DefaultBook.GetNotes();
            DataContext = AllNotes;
            Loaded += new RoutedEventHandler(MDIQuickNotes_Loaded);
            Unloaded += new RoutedEventHandler(MDIQuickNotes_UnLoaded);
            dockManager.LayoutItemActivating += dockManager_LayoutItemActivating;
            dockManager.DockItemActivating += dockManager_DockItemActivating;
            dockManager.DockItemClosed += dockManager_DockItemClosed;
        }
        void bHome_ItemClick(object sender, DevExpress.Xpf.Bars.ItemClickEventArgs e) {
            NavigateHomePage();
        }
        void MDIQuickNotes_UnLoaded(object sender, RoutedEventArgs e) {
            Unloaded -= new RoutedEventHandler(MDIQuickNotes_UnLoaded);
            ((ComboBoxEditSettings)eFontSize.EditSettings).ItemsSource = null;
            ((ComboBoxEditSettings)eFontFamily.EditSettings).ItemsSource = null;
        }
        bool modified;
        protected bool Modified {
            get { return modified; }
            set {
                if(Modified == value) return;
                modified = value;
                OnModifiedChanged();
            }
        }
        void OnModifiedChanged() {
            if(Modified)
                bModified.Content = "Modified";
            else
                bModified.Content = "";
        }
        void bAbout_ItemClick(object sender, DevExpress.Xpf.Bars.ItemClickEventArgs e) {
            ShowAbout();
        }
        void bExit_ItemClick(object sender, DevExpress.Xpf.Bars.ItemClickEventArgs e) {
        }
        void bUndo_ItemClick(object sender, DevExpress.Xpf.Bars.ItemClickEventArgs e) {
            GetEdit().Undo();
        }
        void bRedo_ItemClick(object sender, ItemClickEventArgs e) {
            GetEdit().Redo();
        }
        void bCut_ItemClick(object sender, DevExpress.Xpf.Bars.ItemClickEventArgs e) {
            GetEdit().Cut();
        }
        void bCopy_ItemClick(object sender, DevExpress.Xpf.Bars.ItemClickEventArgs e) {
            GetEdit().Copy();
        }
        void bPaste_ItemClick(object sender, DevExpress.Xpf.Bars.ItemClickEventArgs e) {
            GetEdit().Paste();
        }
        void bClear_ItemClick(object sender, DevExpress.Xpf.Bars.ItemClickEventArgs e) {
            (GetEdit().Document as FlowDocument).Blocks.Clear();
        }
        void bSelectAll_ItemClick(object sender, DevExpress.Xpf.Bars.ItemClickEventArgs e) {
            GetEdit().SelectAll();
        }
        void bFind_ItemClick(object sender, DevExpress.Xpf.Bars.ItemClickEventArgs e) {
        }
        void bReplace_ItemClick(object sender, DevExpress.Xpf.Bars.ItemClickEventArgs e) {
        }
        void bFont_ItemClick(object sender, DevExpress.Xpf.Bars.ItemClickEventArgs e) {
        }
        void bNew_ItemClick(object sender, DevExpress.Xpf.Bars.ItemClickEventArgs e) {
        }
        void bOpen_ItemClick(object sender, DevExpress.Xpf.Bars.ItemClickEventArgs e) {
        }
        void bClose_ItemClick(object sender, ItemClickEventArgs e) {
        }
        void bSave_ItemClick(object sender, ItemClickEventArgs e) {
        }
        void bSaveAs_ItemClick(object sender, ItemClickEventArgs e) {
        }
        void bPrint_ItemClick(object sender, ItemClickEventArgs e) {
        }
        bool isInvokePending = false;
        void InvokeUpdateFormat() {
            if(!isInvokePending) {
                Dispatcher.BeginInvoke(DispatcherPriority.Background, new Action(UpdateFormat));
                isInvokePending = true;
            }
            UpdateFormat();
        }
        protected void UpdateFormat() {
            object value;
            RichTextBox edit = GetEdit();
            value = edit.Selection.GetPropertyValue(TextElement.FontFamilyProperty);
            eFontFamily.EditValue = (value == DependencyProperty.UnsetValue) ? null : value;

            value = edit.Selection.GetPropertyValue(TextElement.FontSizeProperty);
            eFontSize.EditValue = (value == DependencyProperty.UnsetValue) ? null : value;

            value = edit.Selection.GetPropertyValue(TextElement.ForegroundProperty);
            fontColorChooser.Color = (value == DependencyProperty.UnsetValue) ? Colors.Black : ((SolidColorBrush)value).Color;

            value = edit.Selection.GetPropertyValue(TextElement.FontWeightProperty);
            bBold.IsChecked = (value == DependencyProperty.UnsetValue) ? false : ((FontWeight)value == FontWeights.Bold);

            value = edit.Selection.GetPropertyValue(TextElement.FontStyleProperty);
            bItalic.IsChecked = (value == DependencyProperty.UnsetValue) ? false : ((FontStyle)value == FontStyles.Italic);

            value = edit.Selection.GetPropertyValue(Inline.TextDecorationsProperty);
            bUnderline.IsChecked = (value == DependencyProperty.UnsetValue) ? false : value != null && System.Windows.TextDecorations.Underline.Equals(value);

            value = edit.Selection.GetPropertyValue(Paragraph.TextAlignmentProperty);
            bLeft.IsChecked = (value == DependencyProperty.UnsetValue) ? true : (TextAlignment)value == TextAlignment.Left;
            bCenter.IsChecked = (value == DependencyProperty.UnsetValue) ? false : (TextAlignment)value == TextAlignment.Center;
            bRight.IsChecked = (value == DependencyProperty.UnsetValue) ? false : (TextAlignment)value == TextAlignment.Right;

            Paragraph startParagraph = edit.Selection.Start.Paragraph;
            Paragraph endParagraph = edit.Selection.End.Paragraph;
            if(startParagraph != null && endParagraph != null && (startParagraph.Parent is ListItem) && (endParagraph.Parent is ListItem) && object.ReferenceEquals(((ListItem)startParagraph.Parent).List, ((ListItem)endParagraph.Parent).List)) {
                TextMarkerStyle markerStyle = ((ListItem)startParagraph.Parent).List.MarkerStyle;
                bBullets.IsChecked = (markerStyle == TextMarkerStyle.Disc || markerStyle == TextMarkerStyle.Circle || markerStyle == TextMarkerStyle.Square || markerStyle == TextMarkerStyle.Box);
            }
            else {
                bBullets.IsChecked = false;
            }
            isInvokePending = false;
        }
        void fontColorChooser_ColorChanged(object sender, EventArgs e) {
            if(isInvokePending) return;
            GetEdit().Selection.ApplyPropertyValue(TextElement.ForegroundProperty, new SolidColorBrush(fontColorChooser.Color));
            InvokeUpdateFormat();
            InvokeFocusEdit();
        }
        void eFontSize_EditValueChanged(object sender, EventArgs e) {
            if(eFontSize.EditValue == null || isInvokePending) return;
            GetEdit().Selection.ApplyPropertyValue(TextElement.FontSizeProperty, eFontSize.EditValue);
            InvokeUpdateFormat();
            InvokeFocusEdit();
        }
        void eFontFamily_EditValueChanged(object sender, EventArgs e) {
            if(eFontFamily.EditValue == null || isInvokePending) return;
            GetEdit().Selection.ApplyPropertyValue(TextElement.FontFamilyProperty, eFontFamily.EditValue.ToString());
            InvokeUpdateFormat();
            InvokeFocusEdit();
        }
        void InvokeFocusEdit() {
            RichTextBox edit = GetEdit();
            Dispatcher.BeginInvoke(DispatcherPriority.Background, new Action(delegate() { edit.Focus(); }));
        }
        RichTextBox GetEdit() {
            DocumentPanel panel = notesContainer.SelectedItem as DocumentPanel;
            if(panel != null && panel.Layout != null) {
                LayoutGroup group = panel.Layout[0] as LayoutGroup;
                if(group != null) {
                    LayoutControlItem item = group.SelectedItem as LayoutControlItem;
                    if(item == null) item = group[0] as LayoutControlItem;
                    if(item != null) {
                        return (item.Control as RichTextBox) ?? UnavailableEdit;
                    }
                }
            }
            return UnavailableEdit;
        }
        void dockManager_DockItemClosed(object sender, DockItemClosedEventArgs e) {
            DocumentPanel document = e.Item as DocumentPanel;
            if(document != null) {
                Note note = Note.GetNote(document);
                if(note != null && AllNotes.Remove(note)) {
                    dockManager.DockController.RemovePanel(document);
                    Note.SetNote(document, null);
                }
            }
        }
        void dockManager_LayoutItemActivating(object sender, ItemCancelEventArgs e) {
            if(e.Item.Name == "newNotePage") {
                LayoutControlItem item =  CreateNewNotePage(e.Item.Parent);
                e.Cancel = item != null;
                if(item != null) {
                    ActivateDelegate activate = new ActivateDelegate(ActivateLayoutItem);
                    Dispatcher.BeginInvoke(activate, new object[] { item });
                }
            }
        }
        void dockManager_DockItemActivating(object sender, ItemCancelEventArgs e) {
            if(e.Item.Name == "newNote") {
                DocumentPanel panel = CreateNewNote();
                e.Cancel = panel != null;
                if(panel != null) {
                    ActivateDelegate activate = new ActivateDelegate(ActivateDockItem);
                    Dispatcher.BeginInvoke(activate, new object[] { panel });
                }
            }
        }
        delegate void ActivateDelegate(BaseLayoutItem item);
        void ActivateDockItem(BaseLayoutItem panel) {
            dockManager.DockController.Activate(panel);
        }
        void ActivateLayoutItem(BaseLayoutItem item) {
            dockManager.LayoutController.Activate(item);
        }
        DocumentPanel CreateNewNote() {
            Note note = new Note() { Caption = "Untitled", Book = ActiveBook };
            NotePage page = new NotePage() { Caption = "Untitled" };
            note.Pages.Add(page);
            DocumentPanel notePanel = CreateNoteDocument(note);
            bool result = dockManager.DockController.Insert(
                    notesContainer, notePanel, notesContainer.Items.Count - 1
                );
            if(result) AllNotes.Add(note);
            return result ? notePanel : null;
        }
        LayoutControlItem CreateNewNotePage(LayoutGroup group) {
            NotePage page = new NotePage() { Caption = "Untitled" };
            ActiveNote.Pages.Add(page);
            LayoutControlItem notePage = CreateNotePage(page);
            bool result = dockManager.DockController.Insert(group, notePage, group.Items.Count - 1);
            return result ? notePage : null;
        }
        void MDIQuickNotes_Loaded(object sender, RoutedEventArgs e) {
            Loaded -= new RoutedEventHandler(MDIQuickNotes_Loaded);
            noteBooksNavBar.ItemsSource = AllNotes;
            noteBooksNavBar.Loaded += new RoutedEventHandler(noteBooksNavBar_Loaded);
        }
        void noteBooksNavBar_Loaded(object sender, RoutedEventArgs e) {
            noteBooksNavBar.Loaded -= new RoutedEventHandler(noteBooksNavBar_Loaded);
            noteBooksNavBar.View.SelectItem((NavBarItem)noteBooksNavBar.Groups[0].Items[0]);
            ((ComboBoxEditSettings)eFontSize.EditSettings).ItemsSource = FontSizes.Items;
            ((ComboBoxEditSettings)eFontFamily.EditSettings).ItemsSource = FontFamilies.FontNames;
            UpdateFormat();
        }
        protected virtual void OnEditTextChanged(object sender, TextChangedEventArgs e) {
            Modified = true;
        }
        protected virtual void OnEditSelectionChanged(object sender, RoutedEventArgs e) {
            UpdateStatus();
        }
        protected virtual void OnEditLoaded(object sender, RoutedEventArgs e) {
            UpdateStatus();
        }
        void UpdateStatus() {
            int line = 0;
            int col = 0;
            RichTextBox edit = GetEdit();
            edit.CaretPosition.GetLineStartPosition(-100000, out line);
            col = edit.CaretPosition.GetOffsetToPosition(edit.CaretPosition.GetLineStartPosition(0));
            bPosInfo.Content = "Line: " + (-line).ToString() + "  Position: " + (-col).ToString();
            InvokeUpdateFormat();
        }
        public ObservableCollection<Note> AllNotes { get; set; }
        string activeBookCore;
        protected string ActiveBook {
            get { return activeBookCore; }
            set {
                if(activeBookCore == value) return;
                activeBookCore = value;
                OnActiveBookChanged(value);
            }
        }
        Note activeNoteCore;
        protected Note ActiveNote {
            get { return activeNoteCore; }
            set {
                if(activeNoteCore == value) return;
                activeNoteCore = value;
                OnActiveNoteChanged(value);
            }
        }
        void OnActiveBookChanged(string book) {
            ClearNotes();
            foreach(Note note in AllNotes) {
                if(note.Book != book) continue;
                dockManager.DockController.Insert(
                        notesContainer, CreateNoteDocument(note), notesContainer.Items.Count - 1
                    );
            }
        }
        void ClearNotes() {
            BaseLayoutItem[] notes = notesContainer.GetItems();
            foreach(DocumentPanel panel in notes) {
                if(panel.Name == "newNote") continue;
                notesContainer.Remove(panel);
            }
        }
        DocumentPanel CreateNoteDocument(Note note) {
            DocumentPanel document = new DocumentPanel();

            document.HorizontalScrollBarVisibility = ScrollBarVisibility.Disabled;
            document.VerticalScrollBarVisibility = ScrollBarVisibility.Disabled;
            document.AllowFloat = false;
            document.AllowMove = false;
            document.Name = note.Caption;
            document.Caption = note.Caption;

            Note.SetNote(document, note);
            LayoutGroup tabs = new LayoutGroup();
            tabs.Caption = "Pages";
            tabs.ShowCaption = true;
            tabs.GroupBorderStyle = GroupBorderStyle.Tabbed;
            tabs.CaptionLocation = CaptionLocation.Bottom;
            tabs.AddRange(CreateNotePages(note));
            tabs.Add(CreateNewItemPage());
            LayoutGroup group = new LayoutGroup();
            group.Add(tabs);
            document.Content = group;
            return document;
        }
        LayoutControlItem CreateNewItemPage() {
            LayoutControlItem item = new LayoutControlItem();
            item.Name = "newNotePage";
            item.CaptionImage = DefaultBook.NewNoteImage;
            item.ShowCaption = false;
            item.ShowControl = false;
            item.Margin = new Thickness(0, -1, 0, 1);
            item.ToolTip = "Create a new page";
            return item;
        }
        BaseLayoutItem[] CreateNotePages(Note note) {
            BaseLayoutItem[] items = new BaseLayoutItem[note.Pages.Count];
            for(int i = 0; i < items.Length; i++) {
                items[i] = CreateNotePage(note.Pages[i]);
            }
            return items;
        }
        LayoutControlItem CreateNotePage(NotePage page) {
            NoteRichTextBox rtb = new NoteRichTextBox();

            LoadRtf(page, new TextRange(rtb.Document.ContentStart, rtb.Document.ContentEnd));
            rtb.Loaded += new RoutedEventHandler(OnEditLoaded);
            rtb.TextChanged += new TextChangedEventHandler(OnEditTextChanged);
            rtb.SelectionChanged += new RoutedEventHandler(OnEditSelectionChanged);

            Button pageRenameButton = new Button();
            pageRenameButton.Template =  FindResource("renameButton") as ControlTemplate;;
            pageRenameButton.ToolTip = "Rename page";


            Button pageCloseButton = new Button();
            pageCloseButton.Template = FindResource("deleteButton") as ControlTemplate;
            pageCloseButton.ToolTip = "Delete page";

            StackPanel panel = new StackPanel();
            panel.Orientation = Orientation.Horizontal;
            panel.Margin = new Thickness(0,-1,0,1);
            panel.VerticalAlignment = VerticalAlignment.Bottom;
            panel.Children.Add(pageRenameButton);
            panel.Children.Add(pageCloseButton);

            pageCloseButton.Click += ClosePageClick;
            pageRenameButton.Click += RenamePageClick;

            LayoutControlItem result = new LayoutControlItem();
            result.Caption = page.Caption;
            result.CaptionImage = page.CaptionImage;
            result.CaptionImageLocation = ImageLocation.AfterText;
            result.ShowCaption = false;
            result.ControlBoxContent = panel;
            result.Content = rtb;
            return result;
        }
        void RenamePageClick(object sender, RoutedEventArgs e) {
            LayoutControlItem item = DockLayoutManager.GetLayoutItem((DependencyObject)sender) as LayoutControlItem;
            if(item != null) {
                dockManager.LayoutController.Rename(item);
            }
        }
        void ClosePageClick(object sender, RoutedEventArgs e) {
            LayoutControlItem item = DockLayoutManager.GetLayoutItem((DependencyObject)sender) as LayoutControlItem;
            if(item != null) {
                item.Parent.Remove(item);
            }
        }
        void LoadRtf(NotePage page, TextRange range) {
            if(string.IsNullOrEmpty(page.Path)) return;
            Assembly assembly = Assembly.GetAssembly(GetType());
            using(Stream stream = assembly.GetManifestResourceStream(page.Path)) {
                if(stream != null)
                    range.Load(stream, DataFormats.Rtf);
            }
        }
        void OnActiveNoteChanged(Note note) {
            if(note != null)
                dockManager.DockController.Activate(notesContainer[note.Caption]);
        }
        void NotebooksItemSelected(object sender, NavBarItemSelectedEventArgs e) {
            ActiveBook = (string)e.Group.Header;
            if(e.Item != null)
                ActiveNote = e.Item.GetValue(FrameworkElement.DataContextProperty) as Note;
        }
    }
    public class Note {
        #region Static
        public static readonly DependencyProperty NoteProperty = DependencyProperty.RegisterAttached("Note", typeof(Note), typeof(Note), new PropertyMetadata(null));
        public static Note GetNote(DependencyObject obj) {
            return (Note)obj.GetValue(NoteProperty);
        }
        public static void SetNote(DependencyObject obj, Note value) {
            obj.SetValue(NoteProperty, value);
        }
        #endregion Static
        public Note() {
            Pages = new ObservableCollection<NotePage>();
        }
        public string Book { get; set; }
        public string Caption { get; set; }
        public ObservableCollection<NotePage> Pages { get; private set; }
    }
    public class NotePage {
        public string Caption { get; set; }
        public ImageSource CaptionImage { get; set; }
        public string Path { get; set; }
    }
    static class DefaultBook {
        static ImageSource newNoteImageCore;
        public static ImageSource NewNoteImage {
            get {
                if(newNoteImageCore == null) {
                    newNoteImageCore = new BitmapImage(new Uri("/Images/Icons/create.png", UriKind.Relative));
                }
                return newNoteImageCore;
            }
        }
        public static ObservableCollection<Note> GetNotes() {
            Note gNote0 = new Note() { Book = "General", Caption = "Features" };
            Note gNote1 = new Note() { Book = "General", Caption = "Tips" };
            gNote0.Pages.Add(new NotePage() { Caption = "General", Path = "DockingDemo.Data.QuickNotes_General.rtf" });
            gNote0.Pages.Add(new NotePage() { Caption = "Basics", Path = "DockingDemo.Data.QuickNotes_Basics.rtf" });
            gNote1.Pages.Add(new NotePage() { Caption = "Tips", Path = "DockingDemo.Data.QuickNotes_Tips.rtf" });

            Note wNote0 = new Note() { Book = "Work", Caption = "Meetings" };
            Note wNote1 = new Note() { Book = "Work", Caption = "Projects" };
            Note wNote2 = new Note() { Book = "Work", Caption = "Requests" };
            Note hNote0 = new Note() { Book = "Home", Caption = "Shopping" };
            hNote0.Pages.Add(new NotePage() { Caption = "Everyday Shopping" });
            hNote0.Pages.Add(new NotePage() { Caption = "Sunday Shopping" });
            Note hNote1 = new Note() { Book = "Home", Caption = "Garden" };
            Note hNote2 = new Note() { Book = "Home", Caption = "Kids" };
            Note hNote3 = new Note() { Book = "Home", Caption = "Party" };
            return new ObservableCollection<Note>(
                    new Note[] { gNote0, gNote1, wNote0, wNote1, wNote2, hNote0, hNote1, hNote2, hNote3 }
                );
        }
    }
    public class NoteRichTextBox : RichTextBox {
        public NoteRichTextBox() {
            BorderThickness = new Thickness(0);
            VerticalScrollBarVisibility = ScrollBarVisibility.Auto;
            Margin = new Thickness(-7);
        }
        protected override void OnLostFocus(RoutedEventArgs e) {
            e.Handled = true;
            base.OnLostFocus(e);
        }
        protected override void OnLostKeyboardFocus(KeyboardFocusChangedEventArgs e) {
            e.Handled = true;
            base.OnLostKeyboardFocus(e);
        }
    }
}
