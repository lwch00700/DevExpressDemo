using System;
using System.Windows;
using DevExpress.XtraRichEdit;
using DevExpress.Xpf.Editors;

namespace RichEditDemo {
    public partial class DocumentRestrictions : RichEditDemoModule {
        public DocumentRestrictions() {
            InitializeComponent();
            RtfLoadHelper.Load("TextWithImages.rtf", richEdit);
        }

        public bool IsReloadDocument { get { return true; } }
        public bool IsHideDisabled { get { return edtHideDisabled.IsChecked.Value; } }
        public string CurrentFileName { get { return richEdit.Options.DocumentSaveOptions.CurrentFileName; } }

        protected override void RaiseModuleAppear() {
            base.RaiseModuleAppear();
            richEdit.Options.DocumentCapabilities.CharacterFormatting = DevExpress.XtraRichEdit.DocumentCapability.Default;
        }

        void ReloadDocument() {
            if (IsReloadDocument && !String.IsNullOrEmpty(CurrentFileName))
                richEdit.LoadDocument(CurrentFileName);
        }
        DocumentCapability GetOptionValue(object sender) {
            if (((CheckEdit)sender).IsChecked.Value)
                return DocumentCapability.Enabled;
            if (IsHideDisabled)
                return DocumentCapability.Hidden;
            return DocumentCapability.Disabled;
        }
        public DocumentCapability UpdateBarItemVisibility(bool? capabilityCheckBoxChecked) {
            if (!capabilityCheckBoxChecked.HasValue || capabilityCheckBoxChecked.Value)
                return DocumentCapability.Enabled;
            return (IsHideDisabled) ? DocumentCapability.Hidden : DocumentCapability.Disabled;
        }

        void characterFormatting_CheckedChanged(object sender, RoutedEventArgs e) {
            richEdit.Options.DocumentCapabilities.CharacterFormatting = GetOptionValue(sender);
            ReloadDocument();
        }
        void characterStyle_CheckedChanged(object sender, RoutedEventArgs e) {
            richEdit.Options.DocumentCapabilities.CharacterStyle = GetOptionValue(sender);
            ReloadDocument();
        }
        void paragraphInsertNew_CheckedChanged(object sender, RoutedEventArgs e) {
            richEdit.Options.DocumentCapabilities.Paragraphs = GetOptionValue(sender);
            ReloadDocument();
        }
        void paragraphFormatting_CheckedChanged(object sender, RoutedEventArgs e) {
            richEdit.Options.DocumentCapabilities.ParagraphFormatting = GetOptionValue(sender);
            ReloadDocument();
        }
        void paragraphStyle_CheckedChanged(object sender, RoutedEventArgs e) {
            richEdit.Options.DocumentCapabilities.ParagraphStyle = GetOptionValue(sender);
            ReloadDocument();
        }
        void paragraphTabs_CheckedChanged(object sender, RoutedEventArgs e) {
            richEdit.Options.DocumentCapabilities.ParagraphTabs = GetOptionValue(sender);
            ReloadDocument();
        }
        void numberingBulleted_CheckedChanged(object sender, RoutedEventArgs e) {
            richEdit.Options.DocumentCapabilities.Numbering.Bulleted = GetOptionValue(sender);
            ReloadDocument();
        }
        void numberingMultilevel_CheckedChanged(object sender, RoutedEventArgs e) {
            richEdit.Options.DocumentCapabilities.Numbering.MultiLevel = GetOptionValue(sender);
            ReloadDocument();
        }
        void numberingSimple_CheckedChanged(object sender, RoutedEventArgs e) {
            richEdit.Options.DocumentCapabilities.Numbering.Simple = GetOptionValue(sender);
            ReloadDocument();
        }
        void contentPictures_CheckedChanged(object sender, RoutedEventArgs e) {
            richEdit.Options.DocumentCapabilities.InlinePictures = GetOptionValue(sender);
            ReloadDocument();
        }
        void contentTabs_CheckedChanged(object sender, RoutedEventArgs e) {
            richEdit.Options.DocumentCapabilities.TabSymbol = GetOptionValue(sender);
            ReloadDocument();
        }
        void contentFloatingObjects_CheckedChanged(object sender, RoutedEventArgs e) {
            richEdit.Options.DocumentCapabilities.FloatingObjects = GetOptionValue(sender);
            ReloadDocument();
        }
        void hyperlinks_CheckedChanged(object sender, RoutedEventArgs e) {
            richEdit.Options.DocumentCapabilities.Hyperlinks = GetOptionValue(sender);
            ReloadDocument();
        }
        void bookmarks_CheckedChanged(object sender, RoutedEventArgs e) {
            richEdit.Options.DocumentCapabilities.Bookmarks = GetOptionValue(sender);
            ReloadDocument();
        }
        void sections_CheckedChanged(object sender, RoutedEventArgs e) {
            richEdit.Options.DocumentCapabilities.Sections = GetOptionValue(sender);
            ReloadDocument();
        }
        void headersFooters_CheckedChanged(object sender, RoutedEventArgs e) {
            richEdit.Options.DocumentCapabilities.HeadersFooters = GetOptionValue(sender);
            ReloadDocument();
        }
        void tables_CheckedChanged(object sender, RoutedEventArgs e) {
            richEdit.Options.DocumentCapabilities.Tables = GetOptionValue(sender);
            ReloadDocument();
        }

        void edtHideDisabled_Checked(object sender, RoutedEventArgs e) {
            richEdit.BeginUpdate();
            try {
                DocumentCapabilitiesOptions options = richEdit.Options.DocumentCapabilities;
                options.CharacterFormatting = UpdateBarItemVisibility(this.edtAllowCharacterFormatting.IsChecked);
                options.CharacterStyle = UpdateBarItemVisibility(this.edtAllowCharacterStyle.IsChecked);
                options.Paragraphs = UpdateBarItemVisibility(this.edtAllowParagraph.IsChecked);
                options.ParagraphFormatting = UpdateBarItemVisibility(this.edtAllowParagraphPraperties.IsChecked);
                options.ParagraphStyle = UpdateBarItemVisibility(this.edtAllowParagraphStyle.IsChecked);
                options.ParagraphTabs = UpdateBarItemVisibility(this.edtAllowParagraphTabs.IsChecked);
                options.Hyperlinks = UpdateBarItemVisibility(this.edtAllowHyperlinks.IsChecked);
                options.Bookmarks = UpdateBarItemVisibility(this.edtAllowBookmarks.IsChecked);
                options.Numbering.Bulleted = UpdateBarItemVisibility(this.edtAllowBulletNumbering.IsChecked);
                options.Numbering.Simple = UpdateBarItemVisibility(this.edtAllowSimpleNumbering.IsChecked);
                options.Numbering.MultiLevel = UpdateBarItemVisibility(this.edtAllowMultiLevelNumbering.IsChecked);
                options.InlinePictures = UpdateBarItemVisibility(this.edtAllowPicture.IsChecked);
                options.TabSymbol = UpdateBarItemVisibility(this.edtAllowTabSymbol.IsChecked);
                options.Sections = UpdateBarItemVisibility(this.edtAllowSections.IsChecked);
                options.HeadersFooters = UpdateBarItemVisibility(this.edtAllowHeadersFooters.IsChecked);
                options.Tables = UpdateBarItemVisibility(this.edtAllowTables.IsChecked);
                ReloadDocument();
            }
            finally {
                richEdit.EndUpdate();
            }
        }
    }
}
