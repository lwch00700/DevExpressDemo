using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Input;
using DevExpress.Xpf.Grid;
using DevExpress.Xpf.DemoBase.Helpers;
using DevExpress.Xpf.Utils;
using System.Collections;
using DevExpress.Xpf.DemoBase.DataClasses;
using DevExpress.Mvvm;
using DevExpress.DemoData;

namespace GridDemo {
    public class MultiViewViewItem {
        public object Content { get; set; }
        public string DisplayName { get; set; }
    }

    public abstract class MultiViewViewModelBase : BindableBase {
        public object Employees { get; private set; }

        string columnInfoFieldName = "HomePhone";
        public string ColumnInfoFieldName {
            get { return columnInfoFieldName; }
            set { SetProperty(ref columnInfoFieldName, value, () => ColumnInfoFieldName); }
        }

        public MultiViewViewModelBase() {
            Employees = new NWindDataLoader().Employees;
            ChangeFieldNameCommand = new DelegateCommand<object>(OnChangeFieldName);
        }
        public DelegateCommand<object> ChangeFieldNameCommand { get; private set; }
        bool CanExecuteCommand {
            get { return true; }
        }
        void OnChangeFieldName(object param) {
            ColumnInfoFieldName = (string)param;
        }
    }
    public class MultiViewTableViewViewModel : MultiViewViewModelBase {
    }
    public class MultiViewTreeListViewViewModel : MultiViewViewModelBase {
    }
    public class MultiViewCardViewViewModel : MultiViewViewModelBase {
    }
    public class MultiViewBandedTableViewViewModel : MultiViewViewModelBase {
    }
    public class MultiViewBandedTreeListViewViewModel : MultiViewViewModelBase {
    }
}
