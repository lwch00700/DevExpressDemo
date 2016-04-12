﻿using DevExpress.Mvvm.DataAnnotations;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DevExpress.Xpf.LayoutControlDemo {
    [MetadataType(typeof(AttributeSupportMetadata))]
    public class AttributeSupportData_FluentAPI {
        public int ID { get; set; }
        public int Age { get; set; }
        public string Employer { get; set; }
        public string FirstName { get; set; }
        public string FullName { get { return FirstName + " " + LastName; } }
        public Gender Gender { get; set; }
        public string LastName { get; set; }
        public string SSN { get; set; }
        public string Department { get; set; }
    }
    public static class AttributeSupportMetadata {
        public static void BuildMetadata(MetadataBuilder<AttributeSupportData_FluentAPI> builder) {
            builder.Property(x => x.ID)
                .NotAutoGenerated();
            builder.Property(x => x.Age)
                .DisplayFormatString("d2", applyDisplayFormatInEditMode: true);
            builder.Property(x => x.Employer)
                .NotEditable();
            builder.Property(x => x.FirstName)
                .DisplayName("First name")
                .Required();
            builder.Property(x => x.FullName)
                .DisplayName("Full name");
            builder.Property(x => x.Gender)
                .DisplayName("Sex");
            builder.Property(x => x.LastName)
                .DisplayName("Last name")
                .Required();
            builder.Property(x => x.SSN)
                .ReadOnly();
            builder.Property(x => x.Department)
                .NullDisplayText("Department not set");

            builder.DataFormLayout()
                .ContainsProperty(x => x.FirstName)
                .ContainsProperty(x => x.LastName)
                .ContainsProperty(x => x.FullName)
                .ContainsProperty(x => x.Gender);
        }
    }
}
