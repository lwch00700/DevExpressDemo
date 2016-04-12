using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.ComponentModel;
using System.Diagnostics;
using DevExpress.Xpf.Docking;
using System.Windows.Input;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Microsoft.Win32;
using System.IO;
using System.Windows.Controls;
using DevExpress.Mvvm;
using DevExpress.Mvvm.Native;

namespace DockingDemo.ViewModels {
    public abstract class ViewModel : BindableBase, IDisposable {
        protected ViewModel() {
        }
        public virtual string DisplayName { get; protected set; }
        public virtual ImageSource Glyph { get; set; }
        #region IDisposable Members
        public void Dispose() {
            this.OnDispose();
        }
        protected virtual void OnDispose() {
        }
#if DEBUG
        ~ViewModel() {
            string msg = string.Format("{0} ({1}) ({2}) Finalized", this.GetType().Name, this.DisplayName, this.GetHashCode());
            System.Diagnostics.Debug.WriteLine(msg);
        }
#endif
        #endregion //IDisposable Members
    }
    public abstract class WorkspaceViewModel : ViewModel {
        protected WorkspaceViewModel() {
            IsClosed = true;
        }
        public bool IsClosed {
            get { return GetProperty(() => IsClosed); }
            set { SetProperty(() => IsClosed, value, OnIsClosedChanged); }
        }
        public bool IsOpened {
            get { return GetProperty(() => IsOpened); }
            set { SetProperty(() => IsOpened, value); }
        }
        public bool IsActive {
            get { return GetProperty(() => IsActive); }
            set { SetProperty(() => IsActive, value); }
        }
        ICommand _closeCommand;
        public ICommand CloseCommand {
            get {
                if(_closeCommand == null)
                    _closeCommand = DelegateCommandFactory.Create<object>(OnRequestClose);
                return _closeCommand;
            }
        }
        public event EventHandler RequestClose;
        void OnRequestClose(object param) {
            EventHandler handler = this.RequestClose;
            if(handler != null)
                handler(this, EventArgs.Empty);
        }
        protected virtual void OnIsClosedChanged() {
            IsOpened = !IsClosed;
        }
    }
    public class CommandViewModel : ViewModel {
        public CommandViewModel() { }
        public CommandViewModel(string displayName, List<CommandViewModel> subCommands)
            : this(displayName, null, null, subCommands) {
        }
        public CommandViewModel(string displayName, ICommand command = null)
            : this(displayName, null, command, null) {
        }
        public CommandViewModel(WorkspaceViewModel owner, ICommand command)
            : this(string.Empty, owner, command) {
        }
        private CommandViewModel(string displayName, WorkspaceViewModel owner = null, ICommand command = null, List<CommandViewModel> subCommands = null) {
            IsEnabled = true;
            Owner = owner;
            if(Owner != null) {
                DisplayName = Owner.DisplayName;
                Glyph = Owner.Glyph;
            }
            else DisplayName = displayName;
            Command = command;
            Commands = subCommands;
        }
        public ICommand Command { get; private set; }
        public List<CommandViewModel> Commands { get; set; }
        public WorkspaceViewModel Owner { get; private set; }
        public bool IsEnabled { get; set; }
        public bool IsSubItem { get; set; }
        public bool IsSeparator { get; set; }
        public KeyGesture KeyGesture { get; set; }
    }
    abstract public class PanelWorkspaceViewModel : WorkspaceViewModel, IMVVMDockingProperties {
        abstract protected string WorkspaceName { get; }
        public PanelWorkspaceViewModel() {
            ((IMVVMDockingProperties)this).TargetName = WorkspaceName;
        }
        public PanelWorkspaceViewModel(bool supportPinnedState) {
            ShowPinButton = supportPinnedState;
        }
        string IMVVMDockingProperties.TargetName { get; set; }
        public bool Pinned {
            get { return GetProperty(() => Pinned); }
            set { SetProperty(() => Pinned, value, OnPinnedChanged); }
        }
        public bool ShowPinButton {
            get { return GetProperty(() => ShowPinButton); }
            set { SetProperty(() => ShowPinButton, value); }
        }
        public bool IsPreview {
            get { return GetProperty(() => IsPreview); }
            set { SetProperty(() => IsPreview, value); }
        }
        public virtual bool Open() { return false; }
        private void OnPinnedChanged() {
            if(IsPreview) {
                IsPreview = false;
            }
        }
    }
    public class ToolboxViewModel : PanelWorkspaceViewModel {
        protected override string WorkspaceName { get { return "Toolbox"; } }
        public ToolboxViewModel() {
            DisplayName = "Toolbox";
            Glyph = new BitmapImage(new Uri("/DockingDemo;component/Images/VS2010Docking/Toolbox_16x16.png", UriKind.Relative));
        }
    }
    public class SolutionExplorerViewModel : PanelWorkspaceViewModel {
        protected override string WorkspaceName { get { return "RightHost"; } }
        public SolutionExplorerViewModel() {
            DisplayName = "Solution Explorer";
            Glyph = new BitmapImage(new Uri("/DockingDemo;component/Images/VS2010Docking/Solution Explorer.png", UriKind.Relative));
        }
        public Solution Solution { get; set; }
    }
    public class PropertiesViewModel : PanelWorkspaceViewModel {
        protected override string WorkspaceName { get { return "RightHost"; } }
        public PropertiesViewModel() {
            DisplayName = "Properties";
            Glyph = new BitmapImage(new Uri("/DockingDemo;component/Images/VS2010Docking/PropertiesWindow_16x16.png", UriKind.Relative));
        }
    }
    public class ErrorListViewModel : PanelWorkspaceViewModel {
        protected override string WorkspaceName { get { return "BottomHost"; } }
        public ErrorListViewModel() {
            DisplayName = "Error List";
            Glyph = new BitmapImage(new Uri("/DockingDemo;component/Images/VS2010Docking/TaskList_16x16.png", UriKind.Relative));
        }
    }
    public class SearchResultsViewModel : PanelWorkspaceViewModel {
        protected override string WorkspaceName { get { return "BottomHost"; } }
        public SearchResultsViewModel() {
            DisplayName = "Search Results";
            Glyph = new BitmapImage(new Uri("/DockingDemo;component/Images/VS2010Docking/FindInFiles_16x16.png", UriKind.Relative));
        }
    }
    public class OutputViewModel : PanelWorkspaceViewModel {
        protected override string WorkspaceName { get { return "BottomHost"; } }
        public OutputViewModel() {
            DisplayName = "Output";
            Glyph = new BitmapImage(new Uri("/DockingDemo;component/Images/VS2010Docking/Output_16x16.png", UriKind.Relative));
        }
    }
    public class DocumentViewModel : PanelWorkspaceViewModel {
        protected override string WorkspaceName { get { return "DocumentHost"; } }
        public DocumentViewModel(string displayName, string text) : this() {
            DisplayName = displayName;
            Text = text;
        }
        public DocumentViewModel() {
            IsClosed = false;
        }
        public override bool Open() {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Filter = "Text Files (.txt)|*.txt|All Files (*.*)|*.*";
            openFileDialog1.FilterIndex = 1;
            bool? dialogResult = openFileDialog1.ShowDialog();
            bool dialogResultOK = dialogResult.HasValue && dialogResult.Value;
            if(dialogResultOK) {
                DisplayName = openFileDialog1.SafeFileName;
                System.IO.Stream fileStream = File.OpenRead(openFileDialog1.FileName);
                using(System.IO.StreamReader reader = new System.IO.StreamReader(fileStream)) {
                    Text = reader.ReadToEnd();
                }
                fileStream.Close();
            }
            return dialogResultOK;
        }
        public string Text { get; protected set; }
        public string Footer { get; protected set; }
        public string Description { get; protected set; }
    }
    public class BarModel : ViewModel {
        private IList<object> _Commands;
        public BarModel(string displayName)
            : this() {
                DisplayName = displayName;
        }
        public BarModel() {
            _Commands = new ObservableCollection<object>();
        }
        public IList<object> Commands {
            get { return _Commands; }
        }
        public bool IsMainMenu { get; set; }
    }
    public class MainWindowViewModel : BindableBase {
        ReadOnlyCollection<CommandViewModel> _commands;
        ObservableCollection<WorkspaceViewModel> _workspaces;
        const string DefaultText = "This example demonstrates how to use the ItemsSource collection for the DockLayoutManager object in order to apply an MVVM pattern for your application. All BarManager and DockLayoutManager items are defined in the data source. They are bound via the ItemsSource property and visualized via templates. Elements are added and organized in containers according to the attached TargetName property.";
        public MainWindowViewModel() {
            InitDefaultLayout();
        }
        protected virtual void InitDefaultLayout() {
            var panels = new List<PanelWorkspaceViewModel> { _ToolboxViewModel, _SolutionExplorerViewModel, _PropertiesViewModel, _ErrorListViewModel };
            foreach(var panel in panels) {
                OpenOrCloseWorkspace(panel);
            }
            OpenOrCloseWorkspace(new DocumentViewModel("Start Page", DefaultText), true);
        }
        public ReadOnlyCollection<CommandViewModel> Commands {
            get {
                if(_commands == null) {
                    List<CommandViewModel> cmds = this.CreateCommands();
                    _commands = new ReadOnlyCollection<CommandViewModel>(cmds);
                }
                return _commands;
            }
        }
        protected virtual List<CommandViewModel> CreateViewCommands() {
            var showToolboxPanelRelayCommand = DelegateCommandFactory.Create<object>(OnShowToolboxPanel);
            var showSolutionExplorerPanelRelayCommand = DelegateCommandFactory.Create<object>(OnShowSolutionExplorerPanel);
            var showPropertiesPanelRelayCommand = DelegateCommandFactory.Create<object>(OnShowPropertiesPanel);
            var showErrorListPanelRelayCommand = DelegateCommandFactory.Create<object>(OnShowErrorListPanel);
            var showOutputPanelRelayCommand = DelegateCommandFactory.Create<object>(OnShowOutputPanel);
            var showSearchResultsPanelRelayCommand = DelegateCommandFactory.Create<object>(OnShowSearchResultsPanel);
            CommandViewModel toolbox = new CommandViewModel(_ToolboxViewModel, showToolboxPanelRelayCommand);
            CommandViewModel solutionExplorer = new CommandViewModel(_SolutionExplorerViewModel, showSolutionExplorerPanelRelayCommand);
            CommandViewModel properties = new CommandViewModel(_PropertiesViewModel, showPropertiesPanelRelayCommand);
            CommandViewModel errorList = new CommandViewModel(_ErrorListViewModel, showErrorListPanelRelayCommand);
            CommandViewModel output = new CommandViewModel(_OutputViewModel, showOutputPanelRelayCommand);
            CommandViewModel searchResults = new CommandViewModel(_SearchResultsViewModel, showSearchResultsPanelRelayCommand);
            return new List<CommandViewModel>() { toolbox, solutionExplorer, properties, errorList, output, searchResults };
        }
        protected virtual List<CommandViewModel> CreateFileCommands() {
            var openDocumentRelayCommand = DelegateCommandFactory.Create<object>(OnOpenDocument);
            CommandViewModel openDocument = new CommandViewModel("Open...", openDocumentRelayCommand) { Glyph = GlyphHelper.GetGlyph("/Images/Open_16x16.png"), IsEnabled = true };
            return new List<CommandViewModel>() { openDocument };
        }
        List<CommandViewModel> CreateCommands() {
            List<CommandViewModel> fileCommands = CreateFileCommands();
            List<CommandViewModel> viewCommands = CreateViewCommands();
            var file = new CommandViewModel("File", fileCommands);
            var view = new CommandViewModel("View", viewCommands);
            return new List<CommandViewModel> { file, view };
        }
        ReadOnlyCollection<BarModel> _bars;
        public ReadOnlyCollection<BarModel> Bars {
            get {
                if(_bars == null) {
                    List<BarModel> cmds = CreateBars();
                    _bars = new ReadOnlyCollection<BarModel>(cmds);
                }
                return _bars;
            }
        }
        protected virtual List<BarModel> CreateBars() {
            BarModel model = new BarModel("Main") { IsMainMenu = true };
            var commands = CreateCommands();
            foreach(var cmd in commands) {
                model.Commands.Add(cmd);
            }
            return new List<BarModel>() { model };
        }

