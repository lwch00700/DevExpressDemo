using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using DevExpress.Xpf.RichEdit;
using DevExpress.XtraRichEdit.API.Layout;
using DevExpress.XtraRichEdit.API.Native;
using Rectangle = System.Drawing.Rectangle;

namespace RichEditDemo {
    public partial class LayoutAPI : RichEditDemoModule {
        public static readonly DependencyProperty CurrentTreeItemProperty = DependencyProperty.Register("CurrentTreeItem", typeof(RichEditTreeViewItem), typeof(LayoutAPI));
        static readonly DependencyProperty PageCountProperty = DependencyProperty.Register("PageCount", typeof(int), typeof(LayoutAPI), new PropertyMetadata(1));
        static readonly DependencyProperty ActivePageNumberProperty = DependencyProperty.Register("ActivePageNumber", typeof(int), typeof(LayoutAPI), new PropertyMetadata(1));
        ObservableCollection<LayoutInfoItem> layoutInfoItems;

        public LayoutAPI() {
            DataContext = this;
            this.layoutInfoItems = GetLayoutInfoItems();
            InitializeComponent();
            OpenXmlLoadHelper.Load("FloatingObjects.docx", richEdit);
            richEdit.DocumentLayout.DocumentFormatted += DocumentLayout_DocumentFormatted;
        }

        public RichEditTreeViewItem CurrentTreeItem {
            get { return (RichEditTreeViewItem)GetValue(CurrentTreeItemProperty); }
            set { SetValue(CurrentTreeItemProperty, value); }
        }
        public int PageCount {
            get { return (int)GetValue(PageCountProperty); }
            private set { SetValue(PageCountProperty, value); }
        }
        public int ActivePageNumber {
            get { return (int)GetValue(ActivePageNumberProperty); }
            private set { SetValue(ActivePageNumberProperty, value); }
        }
        public ObservableCollection<LayoutInfoItem> LayoutInfoItems { get { return layoutInfoItems; } }

