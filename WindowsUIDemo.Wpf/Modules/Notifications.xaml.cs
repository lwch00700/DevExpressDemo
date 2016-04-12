using DevExpress.Mvvm.UI.Interactivity;
using DevExpress.Xpf.DemoBase;
using DevExpress.Xpf.RichEdit;
using DevExpress.XtraRichEdit;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Media;

namespace WindowsUIDemo {
    [CodeFile("ViewModels/NotificationsViewModel.(cs)")]
    public partial class Notifications : WindowsUIDemoModule {
        public Notifications() {
            InitializeComponent();
#if CLICKONCE
            useWin8Notifications.IsChecked = false;
            useWin8Notifications.IsEnabled = false;
#endif
        }

        NotificationsViewModel ViewModel { get { return (NotificationsViewModel)DataContext; } }

        Stream HtmlStream { get { return typeof(Notifications).Assembly.GetManifestResourceStream("WindowsUIDemo.Modules.Views.CustomNotification.html"); } }

        void RichEditControl_Loaded(object sender, RoutedEventArgs e) {
            settingsRich.ContentChanged += settingsRich_ContentChanged;
            settingsRich.LoadDocument(HtmlStream, DocumentFormat.Html);
        }

        void UpdateRichEditors(bool updateSettingsRich) {
            using(var ms = new MemoryStream()) {
                settingsRich.SaveDocument(ms, DocumentFormat.Html);
                ms.Seek(0, SeekOrigin.Begin);
                MemoryStream patchedStream = PatchBackground(ms, ViewModel.CustomNotificationBackground);
                patchedStream.Seek(0, SeekOrigin.Begin);
                previewRich.LoadDocument(patchedStream, DocumentFormat.Html);
                ViewModel.CustomNotificationText = Encoding.UTF8.GetString(patchedStream.ToArray());
                if(updateSettingsRich) {
                    patchedStream.Seek(0, SeekOrigin.Begin);
                    settingsRich.LoadDocument(patchedStream, DocumentFormat.Html);
                }
            }
        }

        void settingsRich_ContentChanged(object sender, System.EventArgs e) {
            UpdateRichEditors(false);
        }

        void previewRich_Loaded(object sender, RoutedEventArgs e) {
            previewRich.LoadDocument(HtmlStream, DocumentFormat.Html);
        }

        MemoryStream PatchBackground(MemoryStream stream, Color color) {
            string doc = Encoding.UTF8.GetString(stream.ToArray());
            Regex rx = new Regex("color=\"#(.*?)\"");
            var matches = rx.Matches(doc);
            if (matches.Count > 0) {
                string strColor = matches[0].Groups[1].ToString();
                doc = doc.Replace(strColor, color.ToString().Substring(3));
            }
            return new MemoryStream(Encoding.UTF8.GetBytes(doc));
        }

        void PopupColorEdit_ColorChanged(object sender, RoutedEventArgs e) {
            UpdateRichEditors(true);
        }
    }

    public class RichEditBehavior : Behavior<RichEditControl> {
        public string Document {
            get { return (string)GetValue(DocumentProperty); }
            set { SetValue(DocumentProperty, value); }
        }
        public static readonly DependencyProperty DocumentProperty =
            DependencyProperty.Register("Document", typeof(string), typeof(RichEditBehavior), new PropertyMetadata(string.Empty, new PropertyChangedCallback(OnDocumentChanged)));

        private static void OnDocumentChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            ((RichEditBehavior)d).UpdateDocument();
        }
        protected override void OnAttached() {
            base.OnAttached();
            UpdateDocument();
        }

        void UpdateDocument() {
            if(AssociatedObject != null) {
                AssociatedObject.LoadDocument(new MemoryStream(Encoding.UTF8.GetBytes(Document)), DocumentFormat.Html);
            }
        }
    }
}