        public ObservableCollection<WorkspaceViewModel> Workspaces {
            get {
                if(_workspaces == null) {
                    _workspaces = new ObservableCollection<WorkspaceViewModel>();
                    _workspaces.CollectionChanged += this.OnWorkspacesChanged;
                }
                return _workspaces;
            }
        }

        void OnWorkspacesChanged(object sender, NotifyCollectionChangedEventArgs e) {
            if(e.NewItems != null && e.NewItems.Count != 0)
                foreach(WorkspaceViewModel workspace in e.NewItems)
                    workspace.RequestClose += this.OnWorkspaceRequestClose;

            if(e.OldItems != null && e.OldItems.Count != 0)
                foreach(WorkspaceViewModel workspace in e.OldItems)
                    workspace.RequestClose -= this.OnWorkspaceRequestClose;
        }

        void OnWorkspaceRequestClose(object sender, EventArgs e) {
            WorkspaceViewModel workspace = sender as WorkspaceViewModel;
            if(workspace is PanelWorkspaceViewModel) {
                ((PanelWorkspaceViewModel)workspace).IsClosed = true;
                if(workspace is DocumentViewModel) {
                    workspace.Dispose();
                    this.Workspaces.Remove(workspace);
                }
            }
        }
        protected virtual void InitPanelWorkspaceViewModel(PanelWorkspaceViewModel viewModel) { }
        protected T CreatePanelWorkspaceViewModel<T>() where T : PanelWorkspaceViewModel {
            T viewModel = Activator.CreateInstance<T>();
            InitPanelWorkspaceViewModel(viewModel);
            return viewModel;
        }
        ToolboxViewModel toolBoxViewModel;
        public ToolboxViewModel _ToolboxViewModel {
            get {
                if(toolBoxViewModel == null)
                    toolBoxViewModel = CreatePanelWorkspaceViewModel<ToolboxViewModel>();
                return toolBoxViewModel;
            }
        }
        SolutionExplorerViewModel solutionExplorerViewModel;
        public SolutionExplorerViewModel _SolutionExplorerViewModel {
            get {
                if(solutionExplorerViewModel == null) {
                    solutionExplorerViewModel = CreatePanelWorkspaceViewModel<SolutionExplorerViewModel>();
                    solutionExplorerViewModel.Solution = Solution;
                }
                return solutionExplorerViewModel;
            }
        }
        Solution solution;
        Solution Solution {
            get {
                if(solution == null) {
                    solution = new Solution();
                    solution.PropertyChanged += OnSolutionPropertyChanged;
                }
                return solution;
            }
        }
        private void OnSolutionPropertyChanged(object sender, PropertyChangedEventArgs e) {
            if(e.PropertyName == "PreviewItem") {
                OnPreviewItemChaged();
            }
        }
        protected virtual void OnPreviewItemChaged() { }
        PropertiesViewModel propertiesViewModel;
        public PropertiesViewModel _PropertiesViewModel {
            get {
                if(propertiesViewModel == null)
                    propertiesViewModel = CreatePanelWorkspaceViewModel<PropertiesViewModel>();
                return propertiesViewModel;
            }
        }
        ErrorListViewModel errorListViewModel;
        public ErrorListViewModel _ErrorListViewModel {
            get {
                if(errorListViewModel == null)
                    errorListViewModel = CreatePanelWorkspaceViewModel<ErrorListViewModel>();
                return errorListViewModel;
            }
        }
        OutputViewModel outputViewModel;
        public OutputViewModel _OutputViewModel {
            get {
                if(outputViewModel == null)
                    outputViewModel = CreatePanelWorkspaceViewModel<OutputViewModel>();
                return outputViewModel;
            }
        }
        SearchResultsViewModel searchResultsViewModel;
        public SearchResultsViewModel _SearchResultsViewModel {
            get {
                if(searchResultsViewModel == null)
                    searchResultsViewModel = CreatePanelWorkspaceViewModel<SearchResultsViewModel>();
                return searchResultsViewModel;
            }
        }
        protected void OpenOrCloseWorkspace(PanelWorkspaceViewModel workspace, bool activateOnOpen) {
            if(Workspaces.Contains(workspace)) {
                workspace.IsClosed = !workspace.IsClosed;
            }
            else {
                this.Workspaces.Add(workspace);
                workspace.IsClosed = false;
                if(activateOnOpen)
                    this.SetActiveWorkspace(workspace);
            }
        }
        protected void OpenOrCloseWorkspace(PanelWorkspaceViewModel workspace) {
            OpenOrCloseWorkspace(workspace, false);
        }
        void OnShowToolboxPanel(object param) {
            OpenOrCloseWorkspace(_ToolboxViewModel);
        }
        void OnShowSolutionExplorerPanel(object param) {
            OpenOrCloseWorkspace(_SolutionExplorerViewModel);
        }
        void OnShowPropertiesPanel(object param) {
            OpenOrCloseWorkspace(_PropertiesViewModel);
        }
        void OnShowErrorListPanel(object param) {
            OpenOrCloseWorkspace(_ErrorListViewModel);
        }
        void OnShowOutputPanel(object param) {
            OpenOrCloseWorkspace(_OutputViewModel);
        }
        void OnShowSearchResultsPanel(object param) {
            OpenOrCloseWorkspace(_SearchResultsViewModel);
        }
        protected virtual PanelWorkspaceViewModel CreateDocumentViewModel() {
            return CreatePanelWorkspaceViewModel<DocumentViewModel>();
        }
        void OnOpenDocument(object param) {
            var workspace = CreateDocumentViewModel();
            if(!workspace.Open()) return;
            OpenOrCloseWorkspace(workspace, true);
        }

