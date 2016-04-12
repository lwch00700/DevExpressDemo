using System;
using System.IO;
using System.Windows;
using System.Diagnostics;
using DevExpress.Xpf.Core;

using System.Windows.Forms;
using DevExpress.DemoData.Helpers;
using System.Reflection;


namespace SpreadsheetDemo {
    public static class DocumentLoadHelper {
        public static Stream GetDocument(string path) {
            var assembly = Assembly.GetExecutingAssembly();
            return assembly.GetManifestResourceStream(path);
        }
    }
    #region DemoUtils
    public class DemoUtils {
        public static string GetRelativePath(string name) {
            name = "Data\\" + name;
            string path = DataFilesHelper.DataDirectory;
            string s = "\\";
            for (int i = 0; i <= 10; i++) {
                if (System.IO.File.Exists(path + s + name))
                    return System.IO.Path.GetFullPath(path + s + name);
                else
                    s += "..\\";
            }
            return "";
        }

        public static string GetRelativeDirectoryPath(string name) {
            name = "Data\\" + name;
            string path = DataFilesHelper.DataDirectory;
            string s = "\\";
            for (int i = 0; i <= 10; i++) {
                if (System.IO.Directory.Exists(path + s + name))
                    return (path + s + name);
                else
                    s += "..\\";
            }
            return "";
        }

        public static Stream GetDataStream(string fileName) {
            string path = DemoUtils.GetRelativePath(fileName);
            if (!String.IsNullOrEmpty(path)) {
                FileAccess fileAccess = (File.GetAttributes(path) & FileAttributes.ReadOnly) != 0 ? FileAccess.Read : FileAccess.ReadWrite;
                return new FileStream(path, FileMode.Open, fileAccess);
            }
            return null;
        }
    }
    #endregion
    #region DialogHelper
    public class DialogHelper {
        public static MessageBoxResult ShowQuestionDialog(string message) {
            return DXMessageBox.Show(message, System.Windows.Forms.Application.ProductName, MessageBoxButton.OKCancel, MessageBoxImage.Question);
        }
        public static void ShowErrorDialog(string message) {
            DXMessageBox.Show(message, System.Windows.Forms.Application.ProductName, MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
    #endregion
    #region FileOpenHelper
    public class FileOpenHelper {
        public static void ShowFile(string fileName) {
            if (!File.Exists(fileName))
                return;

            if (DialogHelper.ShowQuestionDialog("Do you want to open the resulting file?") == MessageBoxResult.OK)
                Process.Start(fileName);
        }
    }
    #endregion
    #region FileSaveHelper
    public class FileSaveHelper {
        public static string GetSaveFileName(string filter) {
            SaveFileDialog sfDialog = new SaveFileDialog();
            sfDialog.Filter = filter;
            sfDialog.FileName = "Book1";
            if (sfDialog.ShowDialog() != DialogResult.OK)
                return String.Empty;
            return sfDialog.FileName;
        }
    }
    #endregion
}
