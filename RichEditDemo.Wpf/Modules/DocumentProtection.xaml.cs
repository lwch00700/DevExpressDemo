using System;
using System.Collections.Generic;
using System.Windows;
using DevExpress.XtraRichEdit.Services;
using DevExpress.XtraRichEdit.API.Native;
using DevExpress.Xpf.Editors;

namespace RichEditDemo {
    public partial class DocumentProtection : RichEditDemoModule {
        readonly UserService userService;

        public DocumentProtection() {
            InitializeComponent();
            richEdit.RemoveService(typeof(IUserListService));
            this.userService = new UserService();
            richEdit.AddService(typeof(IUserListService), this.userService);
        }

        void richEdit_Loaded(object sender, RoutedEventArgs e) {
            richEdit.DocumentLoaded += OnRichEditControlDocumentLoaded;
            richEdit.DocumentProtectionChanged += OnRichEditControlDocumentProtectionChanged;
            OpenXmlLoadHelper.Load("DocumentProtection.docx", richEdit);
        }

        void OnRichEditControlDocumentProtectionChanged(object sender, EventArgs e) {
            ShowAlert(richEdit.Document.IsDocumentProtected);
        }
        void OnRichEditControlDocumentLoaded(object sender, EventArgs e) {
            RangePermissionCollection rangePermissions = richEdit.Document.BeginUpdateRangePermissions();
            richEdit.Document.CancelUpdateRangePermissions(rangePermissions);
            List<String> users = FetchUsers(rangePermissions);
            userService.Update(users);
            cbUserName.Items.Clear();
            cbUserName.Items.AddRange(users.ToArray());
            if (users.Count > 0)
                cbUserName.EditValue = users[0];
            ShowAlert(richEdit.Document.IsDocumentProtected);
        }
        List<String> FetchUsers(RangePermissionCollection rangePermissions) {
            List<String> users = new List<string>();
            foreach (RangePermission rangePermission in rangePermissions) {
                string userName = rangePermission.UserName;
                if (users.Contains(userName))
                    continue;
                if (!String.IsNullOrEmpty(userName))
                    users.Add(userName);
            }
            return users;
        }
        void SetActiveUser() {
            richEdit.Options.Authentication.EMail = cbUserName.Text;
        }
        void ShowAlert(bool show) {
            pnlAlert.Visibility = show ? Visibility.Visible : Visibility.Collapsed;
        }

        void cbUserName_EditValueChanged(object sender, EditValueChangedEventArgs e) {
            SetActiveUser();
        }
    }
    public class UserService : IUserListService {
        readonly List<string> users = new List<string>();

        public List<string> Users { get { return users; } }

        #region IUserListService Members
        IList<string> IUserListService.GetUsers() {
            return Users;
        }
        #endregion
        public void Update(List<String> userList) {
            this.users.Clear();
            this.users.AddRange(userList);
        }
    }
}
