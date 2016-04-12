using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using DevExpress.XtraRichEdit.Commands;

namespace SpellCheckerDemo {
    public partial class ContainerWithControls : SpellCheckerDemoModule {
        public static readonly DependencyProperty EmployeeProperty;

        static ContainerWithControls() {
            EmployeeProperty = DependencyProperty.Register("Employee", typeof(Employees), typeof(ContainerWithControls), new PropertyMetadata(EmployeesData.DataSource[0]));
        }

        public ContainerWithControls() {
            InitializeComponent();
            this.txtAbout.SelectAllOnGotFocus = false;
        }

        public Employees Employee {
            get { return (Employees)GetValue(EmployeeProperty); }
            set { SetValue(EmployeeProperty, value); }
        }

        private void Button_Click(object sender, RoutedEventArgs e) {
            SpellChecker.CheckContainer(RootLayout);
        }
    }
}
