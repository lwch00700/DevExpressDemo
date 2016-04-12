using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Windows.Media.Imaging;
using System.Xml.Serialization;
using DevExpress.Utils;
using DevExpress.Xpf.DemoBase.Helpers;
using DevExpress.Xpf.Grid;

namespace TreeListDemo {
    public partial class DynamicNodeLoading : TreeListDemoModule {

        public DynamicNodeLoading() {
            InitializeComponent();
            Helper = new FileSystemHelper();
            InitDrives();
        }

        FileSystemDataProviderBase Helper { get; set; }

        public void InitDrives() {
            treeList.BeginDataUpdate();
            try {
                string[] root = Helper.GetLogicalDrives();

                foreach(string s in root) {
                    TreeListNode node = new TreeListNode() { Content = new FileSystemItem(s, "Drive", "<Drive>", s), Image = FileSystemImages.DiskImage };
                    view.Nodes.Add(node);
                    node.IsExpandButtonVisible = DefaultBoolean.True;
                    node.Tag = false;
                }
            }
            catch { }
            treeList.EndDataUpdate();
        }
        private void InitFolder(TreeListNode treeListNode) {
            treeList.BeginDataUpdate();
            InitFolders(treeListNode);
            InitFiles(treeListNode);
            treeList.EndDataUpdate();
        }

        private void InitFiles(TreeListNode treeListNode) {
            FileSystemItem item = treeListNode.Content as FileSystemItem;
            if(item == null) return;
            TreeListNode node;
            try {
                string[] root = Helper.GetFiles(item.FullName);
                foreach(string s in root) {
                    node = new TreeListNode() { Content = new FileSystemItem(Helper.GetFileName(s), "File", Helper.GetFileSize(s).ToString(), s), Image = FileSystemImages.FileImage };
                    node.IsExpandButtonVisible = DefaultBoolean.False;
                    treeListNode.Nodes.Add(node);
                }
            }
            catch { }
        }

        private void InitFolders(TreeListNode treeListNode) {
            FileSystemItem item = treeListNode.Content as FileSystemItem;
            if(item == null) return;

            try {
                string[] root = Helper.GetDirectories(item.FullName);
                foreach(string s in root) {
                    try {
                        TreeListNode node = new TreeListNode() { Content = new FileSystemItem(Helper.GetDirectoryName(s), "Folder", "<Folder>", s), Image = FileSystemImages.ClosedFolderImage };
                        treeListNode.Nodes.Add(node);
                        node.IsExpandButtonVisible = HasFiles(s) ? DefaultBoolean.True : DefaultBoolean.False;
                    }
                    catch { }
                }
            }
            catch {
                treeListNode.IsExpandButtonVisible = DefaultBoolean.False;
            }
        }

        private bool HasFiles(string path) {
            string[] root = Helper.GetFiles(path);
            if(root.Length > 0) return true;
            root = Helper.GetDirectories(path);
            if(root.Length > 0) return true;
            return false;
        }

        private void treeListView_NodeExpanding(object sender, DevExpress.Xpf.Grid.TreeList.TreeListNodeAllowEventArgs e) {
            TreeListNode node = e.Node;
            if (NodeIsFolder(node))
                node.Image = FileSystemImages.OpenedFolderImage;
            if(node.Tag == null || (bool)node.Tag == false) {
                InitFolder(node);
                node.Tag = true;
            }
        }

        private void view_NodeCollapsing(object sender, DevExpress.Xpf.Grid.TreeList.TreeListNodeAllowEventArgs e) {
            TreeListNode node = e.Node;
            if (NodeIsFolder(node))
                node.Image = FileSystemImages.ClosedFolderImage;
        }

        bool NodeIsFolder(TreeListNode node) {
            return (node.Content as FileSystemItem).ItemType == "Folder";
        }
    }

    public abstract class FileSystemDataProviderBase {
        public abstract string[] GetLogicalDrives();
        public abstract string[] GetDirectories(string path);
        public abstract string[] GetFiles(string path);
        public abstract string GetDirectoryName(string path);
        public abstract string GetFileName(string path);
        public abstract string GetFileSize(string path);
        internal string GetFileSize(long size) {
            if (size >= 1024)
                return string.Format("{0:### ### ###} KB", size / 1024);
            return string.Format("{0} Bytes", size);
        }
    }

    public class FileSystemHelper : FileSystemDataProviderBase {

            public override string[] GetLogicalDrives() {
                return Directory.GetLogicalDrives();
            }

            public override string[] GetDirectories(string path) {
                return Directory.GetDirectories(path);
            }

            public override string[] GetFiles(string path) {
                return Directory.GetFiles(path);
            }

            public override string GetDirectoryName(string path) {
                return new DirectoryInfo(path).Name;
            }

            public override string GetFileName(string path) {
                return new FileInfo(path).Name;
            }

            public override string GetFileSize(string path) {
                long size = new FileInfo(path).Length;
                return GetFileSize(size);
            }
    }

    public class FileSystemItem {
        public FileSystemItem(string name, string type, string size, string fullName) {
            this.Name = name;
            this.ItemType = type;
            this.Size = size;
            this.FullName = fullName;
        }
        public string Name { get; set; }
        public string ItemType { get; set; }
        public string Size { get; set; }
        public string FullName { get; set; }
    }
        public class FileSystemImages {
        static BitmapImage fileImage;
        public static BitmapImage FileImage {
            get {
                if(fileImage == null)
                    fileImage = LoadImage("File");
                return fileImage;
            }
        }
        static BitmapImage diskImage;
        public static BitmapImage DiskImage {
            get {
                if(diskImage == null)
                    diskImage = LoadImage("Local_Disk");
                return diskImage;
            }
        }
        static BitmapImage closedFolderImage;
        public static BitmapImage ClosedFolderImage {
            get {
                if(closedFolderImage == null)
                    closedFolderImage = LoadImage("Folder_Closed");
                return closedFolderImage;
            }
        }
        static BitmapImage openedFolderImage;
        public static BitmapImage OpenedFolderImage {
            get {
                if(openedFolderImage == null)
                    openedFolderImage = LoadImage("Folder_Opened");
                return openedFolderImage;
            }
        }
        static BitmapImage LoadImage(string imageName) {
            return new BitmapImage(new Uri("/TreeListDemo;component/Images/" + imageName + ".png", UriKind.Relative));
        }
    }
}