        ObservableCollection<LayoutInfoItem> GetLayoutInfoItems() {
            ObservableCollection<LayoutInfoItem> result = new ObservableCollection<LayoutInfoItem>();
            LayoutType[] types = (LayoutType[])Enum.GetValues(typeof(LayoutType));
            Random rand = new Random();
            byte[] colorBytes = new byte[3];
            foreach (LayoutType type in types) {
                if (IsDrawingSupported(type)) {
                    rand.NextBytes(colorBytes);
                    result.Add(new LayoutInfoItem(type.ToString(), Color.FromRgb(colorBytes[0], colorBytes[1], colorBytes[2])));
                }
            }
            return result;
        }
        bool IsDrawingSupported(LayoutType type) {
            switch (type) {
                case LayoutType.BookmarkEndBox:
                    return false;
                case LayoutType.BookmarkStartBox:
                    return false;
                case LayoutType.CommentEndBox:
                    return false;
                case LayoutType.CommentStartBox:
                    return false;
                case LayoutType.CustomRunBox:
                    return false;
                case LayoutType.DataContainerRunBox:
                    return false;
                case LayoutType.RangePermissionEndBox:
                    return false;
                case LayoutType.RangePermissionStartBox:
                    return false;
                case LayoutType.HiddenTextUnderlineBox:
                    return false;
                case LayoutType.CharacterBox:
                    return false;
                case LayoutType.FloatingObjectAnchorBox:
                    return false;
                default:
                    return true;
            }
        }
        void DocumentLayout_DocumentFormatted(object sender, System.EventArgs e) {
            richEdit.Dispatcher.BeginInvoke(new Action(() => {
                layoutTreeView.Items.Clear();
                int pageCount = richEdit.DocumentLayout.GetFormattedPageCount();
                for (int i = 0; i < pageCount; i++)
                    AddTreeViewItem(layoutTreeView.Items, new TreeViewItemInfo(richEdit.DocumentLayout.GetPage(i), ContentDisplayAction.Select, true));
                PageCount = richEdit.DocumentLayout.GetPageCount();
            }));
        }
        void AddTreeViewItem(ItemCollection collection, TreeViewItemInfo info) {
            RichEditTreeViewItem item = new RichEditTreeViewItem(info);
            item.Header = GetTreeViewItemHeader(info.Element, collection.Count + 1);
            item.Expanded += OnTreeViewItemExpanded;
            item.Selected += OnTreeViewItemSelected;
            collection.Add(item);
        }
        string GetTreeViewItemHeader(LayoutElement element, int index) {
            string typeName = element.Type.ToString();
            switch (element.Type) {
                case LayoutType.Column:
                    return String.Format("{0} #{1}", typeName, index);
                case LayoutType.Page:
                    return String.Format("{0} #{1}", typeName, index);
                case LayoutType.Row:
                    return String.Format("{0} #{1}", typeName, index);
                case LayoutType.TableRow:
                    return String.Format("{0} #{1}", typeName, index);
            }
            return typeName;
        }
        void OnTreeViewItemSelected(object sender, RoutedEventArgs e) {
            RichEditTreeViewItem item = (RichEditTreeViewItem)sender;
            if (!item.IsSelected)
                return;
            CurrentTreeItem = item;
            richEdit.Document.ChangeActiveDocument(richEdit.Document);

            LayoutElement element = CurrentTreeItem.Info.Element;
            if (CurrentTreeItem.Info.DisplayAction == ContentDisplayAction.ScrollTo) {
                TryScrollToHeader(element);
                TryScrollToFooter(element);
                TryScrollToFloatingObject(element);
                TryScrollToComment(element);
            }
            else {
                RangedLayoutElement rangedElement = element as RangedLayoutElement;
                if (rangedElement == null)
                    return;
                ScrollToPosition(rangedElement.Range.Start);
                richEdit.Document.Selection = richEdit.Document.CreateRange(rangedElement.Range.Start, rangedElement.Range.Length);
            }
        }
        void TryScrollToHeader(LayoutElement element) {
            LayoutHeader header = element as LayoutHeader;
            if (header == null)
                header = element.GetParentByType<LayoutHeader>();
            if (header == null)
                return;
            LayoutPageArea nearestPageArea = ((LayoutPage)header.Parent).PageAreas.First;
            ScrollToPosition(nearestPageArea.Range.Start);
        }
        void TryScrollToFooter(LayoutElement element) {
            LayoutFooter footer = element as LayoutFooter;
            if (footer == null)
                footer = element.GetParentByType<LayoutFooter>();
            if (footer == null)
                return;
            LayoutPageArea nearestPageArea = ((LayoutPage)footer.Parent).PageAreas.Last;
            ScrollToPosition(nearestPageArea.Range.Start + nearestPageArea.Range.Length - 1);
        }
        void TryScrollToComment(LayoutElement element) {
            LayoutComment layoutComment = element as LayoutComment;
            if (layoutComment == null)
                layoutComment = element.GetParentByType<LayoutComment>();
            if (layoutComment != null) {
                Comment comment = layoutComment.GetDocumentComment();
                ScrollToPosition(comment.Range.Start);
            }
        }
        void TryScrollToFloatingObject(LayoutElement element) {
            LayoutFloatingObject layoutFloatingObject = element as LayoutFloatingObject;
            if (layoutFloatingObject == null)
                layoutFloatingObject = element.GetParentByType<LayoutTextBox>();
            if (layoutFloatingObject != null) {
                FloatingObjectAnchorBox anchor = layoutFloatingObject.AnchorBox;
                ScrollToPosition(anchor.Range.Start);
                richEdit.Document.Selection = richEdit.Document.CreateRange(anchor.Range.Start, anchor.Range.Length);
            }
        }
        void ScrollToPosition(int position) {
            richEdit.Document.CaretPosition = richEdit.Document.CreatePosition(position);
            richEdit.ScrollToCaret(0.5f);
        }
        void ScrollToPosition(DocumentPosition position) {
            richEdit.Document.CaretPosition = position;
            richEdit.ScrollToCaret(0.5f);
        }
        void OnTreeViewItemExpanded(object sender, RoutedEventArgs e) {
            RichEditTreeViewItem item = (RichEditTreeViewItem)sender;
            if (item.HasItems)
                return;
            ChildElementsCollector collector = new ChildElementsCollector(item.Info);
            int itemsCount = collector.Collection.Count;
            for (int i = 0; i < itemsCount; i++)
                AddTreeViewItem(item.Items, collector.Collection[i]);
        }
        void richEdit_BeforePagePaint(object sender, DevExpress.XtraRichEdit.BeforePagePaintEventArgs e) {
            e.Painter = new CustomPagePainter(LayoutInfoItems);
        }
        void IsCheckedChanged(object sender, RoutedEventArgs e) {
            richEdit.InvalidateDocumentLayout();
        }
        void PopupColorEdit_EditValueChanged(object sender, DevExpress.Xpf.Editors.EditValueChangedEventArgs e) {
            richEdit.Redraw(DevExpress.XtraRichEdit.Internal.RefreshAction.AllDocument);
        }
        void richEdit_SelectionChanged(object sender, EventArgs e) {
            RangedLayoutElement element = richEdit.DocumentLayout.GetElement<RangedLayoutElement>(richEdit.Document.CaretPosition);
            if (element != null)
                ActivePageNumber = richEdit.DocumentLayout.GetPageIndex(element) + 1;
        }
        void richEdit_VisiblePagesChanged(object sender, EventArgs e) {
            ActivePageNumber = richEdit.ActiveView.GetVisiblePageLayoutInfos()[0].PageIndex + 1;
        }
    }
    #region CustomPagePainter
    public class CustomPagePainter : PagePainter {
        ObservableCollection<LayoutInfoItem> layoutInfoItems;

