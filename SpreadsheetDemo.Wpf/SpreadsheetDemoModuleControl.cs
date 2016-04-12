using System;
using DevExpress.Spreadsheet;
using DevExpress.Xpf.DemoBase;
using System.IO;
using System.Globalization;
using DevExpress.XtraSpreadsheet.API.Native.Implementation;

namespace SpreadsheetDemo {
    public class SpreadsheetDemoModule : DemoModule {
        CultureInfo defaultCulture = new CultureInfo("en-US");

        public CultureInfo DefaultCulture { get { return defaultCulture; } }
    }
}
