using DevExpress.Utils;
using DevExpress.Xpf.DemoBase.Helpers;
using DevExpress.Mvvm;
using System.Collections.Generic;
using DevExpress.DemoData;
using DevExpress.DemoData.Models;

namespace GridDemo {
    public class PrintMasterDetailModuleViewModel : BindableBase {
        DefaultBoolean allowPrintDetailsCore, printAllDetailsCore;
        bool printOrdersColumnHeadersCore, printOrdersSummariesCore, printCustomersColumnHeadersCore,
             printCustomersSummariesCore, printInvoicesColumnHeadersCore, printInvoicesSummariesCore,
             printEmployeesColumnHeadersCore, printEmployeesSummariesCore,
             printSelectedEmployeesOnlyCore, printSelectedOrdersOnlyCore, printSelectedInvoicesOnlyCore, printSelectedCustomersOnlyCore;

        public PrintMasterDetailModuleViewModel() {
            Employees = new NWindDataLoader().Employees;
            AllowPrintDetails = DefaultBoolean.True;
            PrintAllDetails = DefaultBoolean.False;
            PrintOrdersSummaries = true;
            PrintOrdersColumnHeaders = true;
            PrintCustomersColumnHeaders = true;
            PrintCustomersSummaries = true;
            PrintInvoicesColumnHeaders = true;
            PrintInvoicesSummaries = true;
            PrintEmployeesColumnHeaders = true;
            PrintEmployeesSummaries = true;
        }

        public object Employees { get; private set; }

        public DefaultBoolean AllowPrintDetails {
            get { return allowPrintDetailsCore; }
            set { SetProperty(ref allowPrintDetailsCore, value, () => AllowPrintDetails); }
        }
        public DefaultBoolean PrintAllDetails {
            get { return printAllDetailsCore; }
            set { SetProperty(ref printAllDetailsCore, value, () => PrintAllDetails); }
        }


        public bool PrintOrdersColumnHeaders {
            get { return printOrdersColumnHeadersCore; }
            set { SetProperty(ref printOrdersColumnHeadersCore, value, () => PrintOrdersColumnHeaders); }
        }
        public bool PrintOrdersSummaries {
            get { return printOrdersSummariesCore; }
            set { SetProperty(ref printOrdersSummariesCore, value, () => PrintOrdersSummaries); }
        }

        public bool PrintCustomersColumnHeaders {
            get { return printCustomersColumnHeadersCore; }
            set { SetProperty(ref printCustomersColumnHeadersCore, value, () => PrintCustomersColumnHeaders); }
        }
        public bool PrintCustomersSummaries {
            get { return printCustomersSummariesCore; }
            set { SetProperty(ref printCustomersSummariesCore, value, () => PrintCustomersSummaries); }
        }

        public bool PrintInvoicesColumnHeaders {
            get { return printInvoicesColumnHeadersCore; }
            set { SetProperty(ref printInvoicesColumnHeadersCore, value, () => PrintInvoicesColumnHeaders); }
        }

        public bool PrintInvoicesSummaries {
            get { return printInvoicesSummariesCore; }
            set { SetProperty(ref printInvoicesSummariesCore, value, () => PrintInvoicesSummaries); }
        }

        public bool PrintEmployeesColumnHeaders {
            get { return printEmployeesColumnHeadersCore; }
            set { SetProperty(ref printEmployeesColumnHeadersCore, value, () => PrintEmployeesColumnHeaders); }
        }

        public bool PrintEmployeesSummaries {
            get { return printEmployeesSummariesCore; }
            set { SetProperty(ref printEmployeesSummariesCore, value, () => PrintEmployeesSummaries); }
        }

        public bool PrintSelectedEmployeesOnly {
            get { return printSelectedEmployeesOnlyCore; }
            set { SetProperty(ref printSelectedEmployeesOnlyCore, value, () => PrintSelectedEmployeesOnly); }
        }

        public bool PrintSelectedOrdersOnly {
            get { return printSelectedOrdersOnlyCore; }
            set { SetProperty(ref printSelectedOrdersOnlyCore, value, () => PrintSelectedOrdersOnly); }
        }

        public bool PrintSelectedInvoicesOnly {
            get { return printSelectedInvoicesOnlyCore; }
            set { SetProperty(ref printSelectedInvoicesOnlyCore, value, () => PrintSelectedInvoicesOnly); }
        }

        public bool PrintSelectedCustomersOnly {
            get { return printSelectedCustomersOnlyCore; }
            set { SetProperty(ref printSelectedCustomersOnlyCore, value, () => PrintSelectedCustomersOnly); }
        }
    }
}
