using DevExpress.Mvvm.UI;
using DevExpress.Xpf;
using DevExpress.Xpf.Core;
namespace BarsDemo {
    public interface IShowAboutDialogService {
        void Show();
    }
    public class ShowAboutDialogService : ServiceBase, IShowAboutDialogService {
        public void Show() {
            string platformName = "WPF";
            AboutToolInfo ati = new AboutToolInfo();
            ati.LicenseState = LicenseState.Licensed;
            ati.ProductDescriptionLine1 = "A demo application that demonstrates the Toolbar and Menu system providing commands for a simple text editor.";
            ati.ProductDescriptionLine2 = "In this demo, the DevExpress Toolbar and Menu library is used to implement text editing commands in a simple text editor.";
            ati.ProductDescriptionLine3 = "Practice using bar commands to control the appearance of the editor's text. To learn more about DevExpress visit:";
            ati.ProductEULA = "DevExpress";
            ati.ProductEULAUri = "http://www.devexpress.com/";
            ati.ProductName = "DXBars for " + platformName;
            ati.Version = AssemblyInfo.MarketingVersion;

            DevExpress.Xpf.ToolAbout tAbout = new ToolAbout(ati);
            AboutWindow aWindow = new AboutWindow();
            aWindow.Content = tAbout;
            aWindow.ShowDialog();
        }
    }
}
