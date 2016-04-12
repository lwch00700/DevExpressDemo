using System;
using System.Windows.Data;
using DevExpress.Xpf.Grid;
using System.Data;
using DevExpress.DemoData.Models;

namespace GridDemo {
    public class CustomerDetailsConverter : IValueConverter {
        #region IValueConverter Members
        object IValueConverter.Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            RowData rowData = (RowData)value;
            if(rowData == null)
                return null;
            Customer row = (Customer)rowData.Row;
            string region = row.Region;
            return String.Format("{0}{1}, {2}, {3}\r\n{4}, {5}", region, row.Country, row.City, row.PostalCode, row.Address, row.Phone);
        }
        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            throw new NotImplementedException();
        }
        #endregion
    }
}
