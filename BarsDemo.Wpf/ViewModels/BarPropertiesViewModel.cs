using System.Diagnostics;

namespace BarsDemo.ViewModels {
    public class BarPropertiesViewModel {
        public virtual IShowAboutDialogService ShowAboutDialogService { get { return null; } }

        public void ShowAboutDialog() {
            ShowAboutDialogService.Show();
        }
        public void NavigateToHomePage() {
            Process.Start("http://www.devexpress.com");
        }
    }
}
