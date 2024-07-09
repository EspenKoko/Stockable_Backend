using Stockable_Backend.Repository.IRepositories;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Stockable_Backend.Model;
using Stockable_Backend.Repository;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddCors(options => options.AddDefaultPolicy(
    builder =>
    {
        builder.AllowAnyOrigin();
        builder.AllowAnyHeader();
        builder.AllowAnyMethod();
    }));


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
/*builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Example API",
        Version = "v1",
        Description = "An example of an ASP.NET Core Web API",
        Contact = new OpenApiContact
        {
            Name = "Example Contact",
            Email = "example@example.com",
            Url = new Uri("https://example.com/contact"),
        },
    });

});*/

// Configure the token options for UserManager
builder.Services.Configure<DataProtectionTokenProviderOptions>(options =>
{
    options.TokenLifespan = TimeSpan.FromMinutes(30); // Set the token expiration time
});

// password validation
builder.Services.AddIdentity<User, Role>(options =>
{
    options.User.RequireUniqueEmail = true;
    options.Password.RequiredLength = 8;
    options.Password.RequireDigit = true;
    options.Password.RequireUppercase = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireNonAlphanumeric = true;
})
.AddEntityFrameworkStores<AppDbContext>()
.AddDefaultTokenProviders();

builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Add Bearer Token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "bearer"
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
                    {
                        {
                            new OpenApiSecurityScheme
                            {
                                Reference=new OpenApiReference
                                {
                                    Type=ReferenceType.SecurityScheme,
                                    Id = "Bearer"
                                }
                            },
                            new string[]{ }
                        }
                    });
});

// jwt token settings
builder.Services.AddAuthentication()
                .AddCookie()
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidIssuer = builder.Configuration["Tokens:Issuer"],
                        ValidAudience = builder.Configuration["Tokens:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Tokens:Key"]))
                    };
                });

builder.Services.Configure<FormOptions>(o =>
{
    o.ValueLengthLimit = int.MaxValue;
    o.MultipartBodyLengthLimit = int.MaxValue;
    o.MemoryBufferThreshold = int.MaxValue;
});

