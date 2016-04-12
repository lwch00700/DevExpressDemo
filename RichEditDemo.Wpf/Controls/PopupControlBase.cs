using System;
using System.Windows.Controls;
using DevExpress.XtraRichEdit.API.Native;
using DevExpress.Xpf.Core;
using System.Windows.Input;

namespace RichEditDemo {
    public class PopupControlBase : UserControl {
        object fEditValue;
        DocumentRange fRange;
        FloatingContainer fOwnerWindow;

        public PopupControlBase() {
            this.KeyDown += PopupControlBase_KeyDown;
        }

        public virtual object EditValue { get { return fEditValue; } }
        public DocumentRange Range { get { return fRange; } set { fRange = value; } }
        public FloatingContainer OwnerWindow {
            get { return fOwnerWindow; }
            set {
                if (fOwnerWindow == value)
                    return;
                fOwnerWindow = value;
                OnOwnerWindowChanged();
            }
        }

        EventHandler onCommit;
        public event EventHandler Commit { add { onCommit += value; } remove { onCommit -= value; } }
        protected void RaiseCommitEvent() {
            if (onCommit != null)
                onCommit(this, EventArgs.Empty);
        }

        protected virtual void OnOwnerWindowChanged() {
        }
        protected virtual void SetEditValueCore(object value) {
            this.fEditValue = value;
        }
        protected virtual void SetEditValue() {
        }
        private void PopupControlBase_KeyDown(object sender, KeyEventArgs e) {
            if (e.Key == Key.Escape)
                Close();
            else if (e.Key == Key.Enter)
                PerformCommit();
        }
        protected virtual void PerformCommit() {
            SetEditValue();
            RaiseCommitEvent();
            Close();
        }
        protected void Close() {
            if (OwnerWindow != null && OwnerWindow.IsOpen)
                OwnerWindow.IsOpen = false;
        }
    }
}
