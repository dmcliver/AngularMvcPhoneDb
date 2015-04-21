using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;

namespace AngularMvcPhoneDb.Core.Domain
{
    public class PhoneDbContext : DbContext
    {
        public PhoneDbContext() : base("PhoneDB")
        {
            this.Configuration.ProxyCreationEnabled = false;
            this.Configuration.LazyLoadingEnabled = false;
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            var phone = modelBuilder.Entity<SmartPhone>().ToTable("SmartPhone").HasKey(s => s.SmartPhoneId);
            phone.Property(s => s.SmartPhoneId);
            phone.Property(s => s.BatteryCapacity);
            phone.Property(s => s.Model).IsRequired();
            phone.Property(s => s.Cpu).IsRequired();
            phone.Property(s => s.PixelHeight).IsRequired();
            phone.Property(s => s.PixelWidth).IsRequired();
            phone.Property(s => s.DisplayType).IsRequired();

            EntityTypeConfiguration<Manufacturer> manufacturer = modelBuilder.Entity<Manufacturer>();
            manufacturer.ToTable("Manufacturer").HasKey(m => m.Name);
            manufacturer.HasMany(m => m.SmartPhones)
                        .WithRequired(p => p.Manufacturer)
                        .HasForeignKey(p => p.ManufacturerName);
        }

        public DbSet<SmartPhone> SmartPhone { get; set; }
        public DbSet<Manufacturer> Manufacturer { get; set; }
    }
} 