//Db
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<IBranchRepository, BranchRepository>();
builder.Services.AddScoped<ICityRepository, CityRepository>();
builder.Services.AddScoped<IClientRepository, ClientRepository>();
builder.Services.AddScoped<IClientUserRepository, ClientUserRepository>();
builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
builder.Services.AddScoped<IEmployeeTypeRepository, EmployeeTypeRepository>();
builder.Services.AddScoped<IErrorCodeRepository, ErrorCodeRepository>();
builder.Services.AddScoped<IHubRepository, HubRepository>();
builder.Services.AddScoped<IHubUserRepository, HubUserRepository>();
builder.Services.AddScoped<IPriceRepository, PriceRepository>();
builder.Services.AddScoped<IAssignedPrinterRepository, AssignedPrinterRepository>();
//builder.Services.AddScoped<IPrinterModelRepository, PrinterModelRepository>();
builder.Services.AddScoped<IPrinterStatusRepository, PrinterStatusRepository>();
builder.Services.AddScoped<IProvinceRepository, ProvinceRepository>();
builder.Services.AddScoped<IStockRepository, StockRepository>();
builder.Services.AddScoped<IStockCategoryRepository, StockCategoryRepository>();
builder.Services.AddScoped<IStockTypeRepository, StockTypeRepository>();
builder.Services.AddScoped<ISupplierRepository, SupplierRepository>();
builder.Services.AddScoped<IVatRepository, VatRepository>();
builder.Services.AddScoped<IHelpRepository, HelpRepository>();
builder.Services.AddScoped<IErrorLogRepository, ErrorLogRepository>();
builder.Services.AddScoped<IErrorLogStatusRepository, ErrorLogStatusRepository>();
//builder.Services.AddScoped<IErrorLogErrorCodesRepository, ErrorLogErrorCodesRepository>();
builder.Services.AddScoped<IDiagnosticsRepository, DiagnosticsRepository>();
builder.Services.AddScoped<IRepairStatusRepository, RepairStatusRepository>();
builder.Services.AddScoped<IRepairRepository, RepairRepository>();
builder.Services.AddScoped<IPurchaseOrderRepository, PurchaseOrderRepository>();
builder.Services.AddScoped<IRepairDiagnosticRepository, RepairDiagnosticRepository>();
builder.Services.AddScoped<IRepairStockRepository, RepairStockRepository>();
builder.Services.AddScoped<IPaymentTypeRepository, PaymentTypeRepository>();
builder.Services.AddScoped<IClientInvoiceRepository, ClientInvoiceRepository>();
builder.Services.AddScoped<IClientOrderRepository, ClientOrderRepository>();
builder.Services.AddScoped<IClientOrderStatusRepository, ClientOrderStatusRepository>();
builder.Services.AddScoped<IClientUserRequestRepository, ClientUserRequestRepository>();
builder.Services.AddScoped<ISupplierOrderStatusRepository, SupplierOrderStatusRepository>();
builder.Services.AddScoped<ISupplierOrderRepository, SupplierOrderRepository>();
builder.Services.AddScoped<IStockTakeRepository, StockTakeRepository>();
builder.Services.AddScoped<IStockTakeStockRepository, StockTakeStockRepository>();
builder.Services.AddScoped<ITechnicalServiceReportRepository, TechnicalServiceReportRepository>();
builder.Services.AddScoped<IPartsRequestRepository, PartsRequestRepository>();
builder.Services.AddScoped<ILabourRateRepository, LabourRateRepository>();
builder.Services.AddScoped<IStockSupplierOrderRepository, StockSupplierOrderRepository>();
builder.Services.AddScoped<IPurchaseOrderStatusRepository, PurchaseOrderStatusRepository>();
builder.Services.AddScoped<IAssignedTechnicianRepository, AssignedTechnicianRepository>();
builder.Services.AddScoped<IClientOrderStockRepository, ClientOrderStockRepository>();
builder.Services.AddScoped<IChatBotInteractionRepository, ChatBotInteractionRepository>();
builder.Services.AddScoped<IAuditTrailRepository, AuditTrailRepository>();
builder.Services.AddScoped<IMarkupRepository, MarkupRepository>();
builder.Services.AddScoped<IEmailRepository, EmailRepository>();
builder.Services.AddScoped<IPDFHelpDocRepository, PDFHelpDocRepository>();
builder.Services.AddScoped<IAuditTrailRepository, AuditTrailRepository>();
builder.Services.AddScoped<ITransitPrinterRepository, TransitPrinterRepository>();
//builder.Services.AddTransient<IEmailRepository, EmailRepository>(); // depending on how emails will be used


var app = builder.Build();

// Create roles
using (var scope = app.Services.CreateScope())
{
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<Role>>();

    // Define the roles you want to create
    var roles = new[] { "SUPER_ADMIN", "ADMIN", "CLIENT_USER", "HUB_USER", "CLIENT_ADMIN", "TECHNICIAN", "INVENTORY_CLERK" };

    foreach (var role in roles)
    {
        var roleExists = roleManager.RoleExistsAsync(role).GetAwaiter().GetResult();

        if (!roleExists)
        {
            roleManager.CreateAsync(new Role { Name = role }).GetAwaiter().GetResult();
        }
    }
}

// Calls default user creation
CreateDefaultUser(app.Services, builder.Configuration);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    //v2
/*    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
    });*/
}
app.UseCors();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();


// Create the default user if it doesn't exist
static void CreateDefaultUser(IServiceProvider serviceProvider, IConfiguration configuration)
{
    var scopeFactory = serviceProvider.GetRequiredService<IServiceScopeFactory>();
    using (var scope = scopeFactory.CreateScope())
    {
        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
        var defaultUserEmail = configuration["DefaultUser:Email"];
        var defaultUserPassword = configuration["DefaultUser:Password"];

        var defaultUser = userManager.FindByEmailAsync(defaultUserEmail).GetAwaiter().GetResult();
        if (defaultUser == null)
        {
            defaultUser = new User
            {
                UserName = defaultUserEmail,
                Email = defaultUserEmail,
                userFirstName = "Admin",
                userLastName = "Admin",
                userType = "Initial_User",
                PhoneNumber = "0000000000"
            };

            var result = userManager.CreateAsync(defaultUser, defaultUserPassword).GetAwaiter().GetResult();
            if (result.Succeeded)
            {
                // You can add the default user to roles if needed
                userManager.AddToRoleAsync(defaultUser, "SUPER_ADMIN").GetAwaiter().GetResult();
            }
            else
            {
                var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                Console.WriteLine($"Error creating default user: {errors}");
            }
        }
    }
}