        void SetActiveWorkspace(WorkspaceViewModel workspace) {
            Debug.Assert(this.Workspaces.Contains(workspace));
            workspace.IsActive = true;
        }
        protected static class GlyphHelper {
            public static BitmapImage GetGlyph(string path) {
                return new BitmapImage(DevExpress.Utils.AssemblyHelper.GetResourceUri(typeof(GlyphHelper).Assembly, path));
            }
        }
    }
    public class SolutionItem : DevExpress.Mvvm.BindableBase {
        readonly Solution solution;
        public SolutionItem(Solution solution) {
            this.solution = solution;
        }
        public string DisplayName {
            get { return GetProperty(() => DisplayName); }
            set { SetProperty(()=>DisplayName, value); }
        }
        public string GlyphPath {
            get { return GetProperty(() => GlyphPath); }
            set { SetProperty(() => GlyphPath, value); }
        }
        private List<SolutionItem> items = new List<SolutionItem>();
        public List<SolutionItem> Items {
            get { return items; }
        }
        public bool IsSelected {
            get { return GetProperty(() => IsSelected); }
            set {
                SetProperty(() => IsSelected, value, OnIsSelectedChanged);
                OnIsSelectedChanged();
            }
        }
        void OnIsSelectedChanged() {
            if(IsSelected)
                solution.PreviewItem = this;
        }
    }
    public class Solution : DevExpress.Mvvm.BindableBase {
        public Solution() {
            SolutionItem root = new SolutionItem(this) { GlyphPath = @"/DockingDemo;component/Images/VS2010Docking/Solution Explorer.png", DisplayName = "WPFApplication" };
            SolutionItem properties = new SolutionItem(this) { GlyphPath = @"/DockingDemo;component/Images/VS2010Docking/ReferencesClosed_16x16.png", DisplayName = "Properties" };
            SolutionItem references = new SolutionItem(this) { GlyphPath = @"/DockingDemo;component/Images/VS2010Docking/ReferencesOpened_16x16.png", DisplayName = "References" };
            SolutionItem file1 = new SolutionItem(this) { GlyphPath = @"/DockingDemo;component/Images/VS2010Docking/FileCS_16x16.png", DisplayName = "File1.cs" };
            SolutionItem file2 = new SolutionItem(this) { GlyphPath = @"/DockingDemo;component/Images/VS2010Docking/FileCS_16x16.png", DisplayName = "File2.cs" };
            SolutionItem file3 = new SolutionItem(this) { GlyphPath = @"/DockingDemo;component/Images/VS2010Docking/FileCS_16x16.png", DisplayName = "Program.cs" };

            root.Items.AddRange(new SolutionItem[] { properties, references, file1, file2, file3 });
            Items.Add(root);
        }
        private List<SolutionItem> items = new List<SolutionItem>();
        public List<SolutionItem> Items {
            get { return items; }
        }
        public SolutionItem PreviewItem {
            get { return GetProperty(() => PreviewItem); }
            set { SetProperty(() => PreviewItem, value); }
        }
    }
}