        public CustomPagePainter(ObservableCollection<LayoutInfoItem> layoutInfoItems) {
            this.layoutInfoItems = layoutInfoItems;
        }

        ObservableCollection<LayoutInfoItem> LayoutInfoItems { get { return layoutInfoItems; } }

        public override void DrawRow(LayoutRow row) {
            base.DrawRow(row);
            HighlightElement(row);
        }
        public override void DrawPlainTextBox(PlainTextBox plainTextBox) {
            base.DrawPlainTextBox(plainTextBox);
            HighlightElement(plainTextBox);
        }
        public override void DrawPage(LayoutPage page) {
            base.DrawPage(page);
            HighlightElement(page);
        }
        public override void DrawColumn(LayoutColumn column) {
            base.DrawColumn(column);
            HighlightElement(column);
        }
        public override void DrawColumnBreakBox(PlainTextBox columnBreakBox) {
            base.DrawColumnBreakBox(columnBreakBox);
            HighlightElement(columnBreakBox);
        }
        public override void DrawComment(LayoutComment comment) {
            base.DrawComment(comment);
            HighlightElement(comment);
        }
        public override void DrawCommentHighlightAreaBox(CommentHighlightAreaBox commentHighlightAreaBox) {
            base.DrawCommentHighlightAreaBox(commentHighlightAreaBox);
            HighlightElement(commentHighlightAreaBox);
        }
        public override void DrawFieldHighlightAreaBox(FieldHighlightAreaBox fieldHighlightAreaBox) {
            base.DrawFieldHighlightAreaBox(fieldHighlightAreaBox);
            HighlightElement(fieldHighlightAreaBox);
        }
        public override void DrawFloatingPicture(LayoutFloatingPicture floatingPicture) {
            base.DrawFloatingPicture(floatingPicture);
            HighlightElement(floatingPicture);
        }
        public override void DrawFooter(LayoutFooter footer) {
            base.DrawFooter(footer);
            HighlightElement(footer);
        }
        public override void DrawHeader(LayoutHeader header) {
            base.DrawHeader(header);
            HighlightElement(header);
        }
        public override void DrawHighlightAreaBox(HighlightAreaBox highlightAreaBox) {
            base.DrawHighlightAreaBox(highlightAreaBox);
            HighlightElement(highlightAreaBox);
        }
        public override void DrawHyphenBox(PlainTextBox hyphen) {
            base.DrawHyphenBox(hyphen);
            HighlightElement(hyphen);
        }
        public override void DrawInlinePictureBox(InlinePictureBox inlinePictureBox) {
            base.DrawInlinePictureBox(inlinePictureBox);
            HighlightElement(inlinePictureBox);
        }
        public override void DrawLineBreakBox(PlainTextBox lineBreakBox) {
            base.DrawLineBreakBox(lineBreakBox);
            HighlightElement(lineBreakBox);
        }
        public override void DrawLineNumberBox(LineNumberBox lineNumberBox) {
            base.DrawLineNumberBox(lineNumberBox);
            HighlightElement(lineNumberBox);
        }
        public override void DrawNumberingListMarkBox(NumberingListMarkBox numberingListMarkBox) {
            base.DrawNumberingListMarkBox(numberingListMarkBox);
            HighlightElement(numberingListMarkBox);
        }
        public override void DrawNumberingListWithSeparatorBox(NumberingListWithSeparatorBox numberingListWithSeparatorBox) {
            base.DrawNumberingListWithSeparatorBox(numberingListWithSeparatorBox);
            HighlightElement(numberingListWithSeparatorBox);
        }
        public override void DrawPageArea(LayoutPageArea pageArea) {
            base.DrawPageArea(pageArea);
            HighlightElement(pageArea);
        }
        public override void DrawPageBreakBox(PlainTextBox pageBreakBox) {
            base.DrawPageBreakBox(pageBreakBox);
            HighlightElement(pageBreakBox);
        }
        public override void DrawPageNumberBox(PlainTextBox pageNumberBox) {
            base.DrawPageNumberBox(pageNumberBox);
            HighlightElement(pageNumberBox);
        }
        public override void DrawParagraphMarkBox(PlainTextBox paragraphMarkBox) {
            base.DrawParagraphMarkBox(paragraphMarkBox);
            HighlightElement(paragraphMarkBox);
        }
        public override void DrawRangePermissionHighlightAreaBox(RangePermissionHighlightAreaBox rangePermissionHighlightAreaBox) {
            base.DrawRangePermissionHighlightAreaBox(rangePermissionHighlightAreaBox);
            HighlightElement(rangePermissionHighlightAreaBox);
        }
        public override void DrawSectionBreakBox(PlainTextBox sectionBreakBox) {
            base.DrawSectionBreakBox(sectionBreakBox);
            HighlightElement(sectionBreakBox);
        }
        public override void DrawSpaceBox(PlainTextBox spaceBox) {
            base.DrawSpaceBox(spaceBox);
            HighlightElement(spaceBox);
        }
        public override void DrawSpecialTextBox(PlainTextBox specialTextBox) {
            base.DrawSpecialTextBox(specialTextBox);
            HighlightElement(specialTextBox);
        }
        public override void DrawStrikeoutBox(StrikeoutBox strikeoutBox) {
            base.DrawStrikeoutBox(strikeoutBox);
            HighlightElement(strikeoutBox);
        }
        public override void DrawTable(LayoutTable table) {
            base.DrawTable(table);
            HighlightElement(table);
        }
        public override void DrawTableCell(LayoutTableCell tableCell) {
            base.DrawTableCell(tableCell);
            HighlightElement(tableCell);
        }
        public override void DrawTableRow(LayoutTableRow tableRow) {
            base.DrawTableRow(tableRow);
            HighlightElement(tableRow);
        }
        public override void DrawTabSpaceBox(PlainTextBox tabSpaceBox) {
            base.DrawTabSpaceBox(tabSpaceBox);
            HighlightElement(tabSpaceBox);
        }
        public override void DrawTextBox(LayoutTextBox textBox) {
            base.DrawTextBox(textBox);
            HighlightElement(textBox);
        }
        public override void DrawUnderlineBox(UnderlineBox underlineBox) {
            base.DrawUnderlineBox(underlineBox);
            HighlightElement(underlineBox);
        }
        void HighlightElement(LayoutElement element) {
            HighlightElement(element, element.Bounds);
        }
        void HighlightElement(LayoutElement element, Rectangle bounds) {
            LayoutInfoItem item = GetItem(element.Type);
            if (item != null && item.IsChecked)
                Canvas.DrawRectangle(new RichEditPen(item.Color), bounds);
        }
        LayoutInfoItem GetItem(LayoutType type) {
            int count = LayoutInfoItems.Count;
            for (int i = 0; i < count; i++) {
                if (LayoutInfoItems[i].Name.Equals(type.ToString()))
                    return LayoutInfoItems[i];
            }
            return null;
        }
    }
    #endregion
    public enum ContentDisplayAction { Select, ScrollTo }
    public struct TreeViewItemInfo {
        LayoutElement element;
        ContentDisplayAction displayAction;
        bool hasChildNodes;

