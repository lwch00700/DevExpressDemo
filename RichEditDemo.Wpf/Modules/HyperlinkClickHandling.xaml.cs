using System;
using System.Collections.Generic;
using DevExpress.XtraRichEdit.API.Native;
using DevExpress.XtraRichEdit;
using DevExpress.Xpf.Core;
using System.Windows;
using DevExpress.Office.Utils;

namespace RichEditDemo {
    public partial class HyperlinkClickHandling : RichEditDemoModule {
        static List<string> products = CreateProducts();
        static List<string> managers = CreateManagers();
        static List<string> contactInfos = CreateContactInfos();
        static List<string> CreateProducts() {
            List<string> result = new List<string>();
            result.Add("DXScheduler™");
            result.Add("DXRichEdit™");
            result.Add("DXSpellChecker™");
            result.Add("DXGrid™");
            result.Add("DXPivotGrid™");
            result.Add("DXBars™");
            result.Add("DXCharts™");
            result.Add("DXLayoutControl™");
            result.Add("DXNavBar™");
            result.Add("DXEditors™");
            result.Add("DXPrinting™");
            result.Add("DXCarousel™");
            result.Add("DXThemes™");
            return result;
        }
        static List<string> CreateManagers() {
            List<string> result = new List<string>();
            result.Add("Mark M Leininger");
            result.Add("Caroline R Geraghty");
            result.Add("Dorothy M Salas");
            result.Add("Pete M Smith");
            result.Add("Lena D Carroll");
            result.Add("Shauna T Tuggle");
            result.Add("Mary R Spencer");
            result.Add("David G Rucker");
            result.Add("Barry D Phillips");
            result.Add("Ronald R Ross");
            result.Add("Nellie J Burdette");
            return result;
        }
        static List<string> CreateContactInfos() {
            List<string> result = new List<string>();
            result.Add("Mark.M.Leininger@dodgit.com, 773-384-2677");
            result.Add("Caroline.R.Geraghty@mailinator.com, 703-968-3712");
            result.Add("Dorothy.M.Salas@trashymail.com, 541-995-3154");
            result.Add("Pete.M.Smith@spambob.com, 954-568-0573");
            result.Add("Lena.D.Carroll@spambob.com, 772-663-2034");
            result.Add("Shauna.T.Tuggle@mailinator.com, 812-463-8021");
            result.Add("Mary.R.Spencer@dodgit.com, 510-819-1801");
            result.Add("David.G.Rucker@spambob.com, 423-550-7308");
            result.Add("Barry.D.Phillips@mailinator.com, 831-308-3866");
            result.Add("Ronald.R.Ross@dodgit.com, 847-242-3792");
            result.Add("Nellie.J.Burdette@pookmail.com, 937-777-5277");
            return result;
        }

        delegate PopupControlBase CreateControlDelegate();
        Dictionary<string, CreateControlDelegate> hyperlinkMappings;
        FloatingContainer activeWindow;

        public HyperlinkClickHandling() {
            InitializeComponent();
            RtfLoadHelper.Load("HyperlinkClickHandling.rtf", richEdit);
            ribbonControl1.SelectedPage = pageHome;
            this.hyperlinkMappings = CreateHyperlinkMappings();
            this.GotFocus += new RoutedEventHandler(HyperlinkClickHandling_GotFocus);
            SubscribeRichEditEvents();
        }

