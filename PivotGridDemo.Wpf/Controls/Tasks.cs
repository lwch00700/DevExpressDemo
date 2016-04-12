using System;
using System.Collections;
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
using System.Windows.Shapes;
using System.Xml;
using DevExpress.Data.Filtering;
using System.Data.SqlClient;
using System.Data;
using System.Windows.Threading;
using System.Threading;
using System.ComponentModel;
using DevExpress.XtraEditors.DXErrorProvider;
using DevExpress.Data;

namespace PivotGridDemo.PivotGrid {
    public abstract class BindingCollection : CollectionBase, IBindingList {
        public virtual void OnListChanged(object item) { }
        public object AddNew() { return null; }
        public bool AllowEdit { get { return true; } }
        public bool AllowNew { get { return false; } }
        public bool AllowRemove { get { return true; } }

        private ListChangedEventHandler listChangedHandler;
        public event ListChangedEventHandler ListChanged {
            add { listChangedHandler += value; }
            remove { listChangedHandler -= value; }
        }
        internal void OnListChanged(ListChangedEventArgs args) {
            if(listChangedHandler != null) {
                listChangedHandler(this, args);
            }
        }
        protected override void OnRemoveComplete(int index, object value) {
            OnListChanged(new ListChangedEventArgs(ListChangedType.ItemDeleted, index));
        }
        protected override void OnInsertComplete(int index, object value) {
            OnListChanged(new ListChangedEventArgs(ListChangedType.ItemAdded, index));
        }

        public void AddIndex(PropertyDescriptor pd) { throw new NotSupportedException(); }
        public void ApplySort(PropertyDescriptor pd, ListSortDirection dir) { throw new NotSupportedException(); }
        public int Find(PropertyDescriptor property, object key) { throw new NotSupportedException(); }
        public bool IsSorted { get { return false; } }
        public void RemoveIndex(PropertyDescriptor pd) { throw new NotSupportedException(); }
        public void RemoveSort() { throw new NotSupportedException(); }
        public ListSortDirection SortDirection { get { throw new NotSupportedException(); } }
        public PropertyDescriptor SortProperty { get { throw new NotSupportedException(); } }
        public bool SupportsChangeNotification { get { return true; } }
        public bool SupportsSearching { get { return false; } }
        public bool SupportsSorting { get { return false; } }
    }
    public class Task {
        int fID;
        string fName;
        DateTime fDate;
        int fPercentComplete;
        bool fComplete;
        string fNote;

        BindingCollection fRelationCollection;
        public Task(BindingCollection relationCollection, int id, string name, DateTime date) {
            this.fRelationCollection = relationCollection;
            this.fID = id;
            this.fName = name;
            this.fDate = date;
            this.fPercentComplete = 0;
            this.fComplete = false;
            this.fNote = "";
        }

        public int ID {
            get { return fID; }
        }

        public string TaskName {
            get { return fName; }
            set {
                fName = value;
                OnListChanged();
            }
        }

        public DateTime DueDate {
            get { return fDate; }
            set {
                fDate = value;
                OnListChanged();
            }
        }

        public bool Complete {
            get { return fComplete; }
            set {
                fComplete = value;
                if(fComplete) fPercentComplete = 100;
                if(!fComplete && fPercentComplete == 100) fPercentComplete = 0;
                OnListChanged();
            }
        }

        public int PercentComplete {
            get { return fPercentComplete; }
            set {
                fPercentComplete = value;
                if(fPercentComplete < 0) fPercentComplete = 0;
                if(fPercentComplete > 100) fPercentComplete = 100;
                fComplete = fPercentComplete == 100;
                OnListChanged();
            }
        }

        public string Note {
            get { return fNote; }
            set {
                fNote = value;
                OnListChanged();
            }
        }

        public string CategoryName {
            get { return GetCategoryByTask((TasksWithCategories)fRelationCollection, this); }
        }