        public TreeViewItemInfo(LayoutElement element, ContentDisplayAction displayAction, bool hasChildNodes) {
            this.element = element;
            this.displayAction = displayAction;
            this.hasChildNodes = hasChildNodes;
        }

        public LayoutElement Element { get { return element; } }
        public ContentDisplayAction DisplayAction { get { return displayAction; } }
        public bool HasChildNodes { get { return hasChildNodes; } }
    }
    #region ChildElementsCollector
    class ChildElementsCollector : LayoutVisitor {
        List<TreeViewItemInfo> collection;
        TreeViewItemInfo parentElement;

        public ChildElementsCollector(TreeViewItemInfo parentElement) {
            this.collection = new List<TreeViewItemInfo>();
            this.parentElement = parentElement;
            Visit(ParentElement.Element);
        }

        public List<TreeViewItemInfo> Collection { get { return collection; } }
        public TreeViewItemInfo ParentElement { get { return parentElement; } }

        protected override void VisitPage(LayoutPage page) {
            TryAddElementToCollection(page, ContentDisplayAction.Select, true);
            base.VisitPage(page);
        }
        protected override void VisitHeader(LayoutHeader header) {
            TryAddElementToCollection(header, ContentDisplayAction.ScrollTo, true);
            base.VisitHeader(header);
        }
        protected override void VisitFooter(LayoutFooter footer) {
            TryAddElementToCollection(footer, ContentDisplayAction.ScrollTo, true);
            base.VisitFooter(footer);
        }
        protected override void VisitPageArea(LayoutPageArea pageArea) {
            TryAddElementToCollection(pageArea, ContentDisplayAction.Select, true);
            base.VisitPageArea(pageArea);
        }
        protected override void VisitBookmarkEndBox(BookmarkBox bookmarkEndBox) {
            TryAddElementToCollection(bookmarkEndBox, ContentDisplayAction.Select, false);
            base.VisitBookmarkEndBox(bookmarkEndBox);
        }
        protected override void VisitBookmarkStartBox(BookmarkBox bookmarkStartBox) {
            TryAddElementToCollection(bookmarkStartBox, ContentDisplayAction.Select, false);
            base.VisitBookmarkStartBox(bookmarkStartBox);
        }
        protected override void VisitColumn(LayoutColumn column) {
            TryAddElementToCollection(column, ContentDisplayAction.Select, true);
            base.VisitColumn(column);
        }
        protected override void VisitColumnBreakBox(PlainTextBox columnBreakBox) {
            TryAddElementToCollection(columnBreakBox, ContentDisplayAction.Select, false);
            base.VisitColumnBreakBox(columnBreakBox);
        }
        protected override void VisitComment(LayoutComment comment) {
            TryAddElementToCollection(comment, ContentDisplayAction.ScrollTo, true);
            base.VisitComment(comment);
        }
        protected override void VisitCommentEndBox(CommentBox commentEndBox) {
            TryAddElementToCollection(commentEndBox, ContentDisplayAction.Select, false);
            base.VisitCommentEndBox(commentEndBox);
        }
        protected override void VisitCommentHighlightAreaBox(CommentHighlightAreaBox commentHighlightAreaBox) {
            TryAddElementToCollection(commentHighlightAreaBox, ContentDisplayAction.Select, false);
            base.VisitCommentHighlightAreaBox(commentHighlightAreaBox);
        }
        protected override void VisitCommentStartBox(CommentBox commentStartBox) {
            TryAddElementToCollection(commentStartBox, ContentDisplayAction.Select, false);
            base.VisitCommentStartBox(commentStartBox);
        }
        protected override void VisitFieldHighlightAreaBox(FieldHighlightAreaBox fieldHighlightAreaBox) {
            TryAddElementToCollection(fieldHighlightAreaBox, ContentDisplayAction.Select, false);
            base.VisitFieldHighlightAreaBox(fieldHighlightAreaBox);
        }
        protected override void VisitFloatingObjectAnchorBox(FloatingObjectAnchorBox floatingObjectAnchorBox) {
            TryAddElementToCollection(floatingObjectAnchorBox, ContentDisplayAction.Select, false);
            base.VisitFloatingObjectAnchorBox(floatingObjectAnchorBox);
        }
        protected override void VisitFloatingPicture(LayoutFloatingPicture floatingPicture) {
            TryAddElementToCollection(floatingPicture, ContentDisplayAction.ScrollTo, false);
            base.VisitFloatingPicture(floatingPicture);
        }
        protected override void VisitHiddenTextUnderlineBox(HiddenTextUnderlineBox hiddenTextUnderlineBox) {
            TryAddElementToCollection(hiddenTextUnderlineBox, ContentDisplayAction.Select, false);
            base.VisitHiddenTextUnderlineBox(hiddenTextUnderlineBox);
        }
        protected override void VisitHighlightAreaBox(HighlightAreaBox highlightAreaBox) {
            TryAddElementToCollection(highlightAreaBox, ContentDisplayAction.Select, false);
            base.VisitHighlightAreaBox(highlightAreaBox);
        }
        protected override void VisitHyphenBox(PlainTextBox hyphen) {
            TryAddElementToCollection(hyphen, ContentDisplayAction.Select, false);
            base.VisitHyphenBox(hyphen);
        }
        protected override void VisitInlinePictureBox(InlinePictureBox inlinePictureBox) {
            TryAddElementToCollection(inlinePictureBox, ContentDisplayAction.Select, false);
            base.VisitInlinePictureBox(inlinePictureBox);
        }
        protected override void VisitLineBreakBox(PlainTextBox lineBreakBox) {
            TryAddElementToCollection(lineBreakBox, ContentDisplayAction.Select, false);
            base.VisitLineBreakBox(lineBreakBox);
        }
        protected override void VisitLineNumberBox(LineNumberBox lineNumberBox) {
            TryAddElementToCollection(lineNumberBox, ContentDisplayAction.Select, false);
            base.VisitLineNumberBox(lineNumberBox);
        }
        protected override void VisitNumberingListMarkBox(NumberingListMarkBox numberingListMarkBox) {
            TryAddElementToCollection(numberingListMarkBox, ContentDisplayAction.Select, false);
            base.VisitNumberingListMarkBox(numberingListMarkBox);
        }
        protected override void VisitNumberingListWithSeparatorBox(NumberingListWithSeparatorBox numberingListWithSeparatorBox) {
            TryAddElementToCollection(numberingListWithSeparatorBox, ContentDisplayAction.Select, true);
            base.VisitNumberingListWithSeparatorBox(numberingListWithSeparatorBox);
        }
        protected override void VisitPageBreakBox(PlainTextBox pageBreakBox) {
            TryAddElementToCollection(pageBreakBox, ContentDisplayAction.Select, false);
            base.VisitPageBreakBox(pageBreakBox);
        }
        protected override void VisitPageNumberBox(PlainTextBox pageNumberBox) {
            TryAddElementToCollection(pageNumberBox, ContentDisplayAction.Select, false);
            base.VisitPageNumberBox(pageNumberBox);
        }
        protected override void VisitParagraphMarkBox(PlainTextBox paragraphMarkBox) {
            TryAddElementToCollection(paragraphMarkBox, ContentDisplayAction.Select, false);
            base.VisitParagraphMarkBox(paragraphMarkBox);
        }
        protected override void VisitPlainTextBox(PlainTextBox plainTextBox) {
            TryAddElementToCollection(plainTextBox, ContentDisplayAction.Select, false);
            base.VisitPlainTextBox(plainTextBox);
        }
        protected override void VisitRangePermissionEndBox(RangePermissionBox rangePermissionEndBox) {
            TryAddElementToCollection(rangePermissionEndBox, ContentDisplayAction.Select, false);
            base.VisitRangePermissionEndBox(rangePermissionEndBox);
        }
        protected override void VisitRangePermissionHighlightAreaBox(RangePermissionHighlightAreaBox rangePermissionHighlightAreaBox) {
            TryAddElementToCollection(rangePermissionHighlightAreaBox, ContentDisplayAction.Select, false);
            base.VisitRangePermissionHighlightAreaBox(rangePermissionHighlightAreaBox);
        }
        protected override void VisitRangePermissionStartBox(RangePermissionBox rangePermissionStartBox) {
            TryAddElementToCollection(rangePermissionStartBox, ContentDisplayAction.Select, false);
            base.VisitRangePermissionStartBox(rangePermissionStartBox);
        }
        protected override void VisitRow(LayoutRow row) {
            TryAddElementToCollection(row, ContentDisplayAction.Select, true);
            base.VisitRow(row);
        }
        protected override void VisitSectionBreakBox(PlainTextBox sectionBreakBox) {
            TryAddElementToCollection(sectionBreakBox, ContentDisplayAction.Select, false);
            base.VisitSectionBreakBox(sectionBreakBox);
        }
        protected override void VisitSpaceBox(PlainTextBox spaceBox) {
            TryAddElementToCollection(spaceBox, ContentDisplayAction.Select, false);
            base.VisitSpaceBox(spaceBox);
        }
        protected override void VisitSpecialTextBox(PlainTextBox specialTextBox) {
            TryAddElementToCollection(specialTextBox, ContentDisplayAction.Select, false);
            base.VisitSpecialTextBox(specialTextBox);
        }
        protected override void VisitStrikeoutBox(StrikeoutBox strikeoutBox) {
            TryAddElementToCollection(strikeoutBox, ContentDisplayAction.Select, false);
            base.VisitStrikeoutBox(strikeoutBox);
        }
        protected override void VisitTable(LayoutTable table) {
            TryAddElementToCollection(table, ContentDisplayAction.Select, true);
            base.VisitTable(table);
        }
        protected override void VisitTableCell(LayoutTableCell tableCell) {
            TryAddElementToCollection(tableCell, ContentDisplayAction.Select, true);
            base.VisitTableCell(tableCell);
        }
        protected override void VisitTableRow(LayoutTableRow tableRow) {
            TryAddElementToCollection(tableRow, ContentDisplayAction.Select, true);
            base.VisitTableRow(tableRow);
        }
        protected override void VisitTabSpaceBox(PlainTextBox tabSpaceBox) {
            TryAddElementToCollection(tabSpaceBox, ContentDisplayAction.Select, false);
            base.VisitTabSpaceBox(tabSpaceBox);
        }
        protected override void VisitTextBox(LayoutTextBox textBox) {
            TryAddElementToCollection(textBox, ContentDisplayAction.ScrollTo, true);
            base.VisitTextBox(textBox);
        }
        protected override void VisitUnderlineBox(UnderlineBox underlineBox) {
            TryAddElementToCollection(underlineBox, ContentDisplayAction.Select, false);
            base.VisitUnderlineBox(underlineBox);
        }
        void TryAddElementToCollection(LayoutElement element, ContentDisplayAction showingType, bool hasChildNodes) {
            if (Object.ReferenceEquals(element.Parent, ParentElement.Element)) {
                ContentDisplayAction type = ParentElement.DisplayAction == ContentDisplayAction.ScrollTo ? ParentElement.DisplayAction : showingType;
                Collection.Add(new TreeViewItemInfo(element, type, hasChildNodes));
            }
        }
    }
    #endregion
    public class RichEditTreeViewItem : TreeViewItem {
        TreeViewItemInfo info;
        bool firstTimeExpanding = true;

