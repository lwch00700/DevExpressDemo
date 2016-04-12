using System;
using System.IO;
using System.Reflection;
using System.Windows;
using DevExpress.DemoData.Helpers;
using DevExpress.Xpf.Core;
using DevExpress.Xpf.DemoBase;
using DevExpress.Xpf.DemoBase.Helpers;
using DevExpress.Xpf.RichEdit;
using DevExpress.XtraRichEdit;

namespace RichEditDemo {
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
        public static Stream GetDataStream(string fileName) {
            string path = DemoUtils.GetRelativePath(fileName);
            if (!String.IsNullOrEmpty(path)) {
                FileAccess fileAccess = (File.GetAttributes(path) & FileAttributes.ReadOnly) != 0 ? FileAccess.Read : FileAccess.ReadWrite;
                return new FileStream(path, FileMode.Open, fileAccess);
            }
            return null;
        }
    }
    public class RtfLoadHelper {
        public static void Load(String fileName, RichEditControl richEditControl) {
            string path = DemoUtils.GetRelativePath(fileName);
            if (!String.IsNullOrEmpty(path))
                richEditControl.LoadDocument(path, DocumentFormat.Rtf);
        }
    }
    public class DocLoadHelper {
        public static void Load(String fileName, RichEditControl richEditControl) {
            string path = DemoUtils.GetRelativePath(fileName);
            if (!String.IsNullOrEmpty(path))
                richEditControl.LoadDocument(path, DocumentFormat.Doc);
        }
    }
    public class HtmlLoadHelper {
        public static void Load(String fileName, RichEditControl richEditControl) {
            string path = DemoUtils.GetRelativePath(fileName);
            if (!String.IsNullOrEmpty(path))
                richEditControl.LoadDocument(path, DocumentFormat.Html);
        }
    }
    public class OpenXmlLoadHelper {
        public static void Load(String fileName, RichEditControl richEditControl) {
            string path = DemoUtils.GetRelativePath(fileName);
            if (!String.IsNullOrEmpty(path))
                richEditControl.LoadDocument(path, DocumentFormat.OpenXml);
        }
    }
    public class PlainTextLoadHelper {
        public static void Load(String fileName, RichEditControl richEditControl) {
            string path = DemoUtils.GetRelativePath(fileName);
            if (!String.IsNullOrEmpty(path))
                richEditControl.LoadDocument(path, DocumentFormat.PlainText);
        }
    }
    public class CodeFileLoadHelper {
        public static void Load(string moduleName, RichEditControl richEditControl) {
            Stream stream = DemoHelper.GetCodeTextStream(typeof(CodeFileLoadHelper).Assembly, GetCodeFileName(moduleName));
            if (stream != null)
                richEditControl.LoadDocument(stream, DocumentFormat.PlainText);
        }
        public static string GetCodeFileName(String moduleName) {
            return String.Format("{0}.xaml{1}", moduleName, DemoHelper.GetCodeSuffix(typeof(CodeFileLoadHelper).Assembly));
        }
    }
    public class RichEditDemoExceptionsHandler {
        readonly RichEditControl control;
        public RichEditDemoExceptionsHandler(RichEditControl control) {
            this.control = control;
        }
        public void Install() {
            if (control != null)
                control.UnhandledException += OnRichEditControlUnhandledException;
        }

        void OnRichEditControlUnhandledException(object sender, RichEditUnhandledExceptionEventArgs e) {
            try {
                if (e.Exception != null)
                    throw e.Exception;
            }
            catch (RichEditUnsupportedFormatException ex) {
                ShowMessage(ex.Message);
                e.Handled = true;
            }
            catch (System.Runtime.InteropServices.ExternalException ex) {
                ShowMessage(ex.Message);
                e.Handled = true;
            }
            catch (System.IO.IOException ex) {
                ShowMessage(ex.Message);
                e.Handled = true;
            }
            catch (System.InvalidOperationException ex) {
                if (ex.Message == DevExpress.XtraRichEdit.Localization.XtraRichEditLocalizer.GetString(DevExpress.XtraRichEdit.Localization.XtraRichEditStringId.Msg_MagicNumberNotFound) ||
                    ex.Message == DevExpress.XtraRichEdit.Localization.XtraRichEditLocalizer.GetString(DevExpress.XtraRichEdit.Localization.XtraRichEditStringId.Msg_UnsupportedDocVersion)) {
                    ShowMessage(ex.Message);
                    e.Handled = true;
                }
                else throw ex;
            }
            catch (SystemException ex) {
                ShowMessage(ex.Message);
                e.Handled = true;
            }
        }
        void ShowMessage(string message) {
            DXMessageBox.Show(message, System.Windows.Forms.Application.ProductName, MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}