        static string GetCategoryByTask(TasksWithCategories collection, Task task) {
            string ret = "";
            for(int i = 0; i < collection.fCategories.Count; i++) {
                if(collection.HasCategory(task, collection.fCategories[i]))
                    ret += string.Format("{0}{1}", (ret == "" ? "" : ", "), collection.fCategories[i].CategoryName);
            }
            if(ret == "") ret = "<None>";
            return ret;
        }

        private void OnListChanged() {
            fRelationCollection.OnListChanged(this);
        }
    }
    public class Category {
        int fID;
        string fName;
        public Category(int id, string name) {
            this.fID = id;
            this.fName = name;
        }

        public int ID {
            get { return fID; }
        }

        public string CategoryName {
            get { return fName; }
            set { fName = value; }
        }
    }
    public class Relation {
        internal Task fTask;
        internal Category fCategory;
        public Relation(Task task, Category category) {
            this.fTask = task;
            this.fCategory = category;
        }
        public string TaskName {
            get { return fTask.TaskName; }
            set { fTask.TaskName = value; }
        }
        public DateTime DueDate {
            get { return fTask.DueDate; }
            set { fTask.DueDate = value; }
        }
        public string CategoryName {
            get { return fCategory.CategoryName; }
        }
        public bool Complete {
            get { return fTask.Complete; }
            set { fTask.Complete = value; }
        }

        public int PercentComplete {
            get { return fTask.PercentComplete; }
            set { fTask.PercentComplete = value; }
        }

        public string Note {
            get { return fTask.Note; }
            set { fTask.Note = value; }
        }
    }
    public class Tasks : BindingCollection {
        public static int MaxTasks = 7;
        public static Tasks GetTasks(TasksWithCategories collection) {
            Tasks ret = new Tasks();
            Random rnd = new Random();
            for(int i = 0; i < MaxTasks; i++) {
                ret.List.Add(new Task(collection, i + 1, "Task" + (i + 1).ToString(), DateTime.Today.AddDays(rnd.Next(5))));
                if(i == 2) ret[i].PercentComplete = 50;
                if(i == 6) ret[i].PercentComplete = 100;
            }
            return ret;
        }
        public Task this[int index] {
            get { return (Task)(List[index]); }
            set { List[index] = value; }
        }
    }
    public class Categories : BindingCollection {
        public static int MaxCategories = 6;
        public static Categories GetCategories(TasksWithCategories collection) {
            Categories ret = new Categories();
            string[] names = new string[] { "Business", "Competitor", "Favorites", "Gifts", "Goals", "Holiday", "Ideas", "International", "Personal" };
            for(int i = 0; i < names.Length; i++)
                ret.List.Add(new Category(i + 1, names[i]));
            return ret;
        }
        public Category this[int index] {
            get { return (Category)(List[index]); }
            set { List[index] = value; }
        }
    }
    public class TasksWithCategories : BindingCollection {
        internal Tasks fTasks;
        internal Categories fCategories;
        public TasksWithCategories() {
            fTasks = Tasks.GetTasks(this);
            fCategories = Categories.GetCategories(this);
        }
        public static TasksWithCategories GetTasksWithCategories() {
            TasksWithCategories ret = new TasksWithCategories();
            Random rnd = new Random();
            for(int i = 0; i < Tasks.MaxTasks; i++)
                for(int j = 0; j < 1 + rnd.Next(Categories.MaxCategories); j++) {
                    Category cat = ret.fCategories[rnd.Next(ret.fCategories.Count)];
                    if(!ret.HasCategory(ret.fTasks[i], cat))
                        ret.List.Add(new Relation(ret.fTasks[i], cat));
                }
            return ret;
        }
        public Relation this[int index] {
            get { return (Relation)(List[index]); }
            set { List[index] = value; }
        }
        public override void OnListChanged(object item) {
            if(item == null) return;
            for(int i = 0; i < this.Count; i++)
                if(item.Equals(this[i].fTask))
                    this.OnListChanged(new ListChangedEventArgs(ListChangedType.ItemChanged, i));
        }
        public bool HasCategory(Task task, Category category) {
            for(int i = 0; i < this.Count; i++)
                if(this[i].fCategory == category && this[i].fTask == task)
                    return true;
            return false;
        }
    }
}
