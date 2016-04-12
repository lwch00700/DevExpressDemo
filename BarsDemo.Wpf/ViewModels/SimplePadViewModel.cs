﻿using DevExpress.Mvvm;
using System;
using System.Collections.Generic;
namespace BarsDemo {
    public class SimplePadViewModel {
        #region properties
        public IEnumerable<double?> FontSizes { get; protected set; }
        #endregion
        #region services
        public virtual IShowAboutDialogService ShowAboutDialogService { get { return null; } }
        #endregion
        #region commands
        public void ShowAboutDialog() {
            ShowAboutDialogService.Show();
        }
        public void NavigateToHomePage() {

            System.Diagnostics.Process.Start("http://www.devexpress.com");
        }
        #endregion
        public SimplePadViewModel() {
            FontSizes = new double?[] {null, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 22, 24, 26, 28, 30,
                    32, 34, 36, 38, 40, 44, 48, 52, 56, 60, 64, 68, 72, 76, 80, 88, 96, 104, 112, 120, 128, 136, 144
                };
        }
    }
}
