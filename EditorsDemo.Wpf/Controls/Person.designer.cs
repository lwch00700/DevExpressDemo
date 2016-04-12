#pragma warning disable 1591

namespace GridDemo.Controls {
    using System.Data.Linq;
    using System.Data.Linq.Mapping;
    using System.Data;
    using System.Collections.Generic;
    using System.Reflection;
    using System.Linq;
    using System.Linq.Expressions;
    using System.ComponentModel;
    using System;


    [global::System.Data.Linq.Mapping.DatabaseAttribute(Name = "DXGridServerModeDB")]
    public partial class PersonDataContext : System.Data.Linq.DataContext {

        private static System.Data.Linq.Mapping.MappingSource mappingSource = new AttributeMappingSource();

        #region Extensibility Method Definitions
        partial void OnCreated();
        partial void InsertPerson(Person instance);
        partial void UpdatePerson(Person instance);
        partial void DeletePerson(Person instance);
        #endregion

        public PersonDataContext(string connection) :
                base(connection, mappingSource) {
            OnCreated();
        }

        public PersonDataContext(System.Data.IDbConnection connection) :
                base(connection, mappingSource) {
            OnCreated();
        }

        public PersonDataContext(string connection, System.Data.Linq.Mapping.MappingSource mappingSource) :
                base(connection, mappingSource) {
            OnCreated();
        }

        public PersonDataContext(System.Data.IDbConnection connection, System.Data.Linq.Mapping.MappingSource mappingSource) :
                base(connection, mappingSource) {
            OnCreated();
        }

        public System.Data.Linq.Table<Person> Person {
            get {
                return this.GetTable<Person>();
            }
        }
    }

    [global::System.Data.Linq.Mapping.TableAttribute(Name = "dbo.Person")]
    public partial class Person : INotifyPropertyChanging, INotifyPropertyChanged {

        private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);

        private int _PersonID;

        private string _FullName;

        private string _Company;

        private string _JobTitle;

        private string _City;

        private string _Address;

        private string _Phone;

        private string _Email;

        #region Extensibility Method Definitions
        partial void OnLoaded();
        partial void OnValidate(System.Data.Linq.ChangeAction action);
        partial void OnCreated();
        partial void OnPersonIDChanging(int value);
        partial void OnPersonIDChanged();
        partial void OnFullNameChanging(string value);
        partial void OnFullNameChanged();
        partial void OnCompanyChanging(string value);
        partial void OnCompanyChanged();
        partial void OnJobTitleChanging(string value);
        partial void OnJobTitleChanged();
        partial void OnCityChanging(string value);
        partial void OnCityChanged();
        partial void OnAddressChanging(string value);
        partial void OnAddressChanged();
        partial void OnPhoneChanging(string value);
        partial void OnPhoneChanged();
        partial void OnEmailChanging(string value);
        partial void OnEmailChanged();
        #endregion

        public Person() {
            OnCreated();
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_PersonID", AutoSync = AutoSync.OnInsert, DbType = "Int NOT NULL IDENTITY", IsPrimaryKey = true, IsDbGenerated = true)]
        public int PersonID {
            get {
                return this._PersonID;
            }
            set {
                if ((this._PersonID != value)) {
                    this.OnPersonIDChanging(value);
                    this.SendPropertyChanging();
                    this._PersonID = value;
                    this.SendPropertyChanged("PersonID");
                    this.OnPersonIDChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_FullName", DbType = "NVarChar(MAX)")]
        public string FullName {
            get {
                return this._FullName;
            }
            set {
                if ((this._FullName != value)) {
                    this.OnFullNameChanging(value);
                    this.SendPropertyChanging();
                    this._FullName = value;
                    this.SendPropertyChanged("FullName");
                    this.OnFullNameChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Company", DbType = "NVarChar(MAX)")]
        public string Company {
            get {
                return this._Company;
            }
            set {
                if ((this._Company != value)) {
                    this.OnCompanyChanging(value);
                    this.SendPropertyChanging();
                    this._Company = value;
                    this.SendPropertyChanged("Company");
                    this.OnCompanyChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_JobTitle", DbType = "NVarChar(MAX)")]
        public string JobTitle {
            get {
                return this._JobTitle;
            }
            set {
                if ((this._JobTitle != value)) {
                    this.OnJobTitleChanging(value);
                    this.SendPropertyChanging();
                    this._JobTitle = value;
                    this.SendPropertyChanged("JobTitle");
                    this.OnJobTitleChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_City", DbType = "NVarChar(MAX)")]
        public string City {
            get {
                return this._City;
            }
            set {
                if ((this._City != value)) {
                    this.OnCityChanging(value);
                    this.SendPropertyChanging();
                    this._City = value;
                    this.SendPropertyChanged("City");
                    this.OnCityChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Address", DbType = "NVarChar(MAX)")]
        public string Address {
            get {
                return this._Address;
            }
            set {
                if ((this._Address != value)) {
                    this.OnAddressChanging(value);
                    this.SendPropertyChanging();
                    this._Address = value;
                    this.SendPropertyChanged("Address");
                    this.OnAddressChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Phone", DbType = "NVarChar(MAX)")]
        public string Phone {
            get {
                return this._Phone;
            }
            set {
                if ((this._Phone != value)) {
                    this.OnPhoneChanging(value);
                    this.SendPropertyChanging();
                    this._Phone = value;
                    this.SendPropertyChanged("Phone");
                    this.OnPhoneChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Email", DbType = "NVarChar(MAX)")]
        public string Email {
            get {
                return this._Email;
            }
            set {
                if ((this._Email != value)) {
                    this.OnEmailChanging(value);
                    this.SendPropertyChanging();
                    this._Email = value;
                    this.SendPropertyChanged("Email");
                    this.OnEmailChanged();
                }
            }
        }

        public event PropertyChangingEventHandler PropertyChanging;

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void SendPropertyChanging() {
            if ((this.PropertyChanging != null)) {
                this.PropertyChanging(this, emptyChangingEventArgs);
            }
        }

        protected virtual void SendPropertyChanged(String propertyName) {
            if ((this.PropertyChanged != null)) {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
#pragma warning restore 1591
