using DevExpress.Internal;
using DevExpress.Utils.Filtering;
using DevExpress.Xpf.LayoutControlDemo;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Common;
using System.Data.Entity;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Text;

namespace DevExpress.DemoData.Models.Vehicles {
    public class Model {
        public int ID { get; set; }
        public int TrademarkID { get; set; }
        public string Name { get; set; }
        public string Modification { get; set; }
        public int CategoryID { get; set; }
        public decimal Price { get; set; }
        public int? MPGCity { get; set; }
        public int? MPGHighway { get; set; }
        public int Doors { get; set; }
        public int BodyStyleID { get; set; }
        public int Cilinders { get; set; }
        public string Horsepower { get; set; }
        public string Torque { get; set; }
        public string TransmissionSpeeds { get; set; }
        public int TransmissionTypeID { get; set; }
        public string Description { get; set; }
        public byte[] Image { get; set; }
        public byte[] Photo { get; set; }
        public DateTime? DeliveryDate { get; set; }
        public bool InStock { get; set; }

        public string TrademarkName { get { return Trademark.Name; } }
        public string CategoryName { get { return Category.Name; } }
        public string BodyStyleName { get { return BodyStyle.Name; } }
        public string TransmissionTypeName { get { return TransmissionType.Name; } }

        public virtual Trademark Trademark { get; set; }
        public virtual Category Category { get; set; }
        public virtual BodyStyle BodyStyle { get; set; }
        public virtual TransmissionType TransmissionType { get; set; }
    }

    public class BodyStyle {
        public int ID { get; set; }
        public string Name { get; set; }
    }

    public class Category {
        public int ID { get; set; }
        public string Name { get; set; }
        public byte[] Picture { get; set; }
    }

    public class Trademark {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Site { get; set; }
        public byte[] Logo { get; set; }
        public string Description { get; set; }
    }

    public class TransmissionType {
        public int ID { get; set; }
        public string Name { get; set; }
    }

    public partial class VehiclesContext : DbContext {
        public VehiclesContext() : base(CreateConnection(), true) { }
        public VehiclesContext(string connectionString) : base(connectionString) { }
        public VehiclesContext(DbConnection connection) : base(connection, true) { }

        protected override void OnModelCreating(DbModelBuilder modelBuilder) {
            modelBuilder.Entity<Model>()
                .ToTable("Model");
            modelBuilder.Entity<BodyStyle>()
                .ToTable("BodyStyle");
            modelBuilder.Entity<Category>()
                .ToTable("Category");
            modelBuilder.Entity<Trademark>()
                .ToTable("Trademark");
            modelBuilder.Entity<TransmissionType>()
                .ToTable("TransmissionType");
            modelBuilder.Entity<Model>()
                .Property(x => x.MPGCity)
                .HasColumnName("MPG City");
            modelBuilder.Entity<Model>()
                .Property(x => x.MPGHighway)
                .HasColumnName("MPG Highway");
            modelBuilder.Entity<Model>()
                .Property(x => x.TransmissionSpeeds)
                .HasColumnName("Transmission Speeds");
            modelBuilder.Entity<Model>()
                .Property(x => x.TransmissionTypeID)
                .HasColumnName("Transmission Type");
            modelBuilder.Entity<Model>()
                .Property(x => x.DeliveryDate)
                .HasColumnName("Delivery Date");
        }

        static string filePath;
        static DbConnection CreateConnection() {
            if(filePath == null)
                filePath = DataDirectoryHelper.GetFile("vehicles.db", DataDirectoryHelper.DataFolderName);
            try {
                var attributes = File.GetAttributes(filePath);
                if(attributes.HasFlag(FileAttributes.ReadOnly)) {
                    File.SetAttributes(filePath, attributes & ~FileAttributes.ReadOnly);
                }
            } catch { }
            var connection = DbProviderFactories.GetFactory("System.Data.SQLite.EF6").CreateConnection();
            connection.ConnectionString = new SQLiteConnectionStringBuilder { DataSource = filePath }.ConnectionString;
            return connection;
        }

        public DbSet<Model> Models { get; set; }
        public DbSet<BodyStyle> BodyStyles { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Trademark> Trademarks { get; set; }
        public DbSet<TransmissionType> TransmissionTypes { get; set; }
    }
}