        void HyperlinkClickHandling_GotFocus(object sender, RoutedEventArgs e) {
            if (this.activeWindow != null) {
                this.activeWindow.IsOpen = false;
                this.activeWindow = null;
            }
        }
        private Dictionary<string, CreateControlDelegate> CreateHyperlinkMappings() {
            Dictionary<string, CreateControlDelegate> result = new Dictionary<string, CreateControlDelegate>();
            HyperlinkCollection hyperlinks = this.richEdit.Document.Hyperlinks;
            result.Add("OpenSelectProductForm", CreateSelectProductControl);
            result.Add("OpenSelectAmountForm", CreateSelectAmountControl);
            result.Add("OpenSelectDateForm", CreateSelectDateControl);
            result.Add("OpenSelectManagerForm", CreateSelectManagerControl);
            return result;
        }
        PopupControlBase CreateSelectProductControl() {
            SelectProductControl result = new SelectProductControl(products);
            result.Commit += OnProductControlCommit;
            return result;
        }
        void OnProductControlCommit(object sender, EventArgs e) {
            SelectProductControl control = (SelectProductControl)sender;
            string value = (string)control.EditValue;
            ReplaceRange(value, control.Range);
        }
        PopupControlBase CreateSelectDateControl() {
            SelectDateControl result = new SelectDateControl();
            result.Commit += OnSelectDateControlCommit;
            return result;
        }
        void OnSelectDateControlCommit(object sender, EventArgs e) {
            SelectDateControl control = (SelectDateControl)sender;
            string value = ((DateTime)control.EditValue).ToString("MMMM, dd yyyy");
            ReplaceRange(value, control.Range);
        }
        PopupControlBase CreateSelectAmountControl() {
            SelectAmountControl result = new SelectAmountControl();
            result.Commit += OnSelectAmountControlCommit;
            return result;
        }
        void OnSelectAmountControlCommit(object sender, EventArgs e) {
            SelectAmountControl control = (SelectAmountControl)sender;
            string value = ((decimal)control.EditValue).ToString("$0.00");
            ReplaceRange(value, control.Range);
        }
        PopupControlBase CreateSelectManagerControl() {
            SelectManagerControl result = new SelectManagerControl(managers, contactInfos);
            result.Commit += OnSelectManagerControl;
            return result;
        }
        void OnSelectManagerControl(object sender, EventArgs e) {
            SelectManagerControl control = (SelectManagerControl)sender;
            string value = (string)control.EditValue;
            ReplaceRange(value, control.Range);
        }
        private void SubscribeRichEditEvents() {
            this.richEdit.HyperlinkClick += new HyperlinkClickEventHandler(OnHyperlinkClick);
        }
        void OnHyperlinkClick(object sender, HyperlinkClickEventArgs e) {
            if (e.ModifierKeys != this.richEdit.Options.Hyperlinks.ModifierKeys)
                return;
            CreateControlDelegate createControl = null;
            if (!hyperlinkMappings.TryGetValue(e.Hyperlink.NavigateUri, out createControl))
                return;
            if (this.activeWindow != null) {
                this.activeWindow.IsOpen = false;
                this.activeWindow = null;
            }
            PopupControlBase control = createControl();
            control.Range = e.Hyperlink.Range;
            FloatingContainer container = FloatingContainerFactory.Create(FloatingMode.Window);
            control.OwnerWindow = container;
            container.Content = control;
            container.Owner = this.richEdit;
            container.Hidden += (obj, args) => {
                ((ILogicalOwner)this.richEdit).RemoveChild((FloatingContainer)obj);
            };
            ((ILogicalOwner)this.richEdit).AddChild(container);
            container.SizeToContent = SizeToContent.WidthAndHeight;
            container.ContainerStartupLocation = WindowStartupLocation.Manual;
            container.FloatLocation = GetWindowLocation();
            container.IsOpen = true;
            this.activeWindow = container;
            control.Focus();
            e.Handled = true;
        }
        void container_Hidden(object sender, RoutedEventArgs e) {
            FloatingContainer container = sender as FloatingContainer;
            ((ILogicalOwner)this.richEdit).RemoveChild(container);
        }
        Point GetWindowLocation() {
            DocumentPosition position = this.richEdit.Document.CaretPosition;
            System.Drawing.Rectangle rect = this.richEdit.GetBoundsFromPosition(position);
            System.Drawing.Point location = new System.Drawing.Point(rect.Right, rect.Bottom);
            System.Drawing.Point localPoint = Units.DocumentsToPixels(location, this.richEdit.DpiX, this.richEdit.DpiY);
            return new Point(localPoint.X, localPoint.Y);
        }
        int GetHyperlinkIndex(Hyperlink hyperlink) {
            HyperlinkCollection hyperlinks = this.richEdit.Document.Hyperlinks;
            int count = hyperlinks.Count;
            for (int i = 0; i < count; i++) {
                if (hyperlinks[i] == hyperlink)
                    return i;
            }
            return -1;
        }
        void ReplaceRange(string value, DocumentRange range) {
            Document document = this.richEdit.Document;
            document.BeginUpdate();
            try {
                document.Replace(range, value);
            }
            finally {
                document.EndUpdate();
            }
        }
    }
}
