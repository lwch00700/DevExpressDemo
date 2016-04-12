using DevExpress.DemoData.Utils;
using DevExpress.Mvvm.Native;
using DevExpress.Xpf.DemoBase.Helpers;
using DevExpress.Xpf.DemoBase.Helpers.TextColorizer;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace DockingDemo.ViewModels {
    public class VS2010DocumentViewModel : DocumentViewModel {
        public CodeLanguageText CodeLanguageText { get; private set; }
        public CodeLanguage CodeLanguage { get; private set; }
        protected override string WorkspaceName { get { return "DocumentHost"; } }
        static int count = 0;
        public override bool Open() {
            CodeLanguage = count % 2 == 0 ? CodeLanguage.XAML : CodeLanguage.CS;
            count++;
            DisplayName = string.Format("File{0}.{1}", count, CodeLanguage.ToString().ToLower());
            Glyph = new BitmapImage(new Uri("/DockingDemo;component/Images/VS2010Docking/FileCS_16x16.png", UriKind.Relative));
            Description = object.Equals(CodeLanguage, CodeLanguage.XAML) ? "Windows Markup File" : "Visual C# Source file";
            Footer = string.Format("c:\\...\\DockingDemo\\{0}", DisplayName);
            string filename = "VS2010Docking." + CodeLanguage.ToString().ToLower();
            CodeLanguageText = new CodeLanguageText(CodeLanguage, () => { return GetCodeText(filename); });
            return true;
        }
        string GetCodeText(string name) {
            System.Reflection.Assembly assembly = typeof(VS2010DocumentViewModel).Assembly;
            using(Stream stream = AssemblyHelper.GetEmbeddedResourceStream(assembly, DemoHelper.GetPath("Data/", assembly) + name, true)) {
                if(stream == null) return string.Empty;
                using(StreamReader reader = new StreamReader(stream)) {
                    return reader.ReadToEnd();
                }
            }
        }
    }
    public class VS2010MainWindowViewModel : MainWindowViewModel {
        protected override void InitDefaultLayout() {
            var panels = new List<PanelWorkspaceViewModel> { _ToolboxViewModel, _SolutionExplorerViewModel, _PropertiesViewModel, _ErrorListViewModel };
            foreach(var panel in panels) {
                OpenOrCloseWorkspace(panel);
            }
            PanelWorkspaceViewModel document = CreateDocumentViewModel();
            document.Open();
            OpenOrCloseWorkspace(document, true);
        }
        protected override PanelWorkspaceViewModel CreateDocumentViewModel() {
            return CreatePanelWorkspaceViewModel<VS2010DocumentViewModel>();
        }
        protected override void InitPanelWorkspaceViewModel(PanelWorkspaceViewModel viewModel) {
            base.InitPanelWorkspaceViewModel(viewModel);
            viewModel.ShowPinButton = true;
        }
        PanelWorkspaceViewModel previewItem;
        protected override void OnPreviewItemChaged() {
            if(this.previewItem!= null) {
                if(this.previewItem.IsPreview) {
                    this.previewItem.IsPreview = false;
                    this.previewItem.IsClosed = true;
                }
            }
            previewItem = CreateDocumentViewModel();
            previewItem.Open();
            previewItem.ShowPinButton = true;
            previewItem.Pinned = true;
            OpenOrCloseWorkspace(previewItem, true);
            previewItem.IsPreview = true;
        }
        protected override List<CommandViewModel> CreateFileCommands() {
            var projectRelayCommand = DelegateCommandFactory.Create<object>(OnNewFileExecuted);
            var fileRelayCommand = DelegateCommandFactory.Create<object>(OnNewFileExecuted);
            var fileOpenRelayCommand = DelegateCommandFactory.Create<object>(OnNewFileExecuted);
            var closeFileRelayCommand = DelegateCommandFactory.Create<object>(OnCloseFileExecuted);

            CommandViewModel newCommand = new CommandViewModel("New") { IsSubItem = true };
            CommandViewModel newProject = new CommandViewModel("Project...", projectRelayCommand) { Glyph = GlyphHelper.GetGlyph("/Images/VS2010Docking/NewProject_16x16.png"), KeyGesture = KeyGestureHelper.GetKeyGesure(Key.N, ModifierKeys.Control | ModifierKeys.Shift) };
            CommandViewModel newFile = new CommandViewModel("File...", fileRelayCommand) { Glyph = GlyphHelper.GetGlyph("/Images/VS2010Docking/File_16x16.png"), KeyGesture = KeyGestureHelper.GetKeyGesure(Key.N, ModifierKeys.Control) };
            newCommand.Commands = new List<CommandViewModel>() { newProject, newFile };

            CommandViewModel openCommand = new CommandViewModel("Open") { IsSubItem = true, };
            CommandViewModel openProject = new CommandViewModel("Project/Solution...") { Glyph = GlyphHelper.GetGlyph("/Images/VS2010Docking/OpenSolution_16x16.png"), KeyGesture = KeyGestureHelper.GetKeyGesure(Key.O, ModifierKeys.Control | ModifierKeys.Shift), IsEnabled = false };
            CommandViewModel openFile = new CommandViewModel("File...", fileOpenRelayCommand) { Glyph = GlyphHelper.GetGlyph("/Images/VS2010Docking/OpenFile_16x16.png"), KeyGesture = KeyGestureHelper.GetKeyGesure(Key.O, ModifierKeys.Control) };
            openCommand.Commands = new List<CommandViewModel>() { openProject, openFile };

            CommandViewModel closeFile = new CommandViewModel("Close", closeFileRelayCommand);
            CommandViewModel closeSolution = new CommandViewModel("Close Solution") { Glyph = GlyphHelper.GetGlyph("/Images/VS2010Docking/CloseSolution_16x16.png") };
            CommandViewModel save = new CommandViewModel("Save") { Glyph = GlyphHelper.GetGlyph("/Images/VS2010Docking/Save_16x16.png"), KeyGesture = KeyGestureHelper.GetKeyGesure(Key.S, ModifierKeys.Control) };
            CommandViewModel saveAll = new CommandViewModel("Save All") { Glyph = GlyphHelper.GetGlyph("/Images/VS2010Docking/SaveAll_16x16.png"), KeyGesture = KeyGestureHelper.GetKeyGesure(Key.S, ModifierKeys.Control | ModifierKeys.Shift) };

            CommandViewModel recentFilesCommand = new CommandViewModel("Recent files") { IsSubItem = true, };
            CommandViewModel recentProjectsCommand = new CommandViewModel("Recent projects and solutions") { IsSubItem = true, };

            return new List<CommandViewModel>() {
                newCommand, openCommand, GetSeparator(), closeFile, closeSolution, GetSeparator(), save,
                saveAll, GetSeparator(), recentFilesCommand, recentProjectsCommand };
        }
        CommandViewModel GetSeparator() {
            return new CommandViewModel() { IsSeparator = true };
        }
        protected override List<BarModel> CreateBars() {
            List<BarModel> bars = base.CreateBars();
            BarModel model = new BarModel("Standard");
            var commands = CreateToolbarCommands();
            foreach(var cmd in commands) {
                model.Commands.Add(cmd);
            }
            bars.Add(model);
            return bars;
        }
        List<CommandViewModel> CreateToolbarCommands() {
            var newProjectRelayCommand = DelegateCommandFactory.Create<object>(OnNewFileExecuted);
            var addNewItemRelayCommand = DelegateCommandFactory.Create<object>(OnNewFileExecuted);
            var openRelayCommand = DelegateCommandFactory.Create<object>(OnNewFileExecuted);

            CommandViewModel newProject = new CommandViewModel("New Project", newProjectRelayCommand) { Glyph = GlyphHelper.GetGlyph("/Images/VS2010Docking/NewProject_16x16.png"), KeyGesture = KeyGestureHelper.GetKeyGesure(Key.N, ModifierKeys.Control | ModifierKeys.Shift) };
            CommandViewModel newFile = new CommandViewModel("Add New Item", addNewItemRelayCommand) { Glyph = GlyphHelper.GetGlyph("/Images/VS2010Docking/File_16x16.png"), KeyGesture = KeyGestureHelper.GetKeyGesure(Key.N, ModifierKeys.Control) };
            CommandViewModel openFile = new CommandViewModel("Open File", openRelayCommand) { Glyph = GlyphHelper.GetGlyph("/Images/VS2010Docking/OpenFile_16x16.png"), KeyGesture = KeyGestureHelper.GetKeyGesure(Key.O, ModifierKeys.Control) };

            CommandViewModel save = new CommandViewModel("Save") { Glyph = GlyphHelper.GetGlyph("/Images/VS2010Docking/Save_16x16.png"), KeyGesture = KeyGestureHelper.GetKeyGesure(Key.S, ModifierKeys.Control) };
            CommandViewModel saveAll = new CommandViewModel("Save All") { Glyph = GlyphHelper.GetGlyph("/Images/VS2010Docking/SaveAll_16x16.png"), KeyGesture = KeyGestureHelper.GetKeyGesure(Key.S, ModifierKeys.Control | ModifierKeys.Shift) };

            CommandViewModel cut = new CommandViewModel("Cut") { Glyph = GlyphHelper.GetGlyph("/Images/VS2010Docking/Cut_16x16.png"), KeyGesture = KeyGestureHelper.GetKeyGesure(Key.S, ModifierKeys.Control | ModifierKeys.Shift) };
            CommandViewModel copy = new CommandViewModel("Copy") { Glyph = GlyphHelper.GetGlyph("/Images/VS2010Docking/Copy_16x16.png"), KeyGesture = KeyGestureHelper.GetKeyGesure(Key.S, ModifierKeys.Control | ModifierKeys.Shift) };
            CommandViewModel paste = new CommandViewModel("Paste") { Glyph = GlyphHelper.GetGlyph("/Images/VS2010Docking/Paste_16x16.png"), KeyGesture = KeyGestureHelper.GetKeyGesure(Key.S, ModifierKeys.Control | ModifierKeys.Shift) };

            CommandViewModel undo = new CommandViewModel("Undo") { Glyph = GlyphHelper.GetGlyph("/Images/VS2010Docking/Undo_16x16.png"), KeyGesture = KeyGestureHelper.GetKeyGesure(Key.S, ModifierKeys.Control | ModifierKeys.Shift) };
            CommandViewModel redo = new CommandViewModel("Redo") { Glyph = GlyphHelper.GetGlyph("/Images/VS2010Docking/Redo_16x16.png"), KeyGesture = KeyGestureHelper.GetKeyGesure(Key.S, ModifierKeys.Control | ModifierKeys.Shift) };

            return new List<CommandViewModel>() {
                newProject, newFile, openFile, save, saveAll, GetSeparator(), cut, copy, paste, GetSeparator(), undo, redo };
        }
        void OnNewFileExecuted(object param) {
            var document = CreateDocumentViewModel();
            document.Open();
            OpenOrCloseWorkspace(document, true);
        }
        void OnCloseFileExecuted(object param) { }
        static class KeyGestureHelper {
            public static KeyGesture GetKeyGesure(Key key, ModifierKeys modifiers) {
                KeyGesture k = new KeyGesture(key, modifiers);
                string s = k.GetDisplayStringForCulture(System.Globalization.CultureInfo.InvariantCulture);
                return new KeyGesture(key, modifiers);
            }
        }
    }
}
