using System;
using System.Globalization;
using DevExpress.Spreadsheet;
using DevExpress.XtraRichEdit;
using System.Text;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using DevExpress.Utils;
using System.Text.RegularExpressions;
using DevExpress.DXperience.Demos;
using DevExpress.Xpf.Grid;
using System.Collections;
using DevExpress.Xpf.DemoBase.Helpers.TextColorizer;
using System.Reflection;

namespace SpreadsheetDemo {
    public partial class PivotAPI : SpreadsheetDemoModule {
        ExampleCodeEditor codeEditor;
        ExampleEvaluatorByTimer evaluator;
        bool richEditControlVBLoaded = false;
        bool richEditControlCsLoaded = false;

        public PivotAPI() {
            InitializeComponent();
            string examplePath = DemoUtils.GetRelativeDirectoryPath("CodeExamples\\Pivots");
            Dictionary<string, FileInfo> examplesCS = CodeExampleDemoUtils.GatherExamplesFromProject(examplePath, ExampleLanguage.Csharp);
            Dictionary<string, FileInfo> examplesVB = CodeExampleDemoUtils.GatherExamplesFromProject(examplePath, ExampleLanguage.VB);
            DisableTabs(examplesCS.Count, examplesVB.Count);
            List<CodeExampleGroup> examples = CodeExampleDemoUtils.FindExamples(examplePath, examplesCS, examplesVB);
            RearrangeExamples(examples);
            ShowExamplesInTreeList(treeList1, examples);

            richEditControlCS.Loaded += richEditControlCS_Loaded;
            richEditControlVB.Loaded += richEditControlVB_Loaded;
            CodeLanguage language = DevExpress.Xpf.DemoBase.Helpers.DemoHelper.GetDemoLanguage(Assembly.GetExecutingAssembly());
            CurrentExampleLanguage = object.Equals(language, CodeLanguage.CS)? ExampleLanguage.Csharp : ExampleLanguage.VB;
            this.evaluator = new SpreadsheetExampleEvaluatorByTimer(true);

            this.evaluator.QueryEvaluate += OnExampleEvaluatorQueryEvaluate;
            this.evaluator.OnBeforeCompile += evaluator_OnBeforeCompile;
            this.evaluator.OnAfterCompile += evaluator_OnAfterCompile;
        }

        void evaluator_OnAfterCompile(object sender, OnAfterCompileEventArgs args) {
            IWorkbook workbook = spreadsheetControl1.Document;
            foreach (Worksheet sheet in workbook.Worksheets)
                sheet.PrintOptions.PrintGridlines = true;

            Worksheet firstSheet = workbook.Worksheets[0];
            Range usedRange = firstSheet.GetUsedRange();
            firstSheet.SelectedCell = usedRange[usedRange.RowCount * usedRange.ColumnCount - 1].Offset(1, 1);

            workbook.Worksheets.ActiveWorksheet.Visible = true;

            if (codeEditor != null)
                codeEditor.AfterCompile(args.Result);
            spreadsheetControl1.EndUpdate();
        }

        void evaluator_OnBeforeCompile(object sender, EventArgs args) {
            spreadsheetControl1.BeginUpdate();
            if (codeEditor != null)
                codeEditor.BeforeCompile();

            IWorkbook workbook = spreadsheetControl1.Document;
            workbook.Options.Culture = DefaultCulture;
            bool loaded = workbook.LoadDocument(DemoUtils.GetRelativePath("PivotTableTemplate.xlsx"));
            System.Diagnostics.Debug.Assert(loaded);
        }

        void richEditControlCS_Loaded(object sender, System.Windows.RoutedEventArgs e) {
            if (richEditControlVBLoaded && !richEditControlCsLoaded)
                CreateCodeEditor();
            richEditControlCsLoaded = true;
        }

        void richEditControlVB_Loaded(object sender, System.Windows.RoutedEventArgs e) {
            if (richEditControlCsLoaded && !richEditControlVBLoaded )
                CreateCodeEditor();
            richEditControlVBLoaded = true;
        }

