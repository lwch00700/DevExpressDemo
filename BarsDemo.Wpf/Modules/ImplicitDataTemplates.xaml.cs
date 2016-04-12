using DevExpress.Xpf.DemoBase;
using System;
using System.Collections.Generic;

namespace BarsDemo {

    [CodeFile("ViewModels/ImplicitDataTemplatesModel.(cs)")]
    public partial class ImplicitDataTemplates : BarsDemoModule {
        public ImplicitDataTemplates() {
            InitializeComponent();
            DataContext = new List<CommandModel> {
                new GroupModel() {
                    Caption = "File",
                    Glyph = "Home_16x16.png",
                    Commands = new List<CommandModel>() {
                        new CommandModel() { Caption = "New", Glyph = "New_16x16.png" },
                        new CommandModel() { Caption = "Open", Glyph = "Open_16x16.png" },
                        new CommandModel() { Caption = "Close", Glyph = "Close_16x16.png" }
                    }
                },
                new CommandModel() { Caption = "Settings", Glyph = "New_16x16.png" },
                new EditorModel() { Caption = "Search:", Glyph="Find_16x16.png" },
                new LabelModel() { Content = DateTime.Now }
            };
        }
    }
}
