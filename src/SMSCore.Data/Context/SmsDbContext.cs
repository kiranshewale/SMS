using Microsoft.EntityFrameworkCore;
using SMSCore.Data.Entities;

namespace SMSCore.Data.Context
{
    public partial class SmsDbContext : DbContext
    {
        public SmsDbContext(DbContextOptions<SmsDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<BankDetailsMaster> BankDetailsMaster { get; set; }
        public virtual DbSet<BranchMaster> BranchMaster { get; set; }
        public virtual DbSet<ColourMaster> ColourMaster { get; set; }
        public virtual DbSet<CustomerDetailsTable> CustomerDetailsTable { get; set; }
        public virtual DbSet<DesignationMaster> DesignationMaster { get; set; }
        public virtual DbSet<DisplayVehiclesDetails> DisplayVehiclesDetails { get; set; }
        public virtual DbSet<GatePassDetails> GatePassDetails { get; set; }
        public virtual DbSet<GatePassVehicles> GatePassVehicles { get; set; }
        public virtual DbSet<MenuMaster> MenuMaster { get; set; }
        public virtual DbSet<ModelMaster> ModelMaster { get; set; }
        public virtual DbSet<ModelVarientColourMapping> ModelVarientColourMapping { get; set; }
        public virtual DbSet<ModelVarientMapping> ModelVarientMapping { get; set; }
        public virtual DbSet<PaymentModeMaster> PaymentModeMaster { get; set; }
        public virtual DbSet<PdiVehiclesDetails> PdiVehiclesDetails { get; set; }
        public virtual DbSet<PermissionMaster> PermissionMaster { get; set; }
        public virtual DbSet<PriceListMaster> PriceListMaster { get; set; }
        public virtual DbSet<QuotationDetails> QuotationDetails { get; set; }
        public virtual DbSet<RoleMaster> RoleMaster { get; set; }
        public virtual DbSet<ShowroomVehiclesDetails> ShowroomVehiclesDetails { get; set; }
        public virtual DbSet<StateListMaster> StateListMaster { get; set; }
        public virtual DbSet<SubDealerMaster> SubDealerMaster { get; set; }
        public virtual DbSet<TaxMaster> TaxMaster { get; set; }
        public virtual DbSet<UserMaster> UserMaster { get; set; }
        public virtual DbSet<UserRoleMapping> UserRoleMapping { get; set; }
        public virtual DbSet<VarientMaster> VarientMaster { get; set; }
        public virtual DbSet<VehicleAllotmentDetails> VehicleAllotmentDetails { get; set; }
        public virtual DbSet<VehiclesMaster> VehiclesMaster { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BankDetailsMaster>(entity =>
            {
                entity.HasNoKey();

                entity.Property(e => e.AccountHolderName).HasMaxLength(150);

                entity.Property(e => e.BankAccountNumber).HasMaxLength(50);

                entity.Property(e => e.BankName).HasMaxLength(200);

                entity.Property(e => e.City).HasMaxLength(50);

                entity.Property(e => e.DateCreated).HasColumnType("datetime");

                entity.Property(e => e.DateUpdated).HasColumnType("datetime");

                entity.Property(e => e.Ifsccode)
                    .HasColumnName("IFSCCode")
                    .HasMaxLength(50);

                entity.Property(e => e.Pincode).HasMaxLength(50);
            });

            modelBuilder.Entity<BranchMaster>(entity =>
            {
                entity.HasNoKey();

                entity.Property(e => e.BranchName).HasMaxLength(100);

                entity.Property(e => e.ContactNo).HasColumnType("decimal(10, 0)");

                entity.Property(e => e.DateCreated).HasColumnType("datetime");

                entity.Property(e => e.Id).ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<ColourMaster>(entity =>
            {
                entity.Property(e => e.ColourCode).HasMaxLength(150);

                entity.Property(e => e.ColourName).HasMaxLength(250);

                entity.Property(e => e.ColourType).HasMaxLength(50);

                entity.Property(e => e.DateCreated).HasColumnType("datetime");

                entity.Property(e => e.DateUpdated).HasColumnType("datetime");

                entity.Property(e => e.FriendlyName).HasMaxLength(250);
            });

            modelBuilder.Entity<CustomerDetailsTable>(entity =>
            {
                entity.Property(e => e.Address1).HasMaxLength(500);

                entity.Property(e => e.Address2).HasMaxLength(500);

                entity.Property(e => e.AdharNo).HasMaxLength(50);

                entity.Property(e => e.CustomerName).HasMaxLength(150);

                entity.Property(e => e.DateCreated).HasColumnType("datetime");

                entity.Property(e => e.DateUpdated).HasColumnType("datetime");

                entity.Property(e => e.District).HasMaxLength(150);

                entity.Property(e => e.EmailId).HasMaxLength(50);

                entity.Property(e => e.Gstno)
                    .HasColumnName("GSTNo")
                    .HasMaxLength(50);

                entity.Property(e => e.MobileNo1).HasColumnType("decimal(10, 0)");

                entity.Property(e => e.MobileNo2).HasColumnType("numeric(10, 0)");

                entity.Property(e => e.PanNo).HasMaxLength(50);

                entity.Property(e => e.Pin).HasMaxLength(50);

                entity.Property(e => e.Salutation).HasMaxLength(50);

                entity.Property(e => e.Taluka).HasMaxLength(150);
            });

            modelBuilder.Entity<DesignationMaster>(entity =>
            {
                entity.Property(e => e.Active).HasDefaultValueSql("((0))");

                entity.Property(e => e.DateCreated).HasColumnType("datetime");

                entity.Property(e => e.DateUpdated).HasColumnType("datetime");

                entity.Property(e => e.Deleted).HasDefaultValueSql("((0))");

                entity.Property(e => e.Designation).HasMaxLength(50);
            });

            modelBuilder.Entity<DisplayVehiclesDetails>(entity =>
            {
                entity.Property(e => e.ComeForm).HasMaxLength(500);

                entity.Property(e => e.DateCreated).HasColumnType("datetime");

                entity.Property(e => e.OutDate).HasColumnType("datetime");

                entity.Property(e => e.Purpose).HasMaxLength(150);
            });

            modelBuilder.Entity<GatePassDetails>(entity =>
            {
                entity.Property(e => e.DateCreated).HasColumnType("datetime");

                entity.Property(e => e.Status).HasMaxLength(50);

                entity.Property(e => e.VehicleOutFor).HasMaxLength(500);

                entity.Property(e => e.VehicleOutTo).HasMaxLength(150);
            });

            modelBuilder.Entity<GatePassVehicles>(entity =>
            {
                entity.Property(e => e.DateCreated).HasColumnType("datetime");
            });

            modelBuilder.Entity<MenuMaster>(entity =>
            {
                entity.Property(e => e.Description).HasMaxLength(500);

                entity.Property(e => e.MenuName).HasMaxLength(250);
            });

            modelBuilder.Entity<ModelMaster>(entity =>
            {
                entity.Property(e => e.DateCreated).HasColumnType("datetime");

                entity.Property(e => e.DateUpdated).HasColumnType("datetime");

                entity.Property(e => e.FriendlyName).HasMaxLength(150);

                entity.Property(e => e.ModelCode).HasMaxLength(50);

                entity.Property(e => e.ModelName).HasMaxLength(150);
            });

            modelBuilder.Entity<ModelVarientColourMapping>(entity =>
            {
                entity.HasKey(e => new { e.Id, e.VarientId, e.ColourId });

                entity.Property(e => e.Id).ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<PaymentModeMaster>(entity =>
            {
                entity.Property(e => e.PaymentMode).HasMaxLength(50);
            });

            modelBuilder.Entity<PdiVehiclesDetails>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.DateCreated).HasColumnType("datetime");

                entity.Property(e => e.VehicleCameFrom).HasMaxLength(50);

                entity.Property(e => e.VehicleInPdiDate).HasColumnType("datetime");

                entity.Property(e => e.VehicleOutFromPdiDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<PriceListMaster>(entity =>
            {
                entity.Property(e => e.AccessoryComboKit).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Antirust).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.CarpetLamination).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.ClayBar).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.DateCreated).HasColumnType("datetime");

                entity.Property(e => e.DateUpdated).HasColumnType("datetime");

                entity.Property(e => e.ExShowroomPrice).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.ExtendedWarranty).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.FastTag).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Hainsurance)
                    .HasColumnName("HAInsurance")
                    .HasColumnType("decimal(18, 2)");

                entity.Property(e => e.HanilDepWithKeyProtect)
                    .HasColumnName("HANilDepWithKeyProtect")
                    .HasColumnType("decimal(18, 2)");

                entity.Property(e => e.HartiwithEngineProtect)
                    .HasColumnName("HARTIWithEngineProtect")
                    .HasColumnType("decimal(18, 2)");

                entity.Property(e => e.ModelCode).HasMaxLength(50);

                entity.Property(e => e.ModelVarient).HasMaxLength(250);

                entity.Property(e => e.Rsa)
                    .HasColumnName("RSA")
                    .HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Rtoindividual)
                    .HasColumnName("RTOIndividual")
                    .HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Tcs)
                    .HasColumnName("TCS")
                    .HasColumnType("decimal(18, 2)");

