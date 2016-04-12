using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using XmlaMetaGetter = DevExpress.XtraPivotGrid.Data.OLAPMetaGetter;
using DevExpress.Xpf.Editors;
using DevExpress.XtraPivotGrid;
using DevExpress.Xpf.Core;

namespace PivotGridDemo.PivotGrid {

    public partial class DataSourceDialog : Control {
        const string ConnectionStringName = "ConnectionString",
            CatalogsComboName = "CatalogsCombo",
            CubesComboName = "CubesCombo",
            UserName= "UserCombo",
            PasswordName = "PasswordCombo",
            ConnectButtonName = "Connect";

        public static readonly DependencyProperty IsCatalogsRetrivingProperty;
        public static readonly DependencyProperty IsCubesRetrivingProperty;
        XmlaMetaGetter metaGetter = new XmlaMetaGetter();

        static DataSourceDialog() {
            Type ownerType = typeof(DataSourceDialog);
            IsCatalogsRetrivingProperty = DependencyProperty.Register("IsCatalogsRetriving", typeof(bool), ownerType, new PropertyMetadata(false));
            IsCubesRetrivingProperty = DependencyProperty.Register("IsCubesRetriving", typeof(bool), ownerType, new PropertyMetadata(false));
        }

        public bool IsCatalogsRetriving {
            get { return (bool)GetValue(IsCatalogsRetrivingProperty); }
            set { SetValue(IsCatalogsRetrivingProperty, value); }
        }

        public bool IsCubesRetriving {
            get { return (bool)GetValue(IsCubesRetrivingProperty); }
            set { SetValue(IsCubesRetrivingProperty, value); }
        }

        ComboBoxEdit CatalogsCombo { get; set; }
        ComboBoxEdit CubesCombo { get; set; }
        TextEdit ConnectionString { get; set; }
        TextEdit User { get; set; }
        PasswordBoxEdit Password { get; set; }
        Button ConnectButton { get; set; }

        public override void OnApplyTemplate() {
            base.OnApplyTemplate();
            CatalogsCombo = GetTemplateChild(CatalogsComboName) as ComboBoxEdit;
            CatalogsCombo.EditValueChanged += OnCatalogsComboEditValueChanged;
            CubesCombo = GetTemplateChild(CubesComboName) as ComboBoxEdit;
            ConnectionString = GetTemplateChild(ConnectionStringName) as TextEdit;
            User = GetTemplateChild(UserName) as TextEdit;
            Password = GetTemplateChild(PasswordName) as PasswordBoxEdit;
            ConnectButton = GetTemplateChild(ConnectButtonName) as Button;
            ConnectButton.Click += Connect;
            ApplyPlatformTemplate();
        }

        void Connect(object sender, RoutedEventArgs e) {
            ClearCombo(CatalogsCombo);
            ClearCombo(CubesCombo);
            IsCatalogsRetriving = true;
            IsCubesRetriving = false;
            metaGetter.ConnectionString = GetConnectionStringCore();
            if(!metaGetter.Connected) {
                ShowMessage("Invalid cube.");
                IsCatalogsRetriving = false;
                return;
            }
            RetriveCatalogsAndCubes();
        }

        void OnCatalogsComboEditValueChanged(object sender, EditValueChangedEventArgs e) {
            IsCatalogsRetriving = false;
            if(IsCatalogEmpty()) {
                IsCubesRetriving = false;
                return;
            }
            IsCubesRetriving = true;
            CubesCombo.Clear();
            metaGetter.ConnectionString = GetConnectionStringCore();
            RetriveCubes();
        }

        bool CatalogOrCubeEmpty() {
            if(IsCatalogEmpty())
                return true;
            return CubesCombo.SelectedIndex < 0 || string.IsNullOrEmpty(CubesCombo.Items[CubesCombo.SelectedIndex] as string);
        }
        bool IsCatalogEmpty() {
            return CatalogsCombo.SelectedIndex < 0 || string.IsNullOrEmpty(CatalogsCombo.Items[CatalogsCombo.SelectedIndex] as string);
        }
        void ClearCombo(ComboBoxEdit edit) {
            edit.Items.Clear();
            edit.EditValue = string.Empty;
        }
    }
}
