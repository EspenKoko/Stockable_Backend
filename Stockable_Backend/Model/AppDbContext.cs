using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace Stockable_Backend.Model
{
    //public class AppDbContext : DbContext
    public class AppDbContext : IdentityDbContext<User, Role, string, UserClaim, UserRole, UserLogin, RoleClaim, UserToken>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Branch> Branches { get; set; }
        public DbSet<PurchaseOrderStatus> PurchaseOrderStatuses { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<EmployeeType> EmployeeTypes { get; set; }
        public DbSet<PrinterStatus> PrinterStatuses { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<ClientUser> ClientUsers { get; set; }
        public DbSet<Diagnostics> Diagnostics { get; set; }
        public DbSet<ErrorCode> ErrorCodes { get; set; }
        public DbSet<AssignedTechnician> AssignedTechnicians { get; set; }
        public DbSet<ErrorLog> ErrorLogs { get; set; }
        //public DbSet<ErrorLogErrorCodes> ErrorLogErrorCodes { get; set; }
        public DbSet<ErrorLogStatus> ErrorLogStatuses { get; set; }
        public DbSet<Help> Help { get; set; }
        public DbSet<Hub> Hubs { get; set; }
        public DbSet<HubUser> HubUsers { get; set; }
        public DbSet<PartsRequest> PartsRequests { get; set; }
        public DbSet<LabourRate> LabourRates { get; set; }
        public DbSet<PaymentType> PaymentTypes { get; set; }
        public DbSet<SupplierOrderStatus> SupplierOrderStatuses { get; set; }
        public DbSet<ClientInvoice> ClientInvoices { get; set; }
        public DbSet<Price> Prices { get; set; }
        public DbSet<AssignedPrinter> AssignedPrinters { get; set; }
        //public DbSet<PrinterModel> PrinterModels { get; set; }
        public DbSet<Province> Provinces { get; set; }
        public DbSet<Repair> Repairs { get; set; }
        public DbSet<RepairDiagnostic> RepairDiagnostics { get; set; }
        public DbSet<RepairStock> RepairStocks { get; set; }
        public DbSet<RepairStatus> RepairStatuses { get; set; }
        public DbSet<PurchaseOrder> PurchaseOrders { get; set; }
        public DbSet<Stock> Stocks { get; set; }
        public DbSet<StockCategory> StockCategories { get; set; }
        public DbSet<ClientOrderStatus> ClientOrderStatus { get; set; }
        public DbSet<StockTakeStock> StockStockTakes { get; set; }
        public DbSet<StockSupplierOrder> StockSupplierOrders { get; set; }
        public DbSet<StockTake> StockTakes { get; set; }
        public DbSet<StockType> StockTypes { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<SupplierOrder> SupplierOrders { get; set; }
        public DbSet<TechnicalServiceReport> TechnicalServiceReports { get; set; }
        public DbSet<ClientOrder> ClientOrders { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<VAT> Vats { get; set; }
        public DbSet<Markup> Markups { get; set; }
        public DbSet<ClientUserRequest> ClientUserRequests { get; set; }
        public DbSet<StockTakeStock> StockTakeStocks { get; set; }
        public DbSet<ClientOrderStock> ClientOrderStocks { get; set; }
        public DbSet<PDFHelpDoc> PDFHelpDocs { get; set; }
        public DbSet<RepairOrders> RepairOrders { get; set; }
        public DbSet<ChatBotInteraction> ChatBotInteractions { get; set; }
        public DbSet<AuditTrail> AuditTrails { get; set; }
        public DbSet<TransitPrinter> TransitPrinters{ get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Role>().ToTable("AspNetRoles").HasKey(r => r.Id);

            //ErrorLog
            modelBuilder.Entity<ErrorLog>()
               .HasKey(e => new { e.errorLogId});

            modelBuilder.Entity<ErrorLog>()
                .HasOne(e => e.clientUser)
                .WithMany()
                .HasForeignKey(e => e.clientUserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ErrorLog>()
                .HasOne(e => e.errorCode)
                .WithMany()
                .HasForeignKey(e => e.errorCodeId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ErrorLog>()
               .HasOne(e => e.assignedPrinter)
               .WithMany()
               .HasForeignKey(e => e.assignedPrinterId)
               .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ErrorLog>()
               .HasOne(e => e.errorLogStatus)
               .WithMany()
               .HasForeignKey(e => e.errorLogStatusId)
               .OnDelete(DeleteBehavior.Restrict);

            //Branch
            modelBuilder.Entity<Branch>()
               .HasKey(e => new { e.branchId });

            modelBuilder.Entity<Branch>()
                .HasOne(e => e.client)
                .WithMany()
                .HasForeignKey(e => e.clientId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Branch>()
                .HasOne(e => e.city)
                .WithMany()
                .HasForeignKey(e => e.cityId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Branch>()
               .HasOne(e => e.assignedPrinter)
               .WithMany()
               .HasForeignKey(e => e.assignedPrinterId)
               .OnDelete(DeleteBehavior.Restrict);

            //RepairDiagnostic
            modelBuilder.Entity<RepairDiagnostic>()
                .HasKey(e => new { e.repairId, e.diagnosticsId });

            modelBuilder.Entity<RepairDiagnostic>()
                .HasOne(e => e.repair)
                .WithMany()
                .HasForeignKey(e => e.repairId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<RepairDiagnostic>()
                .HasOne(e => e.diagnostics)
                .WithMany()
                .HasForeignKey(e => e.diagnosticsId)
                .OnDelete(DeleteBehavior.Cascade);

            //RepairStock
            modelBuilder.Entity<RepairStock>()
                .HasKey(e => new { e.repairId, e.stockId, e.purchaseOrderId });

            modelBuilder.Entity<RepairStock>()
                .HasOne(e => e.repair)
                .WithMany()
                .HasForeignKey(e => e.repairId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<RepairStock>()
                .HasOne(e => e.stock)
                .WithMany()
                .HasForeignKey(e => e.stockId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<RepairStock>()
                .HasOne(e => e.purchaseOrder)
                .WithMany()
                .HasForeignKey(e => e.purchaseOrderId)
                .OnDelete(DeleteBehavior.Restrict);

            //Repair
            modelBuilder.Entity<Repair>()
                .HasKey(e => new { e.repairId});

            modelBuilder.Entity<Repair>()
                .HasOne(e => e.errorLog)
                .WithMany()
                .HasForeignKey(e => e.errorLogId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Repair>()
                .HasOne(e => e.repairStatus)
                .WithMany()
                .HasForeignKey(e => e.repairStatusId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Repair>()
                .HasOne(e => e.employee)
                .WithMany()
                .HasForeignKey(e => e.employeeId)
                .OnDelete(DeleteBehavior.Restrict);

            //AssignedTechnican
            modelBuilder.Entity<AssignedTechnician>()
                .HasKey(e => new { e.errorLogId, e.employeeId,});

            modelBuilder.Entity<AssignedTechnician>()
                .HasOne(e => e.errorLog)
                .WithMany()
                .HasForeignKey(e => e.errorLogId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<AssignedTechnician>()
                .HasOne(e => e.employee)
                .WithMany()
                .HasForeignKey(e => e.employeeId)
                .OnDelete(DeleteBehavior.Restrict);

            //Diagnostics
            modelBuilder.Entity<Diagnostics>()
                .HasKey(e => new { e.diagnosticsId, });

            modelBuilder.Entity<Diagnostics>()
                .HasOne(e => e.repair)
                .WithMany()
                .HasForeignKey(e => e.repairId)
                .OnDelete(DeleteBehavior.Restrict);


            //PartsRequest
            modelBuilder.Entity<PartsRequest>()
                .HasKey(e => new { e.stockId, e.purchaseOrderId });

            modelBuilder.Entity<PartsRequest>()
                .HasOne(e => e.stock)
                .WithMany()
                .HasForeignKey(e => e.stockId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<PartsRequest>()
                .HasOne(e => e.purchaseOrder)
                .WithMany()
                .HasForeignKey(e => e.purchaseOrderId)
                .OnDelete(DeleteBehavior.Cascade);

            //StockTakeStock
            modelBuilder.Entity<StockTakeStock>()
                .HasKey(e => new { e.stockId, e.stockTakeId });

            modelBuilder.Entity<StockTakeStock>()
                .HasOne(e => e.stock)
                .WithMany()
                .HasForeignKey(e => e.stockId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<StockTakeStock>()
                .HasOne(e => e.stockTake)
                .WithMany()
                .HasForeignKey(e => e.stockTakeId)
                .OnDelete(DeleteBehavior.Cascade);

            //StockSupplierOrder
            modelBuilder.Entity<StockSupplierOrder>()
                .HasKey(e => new { e.stockId, e.supplierOrderId });

            modelBuilder.Entity<StockSupplierOrder>()
                .HasOne(e => e.stock)
                .WithMany()
                .HasForeignKey(e => e.stockId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<StockSupplierOrder>()
                .HasOne(e => e.supplierOrder)
                .WithMany()
                .HasForeignKey(e => e.supplierOrderId)
                .OnDelete(DeleteBehavior.Cascade);

            //ClientOrderStock
            modelBuilder.Entity<ClientOrderStock>()
                .HasKey(e => new { e.stockId, e.clientOrderId });

            modelBuilder.Entity<ClientOrderStock>()
                .HasOne(e => e.stock)
                .WithMany()
                .HasForeignKey(e => e.stockId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ClientOrderStock>()
                .HasOne(e => e.clientOrder)
                .WithMany()
                .HasForeignKey(e => e.clientOrderId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