                entity.Property(e => e.TotatPrice).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.VarientType).HasMaxLength(150);
            });

            modelBuilder.Entity<QuotationDetails>(entity =>
            {
                entity.Property(e => e.BookingAmount).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.DateCretaed).HasColumnType("datetime");

                entity.Property(e => e.LeadSource).HasMaxLength(150);

                entity.Property(e => e.ScId).HasColumnName("ScID");

                entity.Property(e => e.VarientType).HasMaxLength(50);
            });

            modelBuilder.Entity<RoleMaster>(entity =>
            {
                entity.Property(e => e.DateCreated).HasColumnType("datetime");

                entity.Property(e => e.Description).HasMaxLength(500);

                entity.Property(e => e.RoleName).HasMaxLength(50);
            });

            modelBuilder.Entity<ShowroomVehiclesDetails>(entity =>
            {
                entity.Property(e => e.DateCreated).HasColumnType("datetime");

                entity.Property(e => e.DateDelivered).HasColumnType("datetime");

                entity.Property(e => e.VehicleCameFrom).HasMaxLength(50);
            });

            modelBuilder.Entity<StateListMaster>(entity =>
            {
                entity.Property(e => e.State).HasMaxLength(250);
            });

            modelBuilder.Entity<SubDealerMaster>(entity =>
            {
                entity.Property(e => e.City).HasMaxLength(150);

                entity.Property(e => e.ContactNo1).HasColumnType("numeric(10, 0)");

                entity.Property(e => e.ContactNo2).HasColumnType("numeric(10, 0)");

                entity.Property(e => e.DateCreated).HasColumnType("datetime");

                entity.Property(e => e.DealerCode).HasMaxLength(50);

                entity.Property(e => e.EmailId).HasMaxLength(50);

                entity.Property(e => e.Pincode).HasMaxLength(10);

                entity.Property(e => e.State).HasMaxLength(150);

                entity.Property(e => e.SubDealerName).HasMaxLength(250);
            });

            modelBuilder.Entity<TaxMaster>(entity =>
            {
                entity.Property(e => e.DateCreated).HasColumnType("datetime");

                entity.Property(e => e.DateUpdated).HasColumnType("datetime");

                entity.Property(e => e.Percentage).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.TaxName).HasMaxLength(200);
            });

            modelBuilder.Entity<UserMaster>(entity =>
            {
                entity.Property(e => e.Address).HasMaxLength(500);

                entity.Property(e => e.ContactNumber).HasColumnType("numeric(10, 0)");

                entity.Property(e => e.DateCreated).HasColumnType("datetime");

                entity.Property(e => e.EmailId)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Password).HasMaxLength(50);

                entity.Property(e => e.UserName).HasMaxLength(50);
            });

            modelBuilder.Entity<UserRoleMapping>(entity =>
            {
                entity.HasOne(d => d.Role)
                    .WithMany(p => p.UserRoleMapping)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("UserRoleMapping_Destination");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserRoleMapping)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("UserRoleMapping_Source");
            });

            modelBuilder.Entity<VarientMaster>(entity =>
            {
                entity.Property(e => e.DateCreated).HasColumnType("datetime");

                entity.Property(e => e.DateUpdated).HasColumnType("datetime");

                entity.Property(e => e.FriendlyName).HasMaxLength(150);

                entity.Property(e => e.VarientCode).HasMaxLength(50);

                entity.Property(e => e.VerientName).HasMaxLength(150);
            });

            modelBuilder.Entity<VehicleAllotmentDetails>(entity =>
            {
                entity.Property(e => e.DateCreated).HasColumnType("datetime");

                entity.Property(e => e.DateUpdated).HasColumnType("datetime");

                entity.Property(e => e.Scid).HasColumnName("SCid");

                entity.Property(e => e.VarientType).HasMaxLength(50);
            });

            modelBuilder.Entity<VehiclesMaster>(entity =>
            {
                entity.Property(e => e.Cess).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Cgst)
                    .HasColumnName("CGST")
                    .HasColumnType("decimal(18, 2)");

                entity.Property(e => e.DateCreated).HasColumnType("datetime");

                entity.Property(e => e.DateUpdated).HasColumnType("datetime");

                entity.Property(e => e.DealerCode).HasMaxLength(50);

                entity.Property(e => e.DiscountAsPerHonda).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.EngineNumber).HasMaxLength(50);

                entity.Property(e => e.GrossAmount).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.HcilinvoiceDate)
                    .HasColumnName("HCILInvoiceDate")
                    .HasColumnType("datetime");

                entity.Property(e => e.Igst)
                    .HasColumnName("IGST")
                    .HasColumnType("decimal(18, 2)");

                entity.Property(e => e.InvoiceNumber).HasMaxLength(50);

                entity.Property(e => e.KeyNumber).HasMaxLength(50);

                entity.Property(e => e.ModelColour).HasMaxLength(50);

                entity.Property(e => e.ModelName).HasMaxLength(50);

                entity.Property(e => e.ModelVarient).HasMaxLength(50);

                entity.Property(e => e.Pdi).HasColumnName("PDI");

                entity.Property(e => e.PurchaseAmount).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Sgst)
                    .HasColumnName("SGST")
                    .HasColumnType("decimal(18, 2)");

                entity.Property(e => e.TrackStatus).HasMaxLength(50);

                entity.Property(e => e.TradeDiscount).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.TransporterName).HasMaxLength(500);

                entity.Property(e => e.TruckRegistrationNumber).HasMaxLength(50);

                entity.Property(e => e.Ugst)
                    .HasColumnName("UGST")
                    .HasColumnType("decimal(18, 2)");

                entity.Property(e => e.UnloadDate).HasColumnType("datetime");

                entity.Property(e => e.VehicleStatus)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Vinnumber)
                    .HasColumnName("VINNumber")
                    .HasMaxLength(50);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
