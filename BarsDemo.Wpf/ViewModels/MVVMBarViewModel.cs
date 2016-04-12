using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using DevExpress.Mvvm;
using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Mvvm.POCO;

namespace BarsDemo {
    public class MVVMBarViewModel {
        public IMessageBoxService MessageBoxService { get { return this.GetService<IMessageBoxService>(); } }
        public MVVMBarViewModel() {
            Bars = new ObservableCollection<BarViewModel>();
            SelectedText = string.Empty;
            Text = string.Empty;
            InitializeClipboardBar();
            InitializeAdditionBar();
        }
        void InitializeAdditionBar() {
            BarViewModel addingBar = ViewModelSource.Create(() => new BarViewModel() { Name = "Addition" });
            Bars.Add(addingBar);
            var addGroupCommand = ViewModelSource.Create(() => new GroupBarButtonInfo() { Caption = "Add", LargeGlyph = DXImageHelper.GetDXImage("Add_32x32.png"), SmallGlyph = DXImageHelper.GetDXImage("Add_16x16.png") });
            var parentCommand = ViewModelSource.Create(() => new ParentBarButtonInfo(this, MyParentCommandType.CommandCreation) { Caption = "Add Command", LargeGlyph = DXImageHelper.GetDXImage("Add_32x32.png"), SmallGlyph = DXImageHelper.GetDXImage("Add_16x16.png") });
            var parentBar = ViewModelSource.Create(() => new ParentBarButtonInfo(this, MyParentCommandType.BarCreation) { Caption = "Add Bar", LargeGlyph = DXImageHelper.GetDXImage("Add_32x32.png"), SmallGlyph = DXImageHelper.GetDXImage("Add_16x16.png") });
            addGroupCommand.Commands.Add(parentCommand);
            addGroupCommand.Commands.Add(parentBar);
            addingBar.Commands.Add(addGroupCommand);
            addingBar.Commands.Add(parentCommand);
            addingBar.Commands.Add(parentBar);
        }
        void InitializeClipboardBar() {
            BarViewModel clipboardBar = ViewModelSource.Create(() => new BarViewModel() { Name = "Clipboard" });
            Bars.Add(clipboardBar);
            var cutCommand = ViewModelSource.Create(() => new BarButtonInfo(cutCommandExecuteFunc) { Caption = "Cut", LargeGlyph = DXImageHelper.GetDXImage("Cut_32x32.png"), SmallGlyph = DXImageHelper.GetDXImage("Cut_16x16.png") });
            var copyCommand = ViewModelSource.Create(() => new BarButtonInfo(copyCommandExecuteFunc) { Caption = "Copy", LargeGlyph = DXImageHelper.GetDXImage("Copy_32x32.png"), SmallGlyph = DXImageHelper.GetDXImage("Copy_16x16.png") });
            var pasteCommand = ViewModelSource.Create(() => new BarButtonInfo(pasteCommandExecuteFunc) { Caption = "Paste", LargeGlyph = DXImageHelper.GetDXImage("Paste_32x32.png"), SmallGlyph = DXImageHelper.GetDXImage("Paste_16x16.png") });
            var selectCommand = ViewModelSource.Create(() => new BarButtonInfo(selectAllCommandExecuteFunc) { Caption = "Select All", LargeGlyph = DXImageHelper.GetDXImage("SelectAll_32x32.png"), SmallGlyph = DXImageHelper.GetDXImage("SelectAll_16x16.png") });
            var blankCommand = ViewModelSource.Create(() => new BarButtonInfo(blankCommandExecuteFunc) { Caption = "Clear Page", LargeGlyph = DXImageHelper.GetDXImage("New_32x32.png"), SmallGlyph = DXImageHelper.GetDXImage("New_16x16.png") });
            clipboardBar.Commands.Add(cutCommand);
            clipboardBar.Commands.Add(copyCommand);
            clipboardBar.Commands.Add(pasteCommand);
            clipboardBar.Commands.Add(selectCommand);
            clipboardBar.Commands.Add(blankCommand);
        }
        public virtual ObservableCollection<BarViewModel> Bars { get; set; }
        public virtual string Text { get; set; }
        public virtual string SelectedText { get; set; }
        public virtual int SelectionStart { get; set; }
        public virtual int SelectionLength { get; set; }
        #region CommandFuncs
        public void cutCommandExecuteFunc() {
            OnCopyExecute();
            SelectedText = String.Empty;
        }
        public void copyCommandExecuteFunc() {
            OnCopyExecute();
        }

        public void pasteCommandExecuteFunc() {
            SelectedText = Clipboard.GetText();
            SelectionStart += SelectionLength;
            SelectionLength = 0;
        }
        public void selectAllCommandExecuteFunc() {
            SelectionStart = 0;
            SelectionLength = string.IsNullOrEmpty(Text) ?  0 : Text.Count();
        }
        public void blankCommandExecuteFunc() {
            Text = string.Empty;
        }
        #endregion

        void OnCopyExecute() {
            Clipboard.SetData(DataFormats.Text, (Object)SelectedText);
        }
    }
    [POCOViewModel]
    public class BarViewModel {
        public BarViewModel() {
            Name = "";
            Commands = new ObservableCollection<BarButtonInfo>();
        }
        public virtual string Name { get; set; }
        public virtual ObservableCollection<BarButtonInfo> Commands { get; set; }
    }
}