        public RichEditTreeViewItem(TreeViewItemInfo info) {
            this.info = info;
            if (info.HasChildNodes)
                Items.Add(new TreeViewItem() { Header = "Loading..." });
        }

        protected override void OnExpanded(RoutedEventArgs e) {
            if (firstTimeExpanding && Info.HasChildNodes)
                Items.RemoveAt(0);
            firstTimeExpanding = false;
            base.OnExpanded(e);
        }

        public TreeViewItemInfo Info { get { return info; } }
    }
    public class LayoutInfoItem : DependencyObject {
        string name;
        static readonly DependencyProperty IsCheckedProperty = DependencyProperty.Register("IsChecked", typeof(bool), typeof(LayoutInfoItem), new PropertyMetadata(false));
        static readonly DependencyProperty ColorProperty = DependencyProperty.Register("Color", typeof(Color), typeof(LayoutInfoItem));

        public LayoutInfoItem(string name, Color color) {
            this.name = name;
            Color = color;
        }

        public string Name { get { return name; } set { name = value; } }
        public Color Color {
            get { return (Color)GetValue(ColorProperty); }
            set { SetValue(ColorProperty, value); }
        }
        public bool IsChecked {
            get { return (bool)GetValue(IsCheckedProperty); }
            set { SetValue(IsCheckedProperty, value); }
        }
    }
    public class PageInfoConverter : System.Windows.Data.IMultiValueConverter {
        public object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            return String.Format("Page: {0} of {1}", values[0], values[1]);
        }
        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture) {
            throw new NotImplementedException();
        }
    }
}
