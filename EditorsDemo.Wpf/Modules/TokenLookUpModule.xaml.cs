using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using DevExpress.Xpf.DemoBase.DataClasses;

namespace EditorsDemo {

    public partial class TokenLookUpModule : EditorsDemoModule {
        public TokenLookUpModule() {
            InitializeComponent();
            DataContext = new TokenLookUpSource();
        }
    }
    public class TokenLookUpSource {
        CollectionViewViewModel viewModel = new CollectionViewViewModel();
        public object Source { get { return viewModel; } }

        List<object> selectedEmployees;
        public object SelectedEmployees {
            get {
                if (selectedEmployees == null)
                    selectedEmployees = CreateSelectedEmployees();
                return selectedEmployees;
            }
        }
        List<object> selectedEmployees2;
        public object SelectedEmployees2 {
            get {
                if (selectedEmployees2 == null)
                    selectedEmployees2 = CreateSelectedEmployees2();
                return selectedEmployees2;
            }
        }
        List<object> selectedEmployees3;
        public object SelectedEmployees3 {
            get {
                if (selectedEmployees3 == null)
                    selectedEmployees3 = CreateSelectedCountries3();
                return selectedEmployees3;
            }
        }

        private List<object> CreateSelectedCountries3() {
            return new List<object>() {
                viewModel.Employees[0],
                viewModel.Employees[1],
                viewModel.Employees[12],
                viewModel.Employees[5],
                viewModel.Employees[7],
                viewModel.Employees[3],
                viewModel.Employees[10],
                viewModel.Employees[15],
                viewModel.Employees[21],
                viewModel.Employees[25],
                viewModel.Employees[29],
                viewModel.Employees[30],
                viewModel.Employees[40],
                viewModel.Employees[22],
                viewModel.Employees[54],
                viewModel.Employees[20],
                viewModel.Employees[31],
                viewModel.Employees[37],
                viewModel.Employees[43],
                viewModel.Employees[49],
                viewModel.Employees[4],
                viewModel.Employees[6],
                viewModel.Employees[11],
                viewModel.Employees[33],
                viewModel.Employees[32],
                viewModel.Employees[19],
                viewModel.Employees[14],
                viewModel.Employees[23],
                viewModel.Employees[27],
                viewModel.Employees[38],
            };
        }

        private List<object> CreateSelectedEmployees2() {
            return new List<object>() {
                viewModel.Employees[0],
                viewModel.Employees[1],
                viewModel.Employees[12],
                viewModel.Employees[5],
                viewModel.Employees[7],
                viewModel.Employees[3]
            };
        }
        private List<object> CreateSelectedEmployees() {
            return new List<object>() {
                viewModel.Employees[7],
                viewModel.Employees[3],
                viewModel.Employees[10]
            };
        }
    }
}
