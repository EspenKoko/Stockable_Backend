using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Stockable_Backend.Migrations
{
    public partial class test : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    userFirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    userLastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    userType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    profilePicture = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AuditTrails",
                columns: table => new
                {
                    auditTrailId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    userId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    userName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    userAction = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuditTrails", x => x.auditTrailId);
                });

            migrationBuilder.CreateTable(
                name: "ChatBotInteractions",
                columns: table => new
                {
                    botInteractionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    message = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    type = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChatBotInteractions", x => x.botInteractionId);
                });

            migrationBuilder.CreateTable(
                name: "ClientInvoices",
                columns: table => new
                {
                    clientInvoiceId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    clientInvoiceNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    clientInvoiceDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientInvoices", x => x.clientInvoiceId);
                });

            migrationBuilder.CreateTable(
                name: "ClientOrderStatus",
                columns: table => new
                {
                    clientOrderStatusId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    clientOrderStatusName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientOrderStatus", x => x.clientOrderStatusId);
                });

            migrationBuilder.CreateTable(
                name: "Clients",
                columns: table => new
                {
                    clientId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    clientName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    clientNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    clientEmail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    clientAddress = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clients", x => x.clientId);
                });

            migrationBuilder.CreateTable(
                name: "EmployeeTypes",
                columns: table => new
                {
                    employeeTypeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    employeeTypeName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    employeeTypeDescription = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeTypes", x => x.employeeTypeId);
                });

            migrationBuilder.CreateTable(
                name: "ErrorCodes",
                columns: table => new
                {
                    errorCodeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    errorCodeName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    errorCodeDescription = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ErrorCodes", x => x.errorCodeId);
                });

            migrationBuilder.CreateTable(
                name: "ErrorLogStatuses",
                columns: table => new
                {
                    errorLogStatusId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    errorLogStatusName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ErrorLogStatuses", x => x.errorLogStatusId);
                });

            migrationBuilder.CreateTable(
                name: "Help",
                columns: table => new
                {
                    helpId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    helpName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    helpDescription = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Help", x => x.helpId);
                });

            migrationBuilder.CreateTable(
                name: "LabourRates",
                columns: table => new
                {
                    labourRateId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    labourRate = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    labourRateDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LabourRates", x => x.labourRateId);
                });

            migrationBuilder.CreateTable(
                name: "Markups",
                columns: table => new
                {
                    markupId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    markupPercent = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    markupDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Markups", x => x.markupId);
                });

            migrationBuilder.CreateTable(
                name: "PaymentTypes",
                columns: table => new
                {
                    paymentTypeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    paymentTypeName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentTypes", x => x.paymentTypeId);
                });

            migrationBuilder.CreateTable(
                name: "PDFHelpDocs",
                columns: table => new
                {
                    docId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    userType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    pdfContent = table.Column<byte[]>(type: "varbinary(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PDFHelpDocs", x => x.docId);
                });

            migrationBuilder.CreateTable(
                name: "PrinterStatuses",
                columns: table => new
                {
                    printerStatusId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    printerStatusName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PrinterStatuses", x => x.printerStatusId);
                });

            migrationBuilder.CreateTable(
                name: "Provinces",
                columns: table => new
                {
                    provinceId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    provinceName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Provinces", x => x.provinceId);
                });

            migrationBuilder.CreateTable(
                name: "PurchaseOrderStatuses",
                columns: table => new
                {
                    purchaseOrderStatusId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    purchaseOrderStatusName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PurchaseOrderStatuses", x => x.purchaseOrderStatusId);
                });

            migrationBuilder.CreateTable(
                name: "RepairOrders",
                columns: table => new
                {
                    repairOrderId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    vat = table.Column<int>(type: "int", nullable: false),
                    markUp = table.Column<int>(type: "int", nullable: false),
                    labourRate = table.Column<int>(type: "int", nullable: false),
                    date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    total = table.Column<int>(type: "int", nullable: false),
                    serialNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    client = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    branchCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    repairId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RepairOrders", x => x.repairOrderId);
                });

            migrationBuilder.CreateTable(
                name: "RepairStatuses",
                columns: table => new
                {
                    repairStatusId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    repairStatusName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RepairStatuses", x => x.repairStatusId);
                });

            migrationBuilder.CreateTable(
                name: "StockCategories",
                columns: table => new
                {
                    stockCategoryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    stockCategoryName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StockCategories", x => x.stockCategoryId);
                });

            migrationBuilder.CreateTable(
                name: "SupplierOrderStatuses",
                columns: table => new
                {
                    supplierOrderStatusId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    supplierOrderStatusName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SupplierOrderStatuses", x => x.supplierOrderStatusId);
                });

            migrationBuilder.CreateTable(
                name: "Suppliers",
                columns: table => new
                {
                    supplierId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    supplierName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    supplierAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    supplierContactNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    supplierEmail = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Suppliers", x => x.supplierId);
                });

            migrationBuilder.CreateTable(
                name: "TransitPrinters",
                columns: table => new
                {
                    transitPrinterId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    technicianId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    assignedPrinterId = table.Column<int>(type: "int", nullable: false),
                    errorLogId = table.Column<int>(type: "int", nullable: false),
                    date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransitPrinters", x => x.transitPrinterId);
                });

            migrationBuilder.CreateTable(
                name: "Vats",
                columns: table => new
                {
                    vatId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    vatPercent = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    vatDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vats", x => x.vatId);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    employeeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    empHireDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    employeeTypeId = table.Column<int>(type: "int", nullable: false),
                    userId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.employeeId);
                    table.ForeignKey(
                        name: "FK_Employees_AspNetUsers_userId",
                        column: x => x.userId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Employees_EmployeeTypes_employeeTypeId",
                        column: x => x.employeeTypeId,
                        principalTable: "EmployeeTypes",
                        principalColumn: "employeeTypeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AssignedPrinters",
                columns: table => new
                {
                    assignedPrinterId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    serialNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    printerModel = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    clientId = table.Column<int>(type: "int", nullable: false),
                    printerStatusId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssignedPrinters", x => x.assignedPrinterId);
                    table.ForeignKey(
                        name: "FK_AssignedPrinters_Clients_clientId",
                        column: x => x.clientId,
                        principalTable: "Clients",
                        principalColumn: "clientId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AssignedPrinters_PrinterStatuses_printerStatusId",
                        column: x => x.printerStatusId,
                        principalTable: "PrinterStatuses",
                        principalColumn: "printerStatusId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Cities",
                columns: table => new
                {
                    cityId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    cityName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    provinceId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cities", x => x.cityId);
                    table.ForeignKey(
                        name: "FK_Cities_Provinces_provinceId",
                        column: x => x.provinceId,
                        principalTable: "Provinces",
                        principalColumn: "provinceId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StockTypes",
                columns: table => new
                {
                    stockTypeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    stockTypeName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    stockCategoryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StockTypes", x => x.stockTypeId);
                    table.ForeignKey(
                        name: "FK_StockTypes_StockCategories_stockCategoryId",
                        column: x => x.stockCategoryId,
                        principalTable: "StockCategories",
                        principalColumn: "stockCategoryId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StockTakes",
                columns: table => new
                {
                    stockTakeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    stockTakeDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    employeeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StockTakes", x => x.stockTakeId);
                    table.ForeignKey(
                        name: "FK_StockTakes_Employees_employeeId",
                        column: x => x.employeeId,
                        principalTable: "Employees",
                        principalColumn: "employeeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SupplierOrders",
                columns: table => new
                {
                    supplierOrderId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    employeeId = table.Column<int>(type: "int", nullable: false),
                    supplierOrderStatusId = table.Column<int>(type: "int", nullable: false),
                    supplierId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SupplierOrders", x => x.supplierOrderId);
                    table.ForeignKey(
                        name: "FK_SupplierOrders_Employees_employeeId",
                        column: x => x.employeeId,
                        principalTable: "Employees",
                        principalColumn: "employeeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SupplierOrders_SupplierOrderStatuses_supplierOrderStatusId",
                        column: x => x.supplierOrderStatusId,
                        principalTable: "SupplierOrderStatuses",
                        principalColumn: "supplierOrderStatusId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SupplierOrders_Suppliers_supplierId",
                        column: x => x.supplierId,
                        principalTable: "Suppliers",
                        principalColumn: "supplierId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Branches",
                columns: table => new
                {
                    branchId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    branchName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    branchCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    clientId = table.Column<int>(type: "int", nullable: false),
                    cityId = table.Column<int>(type: "int", nullable: false),
                    assignedPrinterId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Branches", x => x.branchId);
                    table.ForeignKey(
                        name: "FK_Branches_AssignedPrinters_assignedPrinterId",
                        column: x => x.assignedPrinterId,
                        principalTable: "AssignedPrinters",
                        principalColumn: "assignedPrinterId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Branches_Cities_cityId",
                        column: x => x.cityId,
                        principalTable: "Cities",
                        principalColumn: "cityId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Branches_Clients_clientId",
                        column: x => x.clientId,
                        principalTable: "Clients",
                        principalColumn: "clientId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Hubs",
                columns: table => new
                {
                    hubId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    hubName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    qtyOnHand = table.Column<int>(type: "int", nullable: false),
                    hubPrinterThreshold = table.Column<int>(type: "int", nullable: false),
                    cityId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Hubs", x => x.hubId);
                    table.ForeignKey(
                        name: "FK_Hubs_Cities_cityId",
                        column: x => x.cityId,
                        principalTable: "Cities",
                        principalColumn: "cityId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Stocks",
                columns: table => new
                {
                    stockId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    stockName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    stockDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    qtyOnHand = table.Column<int>(type: "int", nullable: false),
                    minStockThreshold = table.Column<int>(type: "int", nullable: false),
                    maxStockThreshold = table.Column<int>(type: "int", nullable: false),
                    stockTypeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stocks", x => x.stockId);
                    table.ForeignKey(
                        name: "FK_Stocks_StockTypes_stockTypeId",
                        column: x => x.stockTypeId,
                        principalTable: "StockTypes",
                        principalColumn: "stockTypeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ClientUserRequests",
                columns: table => new
                {
                    clientUserRequestId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    surname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    number = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    clientUserPosition = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    role = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    userCreated = table.Column<bool>(type: "bit", nullable: false),
                    branchId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientUserRequests", x => x.clientUserRequestId);
                    table.ForeignKey(
                        name: "FK_ClientUserRequests_Branches_branchId",
                        column: x => x.branchId,
                        principalTable: "Branches",
                        principalColumn: "branchId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ClientUsers",
                columns: table => new
                {
                    clientUserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    clientUserPosition = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    clientId = table.Column<int>(type: "int", nullable: false),
                    userId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    branchId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientUsers", x => x.clientUserId);
                    table.ForeignKey(
                        name: "FK_ClientUsers_AspNetUsers_userId",
                        column: x => x.userId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ClientUsers_Branches_branchId",
                        column: x => x.branchId,
                        principalTable: "Branches",
                        principalColumn: "branchId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ClientUsers_Clients_clientId",
                        column: x => x.clientId,
                        principalTable: "Clients",
                        principalColumn: "clientId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HubUsers",
                columns: table => new
                {
                    hubUserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    hubUserName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    hubUserSurname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    hubUserPhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    hubUserEmail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    hubUserPostion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    userId = table.Column<int>(type: "int", nullable: false),
                    userId1 = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    hubId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HubUsers", x => x.hubUserId);
                    table.ForeignKey(
                        name: "FK_HubUsers_AspNetUsers_userId1",
                        column: x => x.userId1,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_HubUsers_Hubs_hubId",
                        column: x => x.hubId,
                        principalTable: "Hubs",
                        principalColumn: "hubId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Prices",
                columns: table => new
                {
                    priceId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    price = table.Column<int>(type: "int", nullable: false),
                    priceDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    stockId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Prices", x => x.priceId);
                    table.ForeignKey(
                        name: "FK_Prices_Stocks_stockId",
                        column: x => x.stockId,
                        principalTable: "Stocks",
                        principalColumn: "stockId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StockSupplierOrders",
                columns: table => new
                {
                    stockId = table.Column<int>(type: "int", nullable: false),
                    supplierOrderId = table.Column<int>(type: "int", nullable: false),
                    qty = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StockSupplierOrders", x => new { x.stockId, x.supplierOrderId });
                    table.ForeignKey(
                        name: "FK_StockSupplierOrders_Stocks_stockId",
                        column: x => x.stockId,
                        principalTable: "Stocks",
                        principalColumn: "stockId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StockSupplierOrders_SupplierOrders_supplierOrderId",
                        column: x => x.supplierOrderId,
                        principalTable: "SupplierOrders",
                        principalColumn: "supplierOrderId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StockTakeStock",
                columns: table => new
                {
                    stockId = table.Column<int>(type: "int", nullable: false),
                    stockTakeId = table.Column<int>(type: "int", nullable: false),
                    qty = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StockTakeStock", x => new { x.stockId, x.stockTakeId });
                    table.ForeignKey(
                        name: "FK_StockTakeStock_Stocks_stockId",
                        column: x => x.stockId,
                        principalTable: "Stocks",
                        principalColumn: "stockId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StockTakeStock_StockTakes_stockTakeId",
                        column: x => x.stockTakeId,
                        principalTable: "StockTakes",
                        principalColumn: "stockTakeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ClientOrders",
                columns: table => new
                {
                    clientOrderId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    clientOrderStatusId = table.Column<int>(type: "int", nullable: false),
                    paymentTypeId = table.Column<int>(type: "int", nullable: false),
                    clientInvoiceId = table.Column<int>(type: "int", nullable: false),
                    clientUserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientOrders", x => x.clientOrderId);
                    table.ForeignKey(
                        name: "FK_ClientOrders_ClientInvoices_clientInvoiceId",
                        column: x => x.clientInvoiceId,
                        principalTable: "ClientInvoices",
                        principalColumn: "clientInvoiceId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ClientOrders_ClientOrderStatus_clientOrderStatusId",
                        column: x => x.clientOrderStatusId,
                        principalTable: "ClientOrderStatus",
                        principalColumn: "clientOrderStatusId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ClientOrders_ClientUsers_clientUserId",
                        column: x => x.clientUserId,
                        principalTable: "ClientUsers",
                        principalColumn: "clientUserId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ClientOrders_PaymentTypes_paymentTypeId",
                        column: x => x.paymentTypeId,
                        principalTable: "PaymentTypes",
                        principalColumn: "paymentTypeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ErrorLogs",
                columns: table => new
                {
                    errorLogId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    errorLogDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    errorLogDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    errorLogStatusId = table.Column<int>(type: "int", nullable: false),
                    clientUserId = table.Column<int>(type: "int", nullable: false),
                    assignedPrinterId = table.Column<int>(type: "int", nullable: false),
                    errorCodeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ErrorLogs", x => x.errorLogId);
                    table.ForeignKey(
                        name: "FK_ErrorLogs_AssignedPrinters_assignedPrinterId",
                        column: x => x.assignedPrinterId,
                        principalTable: "AssignedPrinters",
                        principalColumn: "assignedPrinterId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ErrorLogs_ClientUsers_clientUserId",
                        column: x => x.clientUserId,
                        principalTable: "ClientUsers",
                        principalColumn: "clientUserId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ErrorLogs_ErrorCodes_errorCodeId",
                        column: x => x.errorCodeId,
                        principalTable: "ErrorCodes",
                        principalColumn: "errorCodeId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ErrorLogs_ErrorLogStatuses_errorLogStatusId",
                        column: x => x.errorLogStatusId,
                        principalTable: "ErrorLogStatuses",
                        principalColumn: "errorLogStatusId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ClientOrderStocks",
                columns: table => new
                {
                    stockId = table.Column<int>(type: "int", nullable: false),
                    clientOrderId = table.Column<int>(type: "int", nullable: false),
                    qty = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientOrderStocks", x => new { x.stockId, x.clientOrderId });
                    table.ForeignKey(
                        name: "FK_ClientOrderStocks_ClientOrders_clientOrderId",
                        column: x => x.clientOrderId,
                        principalTable: "ClientOrders",
                        principalColumn: "clientOrderId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ClientOrderStocks_Stocks_stockId",
                        column: x => x.stockId,
                        principalTable: "Stocks",
                        principalColumn: "stockId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AssignedTechnicians",
                columns: table => new
                {
                    errorLogId = table.Column<int>(type: "int", nullable: false),
                    employeeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssignedTechnicians", x => new { x.errorLogId, x.employeeId });
                    table.ForeignKey(
                        name: "FK_AssignedTechnicians_Employees_employeeId",
                        column: x => x.employeeId,
                        principalTable: "Employees",
                        principalColumn: "employeeId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AssignedTechnicians_ErrorLogs_errorLogId",
                        column: x => x.errorLogId,
                        principalTable: "ErrorLogs",
                        principalColumn: "errorLogId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Repairs",
                columns: table => new
                {
                    repairId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    errorLogId = table.Column<int>(type: "int", nullable: false),
                    repairStatusId = table.Column<int>(type: "int", nullable: false),
                    employeeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Repairs", x => x.repairId);
                    table.ForeignKey(
                        name: "FK_Repairs_Employees_employeeId",
                        column: x => x.employeeId,
                        principalTable: "Employees",
                        principalColumn: "employeeId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Repairs_ErrorLogs_errorLogId",
                        column: x => x.errorLogId,
                        principalTable: "ErrorLogs",
                        principalColumn: "errorLogId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Repairs_RepairStatuses_repairStatusId",
                        column: x => x.repairStatusId,
                        principalTable: "RepairStatuses",
                        principalColumn: "repairStatusId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Diagnostics",
                columns: table => new
                {
                    diagnosticsId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    diagnosticComment = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    rollerCheck = table.Column<bool>(type: "bit", nullable: false),
                    lcdScreenCheck = table.Column<bool>(type: "bit", nullable: false),
                    powerSupplyCheck = table.Column<bool>(type: "bit", nullable: false),
                    motherboardCheck = table.Column<bool>(type: "bit", nullable: false),
                    hopperCheck = table.Column<bool>(type: "bit", nullable: false),
                    beltCheck = table.Column<bool>(type: "bit", nullable: false),
                    ethernetPortCheck = table.Column<bool>(type: "bit", nullable: false),
                    repairId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Diagnostics", x => x.diagnosticsId);
                    table.ForeignKey(
                        name: "FK_Diagnostics_Repairs_repairId",
                        column: x => x.repairId,
                        principalTable: "Repairs",
                        principalColumn: "repairId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PurchaseOrders",
                columns: table => new
                {
                    purchaseOrderId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    purchaseOrderDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    repairTime = table.Column<int>(type: "int", nullable: false),
                    repairId = table.Column<int>(type: "int", nullable: false),
                    purchaseOrderStatusId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PurchaseOrders", x => x.purchaseOrderId);
                    table.ForeignKey(
                        name: "FK_PurchaseOrders_PurchaseOrderStatuses_purchaseOrderStatusId",
                        column: x => x.purchaseOrderStatusId,
                        principalTable: "PurchaseOrderStatuses",
                        principalColumn: "purchaseOrderStatusId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PurchaseOrders_Repairs_repairId",
                        column: x => x.repairId,
                        principalTable: "Repairs",
                        principalColumn: "repairId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RepairDiagnostics",
                columns: table => new
                {
                    repairId = table.Column<int>(type: "int", nullable: false),
                    diagnosticsId = table.Column<int>(type: "int", nullable: false),
                    isComplete = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RepairDiagnostics", x => new { x.repairId, x.diagnosticsId });
                    table.ForeignKey(
                        name: "FK_RepairDiagnostics_Diagnostics_diagnosticsId",
                        column: x => x.diagnosticsId,
                        principalTable: "Diagnostics",
                        principalColumn: "diagnosticsId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RepairDiagnostics_Repairs_repairId",
                        column: x => x.repairId,
                        principalTable: "Repairs",
                        principalColumn: "repairId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PartsRequests",
                columns: table => new
                {
                    stockId = table.Column<int>(type: "int", nullable: false),
                    purchaseOrderId = table.Column<int>(type: "int", nullable: false),
                    qty = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PartsRequests", x => new { x.stockId, x.purchaseOrderId });
                    table.ForeignKey(
                        name: "FK_PartsRequests_PurchaseOrders_purchaseOrderId",
                        column: x => x.purchaseOrderId,
                        principalTable: "PurchaseOrders",
                        principalColumn: "purchaseOrderId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PartsRequests_Stocks_stockId",
                        column: x => x.stockId,
                        principalTable: "Stocks",
                        principalColumn: "stockId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RepairStocks",
                columns: table => new
                {
                    stockId = table.Column<int>(type: "int", nullable: false),
                    repairId = table.Column<int>(type: "int", nullable: false),
                    purchaseOrderId = table.Column<int>(type: "int", nullable: false),
                    qty = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RepairStocks", x => new { x.repairId, x.stockId, x.purchaseOrderId });
                    table.ForeignKey(
                        name: "FK_RepairStocks_PurchaseOrders_purchaseOrderId",
                        column: x => x.purchaseOrderId,
                        principalTable: "PurchaseOrders",
                        principalColumn: "purchaseOrderId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RepairStocks_Repairs_repairId",
                        column: x => x.repairId,
                        principalTable: "Repairs",
                        principalColumn: "repairId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RepairStocks_Stocks_stockId",
                        column: x => x.stockId,
                        principalTable: "Stocks",
                        principalColumn: "stockId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TechnicalServiceReports",
                columns: table => new
                {
                    technicalServiceReportId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    purchaseOrderId = table.Column<int>(type: "int", nullable: false),
                    repairsDone = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TechnicalServiceReports", x => x.technicalServiceReportId);
                    table.ForeignKey(
                        name: "FK_TechnicalServiceReports_PurchaseOrders_purchaseOrderId",
                        column: x => x.purchaseOrderId,
                        principalTable: "PurchaseOrders",
                        principalColumn: "purchaseOrderId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AssignedPrinters_clientId",
                table: "AssignedPrinters",
                column: "clientId");

            migrationBuilder.CreateIndex(
                name: "IX_AssignedPrinters_printerStatusId",
                table: "AssignedPrinters",
                column: "printerStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_AssignedTechnicians_employeeId",
                table: "AssignedTechnicians",
                column: "employeeId");

            migrationBuilder.CreateIndex(
                name: "IX_Branches_assignedPrinterId",
                table: "Branches",
                column: "assignedPrinterId");

            migrationBuilder.CreateIndex(
                name: "IX_Branches_cityId",
                table: "Branches",
                column: "cityId");

            migrationBuilder.CreateIndex(
                name: "IX_Branches_clientId",
                table: "Branches",
                column: "clientId");

            migrationBuilder.CreateIndex(
                name: "IX_Cities_provinceId",
                table: "Cities",
                column: "provinceId");

            migrationBuilder.CreateIndex(
                name: "IX_ClientOrders_clientInvoiceId",
                table: "ClientOrders",
                column: "clientInvoiceId");

            migrationBuilder.CreateIndex(
                name: "IX_ClientOrders_clientOrderStatusId",
                table: "ClientOrders",
                column: "clientOrderStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_ClientOrders_clientUserId",
                table: "ClientOrders",
                column: "clientUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ClientOrders_paymentTypeId",
                table: "ClientOrders",
                column: "paymentTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_ClientOrderStocks_clientOrderId",
                table: "ClientOrderStocks",
                column: "clientOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_ClientUserRequests_branchId",
                table: "ClientUserRequests",
                column: "branchId");

            migrationBuilder.CreateIndex(
                name: "IX_ClientUsers_branchId",
                table: "ClientUsers",
                column: "branchId");

            migrationBuilder.CreateIndex(
                name: "IX_ClientUsers_clientId",
                table: "ClientUsers",
                column: "clientId");

            migrationBuilder.CreateIndex(
                name: "IX_ClientUsers_userId",
                table: "ClientUsers",
                column: "userId");

            migrationBuilder.CreateIndex(
                name: "IX_Diagnostics_repairId",
                table: "Diagnostics",
                column: "repairId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_employeeTypeId",
                table: "Employees",
                column: "employeeTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_userId",
                table: "Employees",
                column: "userId");

            migrationBuilder.CreateIndex(
                name: "IX_ErrorLogs_assignedPrinterId",
                table: "ErrorLogs",
                column: "assignedPrinterId");

            migrationBuilder.CreateIndex(
                name: "IX_ErrorLogs_clientUserId",
                table: "ErrorLogs",
                column: "clientUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ErrorLogs_errorCodeId",
                table: "ErrorLogs",
                column: "errorCodeId");

            migrationBuilder.CreateIndex(
                name: "IX_ErrorLogs_errorLogStatusId",
                table: "ErrorLogs",
                column: "errorLogStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Hubs_cityId",
                table: "Hubs",
                column: "cityId");

            migrationBuilder.CreateIndex(
                name: "IX_HubUsers_hubId",
                table: "HubUsers",
                column: "hubId");

            migrationBuilder.CreateIndex(
                name: "IX_HubUsers_userId1",
                table: "HubUsers",
                column: "userId1");

            migrationBuilder.CreateIndex(
                name: "IX_PartsRequests_purchaseOrderId",
                table: "PartsRequests",
                column: "purchaseOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Prices_stockId",
                table: "Prices",
                column: "stockId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseOrders_purchaseOrderStatusId",
                table: "PurchaseOrders",
                column: "purchaseOrderStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseOrders_repairId",
                table: "PurchaseOrders",
                column: "repairId");

            migrationBuilder.CreateIndex(
                name: "IX_RepairDiagnostics_diagnosticsId",
                table: "RepairDiagnostics",
                column: "diagnosticsId");

            migrationBuilder.CreateIndex(
                name: "IX_Repairs_employeeId",
                table: "Repairs",
                column: "employeeId");

            migrationBuilder.CreateIndex(
                name: "IX_Repairs_errorLogId",
                table: "Repairs",
                column: "errorLogId");

            migrationBuilder.CreateIndex(
                name: "IX_Repairs_repairStatusId",
                table: "Repairs",
                column: "repairStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_RepairStocks_purchaseOrderId",
                table: "RepairStocks",
                column: "purchaseOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_RepairStocks_stockId",
                table: "RepairStocks",
                column: "stockId");

            migrationBuilder.CreateIndex(
                name: "IX_Stocks_stockTypeId",
                table: "Stocks",
                column: "stockTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_StockSupplierOrders_supplierOrderId",
                table: "StockSupplierOrders",
                column: "supplierOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_StockTakes_employeeId",
                table: "StockTakes",
                column: "employeeId");

            migrationBuilder.CreateIndex(
                name: "IX_StockTakeStock_stockTakeId",
                table: "StockTakeStock",
                column: "stockTakeId");

            migrationBuilder.CreateIndex(
                name: "IX_StockTypes_stockCategoryId",
                table: "StockTypes",
                column: "stockCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_SupplierOrders_employeeId",
                table: "SupplierOrders",
                column: "employeeId");

            migrationBuilder.CreateIndex(
                name: "IX_SupplierOrders_supplierId",
                table: "SupplierOrders",
                column: "supplierId");

            migrationBuilder.CreateIndex(
                name: "IX_SupplierOrders_supplierOrderStatusId",
                table: "SupplierOrders",
                column: "supplierOrderStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_TechnicalServiceReports_purchaseOrderId",
                table: "TechnicalServiceReports",
                column: "purchaseOrderId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "AssignedTechnicians");

            migrationBuilder.DropTable(
                name: "AuditTrails");

            migrationBuilder.DropTable(
                name: "ChatBotInteractions");

            migrationBuilder.DropTable(
                name: "ClientOrderStocks");

            migrationBuilder.DropTable(
                name: "ClientUserRequests");

            migrationBuilder.DropTable(
                name: "Help");

            migrationBuilder.DropTable(
                name: "HubUsers");

            migrationBuilder.DropTable(
                name: "LabourRates");

            migrationBuilder.DropTable(
                name: "Markups");

            migrationBuilder.DropTable(
                name: "PartsRequests");

            migrationBuilder.DropTable(
                name: "PDFHelpDocs");

            migrationBuilder.DropTable(
                name: "Prices");

            migrationBuilder.DropTable(
                name: "RepairDiagnostics");

            migrationBuilder.DropTable(
                name: "RepairOrders");

            migrationBuilder.DropTable(
                name: "RepairStocks");

            migrationBuilder.DropTable(
                name: "StockSupplierOrders");

            migrationBuilder.DropTable(
                name: "StockTakeStock");

            migrationBuilder.DropTable(
                name: "TechnicalServiceReports");

            migrationBuilder.DropTable(
                name: "TransitPrinters");

            migrationBuilder.DropTable(
                name: "Vats");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "ClientOrders");

            migrationBuilder.DropTable(
                name: "Hubs");

            migrationBuilder.DropTable(
                name: "Diagnostics");

            migrationBuilder.DropTable(
                name: "SupplierOrders");

            migrationBuilder.DropTable(
                name: "Stocks");

            migrationBuilder.DropTable(
                name: "StockTakes");

            migrationBuilder.DropTable(
                name: "PurchaseOrders");

            migrationBuilder.DropTable(
                name: "ClientInvoices");

            migrationBuilder.DropTable(
                name: "ClientOrderStatus");

            migrationBuilder.DropTable(
                name: "PaymentTypes");

            migrationBuilder.DropTable(
                name: "SupplierOrderStatuses");

            migrationBuilder.DropTable(
                name: "Suppliers");

            migrationBuilder.DropTable(
                name: "StockTypes");

            migrationBuilder.DropTable(
                name: "PurchaseOrderStatuses");

            migrationBuilder.DropTable(
                name: "Repairs");

            migrationBuilder.DropTable(
                name: "StockCategories");

            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropTable(
                name: "ErrorLogs");

            migrationBuilder.DropTable(
                name: "RepairStatuses");

            migrationBuilder.DropTable(
                name: "EmployeeTypes");

            migrationBuilder.DropTable(
                name: "ClientUsers");

            migrationBuilder.DropTable(
                name: "ErrorCodes");

            migrationBuilder.DropTable(
                name: "ErrorLogStatuses");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Branches");

            migrationBuilder.DropTable(
                name: "AssignedPrinters");

            migrationBuilder.DropTable(
                name: "Cities");

            migrationBuilder.DropTable(
                name: "Clients");

            migrationBuilder.DropTable(
                name: "PrinterStatuses");

            migrationBuilder.DropTable(
                name: "Provinces");
        }
    }
}
