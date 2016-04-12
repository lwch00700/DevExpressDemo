using DevExpress.Mvvm.POCO;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using DevExpress.Mvvm.DataAnnotations;

namespace RibbonDemo {
    public class MVVMRibbonViewModel {
        public virtual string Text { get; set; }
        public virtual string SelectedText { get; set; }
        public virtual int SelectionStart { get; set; }
        public virtual int SelectionLength { get; set; }
        public virtual ObservableCollection<CategoryModel> Categories { get; set; }
        public virtual ObservableCollection<CommandModel> MenuItems { get; set; }

        public MVVMRibbonViewModel() {
            Categories = new ObservableCollection<CategoryModel>();
            MenuItems = new ObservableCollection<CommandModel>();
            SelectedText = string.Empty;
            PageModel homePage = ViewModelSource.Create(() => new PageModel() { Name = "Home" });
            CategoryModel category = ViewModelSource.Create<CategoryModel>();
            Categories.Add(category);
            category.Pages.Add(homePage);
            InitializeClipboardGroup(homePage);
            InitializeAdditionGroup(homePage);
        }

        void InitializeAdditionGroup(PageModel homePage) {
            PageGroupModel addingGroup = ViewModelSource.Create(() => new PageGroupModel() { Name = "Addition", Glyph = GlyphHelper.GetGlyph("/Images/Icons/Add_32x32.png") });
            homePage.Groups.Add(addingGroup);
            MyGroupCommand addGroupCommand = ViewModelSource.Create(() => new MyGroupCommand() { Caption = "Add", LargeGlyph = GlyphHelper.GetGlyph("/Images/Icons/Add_32x32.png"), SmallGlyph = GlyphHelper.GetGlyph("/Images/Icons/Add_16x16.png") });
            MyParentCommand parentCommand = ViewModelSource.Create(() => new MyParentCommand(this, MyParentCommandType.CommandCreation) { Caption = "Add New Command", LargeGlyph = GlyphHelper.GetGlyph("/Images/Icons/Add_32x32.png"), SmallGlyph = GlyphHelper.GetGlyph("/Images/Icons/Add_16x16.png") });
            MyParentCommand parentGroup = ViewModelSource.Create(() => new MyParentCommand(this, MyParentCommandType.GroupCreation) { Caption = "Add New Group", LargeGlyph = GlyphHelper.GetGlyph("/Images/Icons/Add_32x32.png"), SmallGlyph = GlyphHelper.GetGlyph("/Images/Icons/Add_16x16.png") });
            MyParentCommand parentPage = ViewModelSource.Create(() => new MyParentCommand(this, MyParentCommandType.PageCreation) { Caption = "Add New Page", LargeGlyph = GlyphHelper.GetGlyph("/Images/Icons/Add_32x32.png"), SmallGlyph = GlyphHelper.GetGlyph("/Images/Icons/Add_16x16.png") });
            addGroupCommand.Commands.Add(parentCommand);
            addGroupCommand.Commands.Add(parentGroup);
            addGroupCommand.Commands.Add(parentPage);
            addingGroup.Commands.Add(addGroupCommand);
            addingGroup.Commands.Add(parentCommand);
            addingGroup.Commands.Add(parentGroup);
            addingGroup.Commands.Add(parentPage);
            MenuItems.Add(parentCommand);
            MenuItems.Add(parentGroup);
            MenuItems.Add(parentPage);
        }
        void InitializeClipboardGroup(PageModel homePage) {
            PageGroupModel clipboardGroup = ViewModelSource.Create(() => new PageGroupModel() { Name = "Clipboard", Glyph = GlyphHelper.GetGlyph("/Images/Icons/paste-32x32.png") });
            homePage.Groups.Add(clipboardGroup);
            CommandModel cutCommand = ViewModelSource.Create(() => new CommandModel(cutCommandExecuteFunc) { Caption = "Cut", LargeGlyph = GlyphHelper.GetGlyph("/Images/Icons/cut-32x32.png"), SmallGlyph = GlyphHelper.GetGlyph("/Images/Icons/cut-16x16.png") });
            CommandModel copyCommand = ViewModelSource.Create(() => new CommandModel(copyCommandExecuteFunc) { Caption = "Copy", LargeGlyph = GlyphHelper.GetGlyph("/Images/Icons/copy-32x32.png"), SmallGlyph = GlyphHelper.GetGlyph("/Images/Icons/copy-16x16.png") });
            CommandModel pasteCommand = ViewModelSource.Create(() => new CommandModel(pasteCommandExecuteFunc) { Caption = "Paste", LargeGlyph = GlyphHelper.GetGlyph("/Images/Icons/paste-32x32.png"), SmallGlyph = GlyphHelper.GetGlyph("/Images/Icons/paste-16x16.png") });
            CommandModel selectCommand = ViewModelSource.Create(() => new CommandModel(selectAllCommandExecuteFunc) { Caption = "Select All", LargeGlyph = GlyphHelper.GetGlyph("/Images/Icons/SelectAll_32x32.png"), SmallGlyph = GlyphHelper.GetGlyph("/Images/Icons/SelectAll_16x16.png") });
            CommandModel blankCommand = ViewModelSource.Create(() => new CommandModel(blankCommandExecuteFunc) { Caption = "Clear Page", LargeGlyph = GlyphHelper.GetGlyph("/Images/Icons/new-32x32.png"), SmallGlyph = GlyphHelper.GetGlyph("/Images/Icons/new-16x16.png") });
            clipboardGroup.Commands.Add(cutCommand);
            clipboardGroup.Commands.Add(copyCommand);
            clipboardGroup.Commands.Add(pasteCommand);
            clipboardGroup.Commands.Add(selectCommand);
            clipboardGroup.Commands.Add(blankCommand);
        }
        public void Clear() {
            foreach(CategoryModel cat in Categories) {
                cat.Clear();
            }
            Categories.Clear();
        }
        public void cutCommandExecuteFunc() {
            Clipboard.SetText(SelectedText);
            SelectedText = String.Empty;
        }
        public void copyCommandExecuteFunc() {
            Clipboard.SetText(SelectedText);
        }
        public void pasteCommandExecuteFunc() {
            SelectedText = Clipboard.GetText();
            SelectionStart += SelectionLength;
            SelectionLength = 0;
        }
        public void selectAllCommandExecuteFunc() {
            SelectionStart = 0;
            SelectionLength = Text == null ? 0 : Text.Count();
        }
        public void blankCommandExecuteFunc() {
            Text = string.Empty;
        }
    }

    public class CategoryModel {
        public virtual string Name { get; set; }
        public virtual ObservableCollection<PageModel> Pages { get; set; }
        public CategoryModel() {
            Pages = new ObservableCollection<PageModel>();
            Name = "";
        }
        public void Clear() {
            foreach(PageModel cat in Pages) {
                cat.Clear();
            }
            Pages.Clear();
        }
    }
    [POCOViewModel]
    public class PageModel {
        public PageModel() {
            Groups = new ObservableCollection<PageGroupModel>();
        }
        public virtual string Name { get; set; }
        public virtual ObservableCollection<PageGroupModel> Groups { get; set; }
        public void Clear() {
            foreach(PageGroupModel cat in Groups) {
                cat.Clear();
            }
            Groups.Clear();
        }
    }

}