        void DisableTabs(int examplesCSCount, int examplesVBCount) {
            if (examplesCSCount == 0)
                tabControl.GetTabItem((int)ExampleLanguage.Csharp).IsEnabled = false;
            if (examplesVBCount == 0)
                tabControl.GetTabItem((int)ExampleLanguage.VB).IsEnabled = false;
        }

        void CreateCodeEditor(){
            System.Diagnostics.Debug.Assert(codeEditor == null);

            richEditControlCS.Tag = "Cs";
            richEditControlVB.Tag = "Vb";
            this.codeEditor = new ExampleCodeEditor(richEditControlCS, richEditControlVB);
            this.codeEditor.CurrentExampleLanguage = CurrentExampleLanguage;

            ShowFirstExample();
        }

        void RearrangeExamples(List<CodeExampleGroup> examples) {
            for(int i = 0; i < examples.Count; i++) {
                CodeExampleGroup group = examples[i];
                if(group.Name == "Pivot Table Actions") {
                    examples.RemoveAt(i);
                    examples.Insert(0, group);
                    break;
                }
            }
        }
        void ShowExamplesInTreeList(TreeListControl treeList1, List<CodeExampleGroup> examples) {
            treeList1.ItemsSource = examples;
        }
        void ShowFirstExample() {
            treeList1.View.ExpandAllNodes();

            if (treeList1.View.Nodes.Count > 0)
                treeList1.View.FocusedNode = treeList1.View.Nodes[0].Nodes.First();
        }

        ExampleLanguage CurrentExampleLanguage {
            get { return (ExampleLanguage)tabControl.SelectedIndex; }
            set {
                if (codeEditor!=null)
                    this.codeEditor.CurrentExampleLanguage = value;
                tabControl.SelectedIndex = (value == ExampleLanguage.Csharp) ? 0 : 1;
            }
        }
        private void OnNewExampleSelected(object sender, CurrentItemChangedEventArgs e) {
            CodeExample newExample = e.NewItem as CodeExample;
            CodeExample oldExample = e.OldItem as CodeExample;

            if (newExample == null )
                return;

            if (codeEditor == null)
                return;

            string exampleCode = codeEditor.ShowExample(oldExample, newExample);
            codeExampleNameLbl.Content = CodeExampleDemoUtils.ConvertStringToMoreHumanReadableForm(newExample.RegionName) + " example";

            CodeEvaluationEventArgs args = new CodeEvaluationEventArgs();
            InitializeCodeEvaluationEventArgs(args);
            evaluator.ForceCompile(args);
        }
        void InitializeCodeEvaluationEventArgs(CodeEvaluationEventArgs e) {
            e.Result = true;
            if (codeEditor == null)
                return;

            e.Code = codeEditor.CurrentCodeEditor.Text;
            e.Language = CurrentExampleLanguage;
            e.EvaluationParameter = spreadsheetControl1.Document;
        }
        void OnExampleEvaluatorQueryEvaluate(object sender, CodeEvaluationEventArgs e) {
            e.Result = false;
            if ((codeEditor != null) &&codeEditor.RichEditTextChanged) {
                TimeSpan span = DateTime.Now - codeEditor.LastExampleCodeModifiedTime;

                if (span < TimeSpan.FromMilliseconds(1000)) {
                    codeEditor.ResetLastExampleModifiedTime();
                    return;
                }
                InitializeCodeEvaluationEventArgs(e);
            }
        }

        void tabControl_SelectionChanged(object sender, DevExpress.Xpf.Core.TabControlSelectionChangedEventArgs e) {
            ExampleLanguage value = (ExampleLanguage)(e.NewSelectedIndex);

            if(this.codeEditor !=null)
                this.codeEditor.CurrentExampleLanguage = value;
        }

        private void view_CustomColumnDisplayText(object sender, DevExpress.Xpf.Grid.TreeList.TreeListCustomColumnDisplayTextEventArgs e) {

            if (e.Node.HasChildren && e.Node.Content is CodeExampleGroup) {
                e.DisplayText = (e.Node.Content as CodeExampleGroup).Name;
            }
        }
    }
}
