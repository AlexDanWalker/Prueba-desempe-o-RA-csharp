using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using TalentPlus.Application.Interfaces;
using TalentPlus.Application.Interfaces.Repositories;
using TalentPlus.Application.Interfaces.Services;
using TalentPlus.Application.Services;
using TalentPlus.Infrastructure.Data;
using TalentPlus.Infrastructure.Email;
using TalentPlus.Infrastructure.Excel;
using TalentPlus.Infrastructure.Pdf;
using TalentPlus.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// 🔹 Conexión a la base de datos
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<TalentPlusDbContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

// 🔹 Configuración de Identity
builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<TalentPlusDbContext>()
    .AddDefaultTokenProviders();

// 🔹 Configuración de JWT
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(
            System.Text.Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"] ?? throw new InvalidOperationException("JWT Key is missing")))
    };
});

builder.Services.AddAuthorization();

// 🔹 Configuración de CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
    });
});

// 🔹 Configuración de Email
builder.Services.Configure<EmailSettings>(builder.Configuration.GetSection("EmailSettings"));
builder.Services.AddScoped<IEmailService, EmailService>();

// 🔹 Registro de repositorios
builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
builder.Services.AddScoped<IDepartmentRepository, DepartmentRepository>();
builder.Services.AddScoped<IPositionRepository, PositionRepository>();
builder.Services.AddScoped<IEducationalLevelRepository, EducationalLevelRepository>();

// 🔹 Registro de servicios de infraestructura (PDF, Excel)
builder.Services.AddScoped<IEmployeeExcelImporter, EmployeeExcelImporter>();
builder.Services.AddScoped<IEmployeePdfGenerator, EmployeePdfGenerator>();

// 🔹 Registro de servicios de aplicación
builder.Services.AddScoped<IEmployeeService>(sp =>
{
    var repository = sp.GetRequiredService<IEmployeeRepository>();
    var excelImporter = sp.GetRequiredService<IEmployeeExcelImporter>();
    var pdfGenerator = sp.GetRequiredService<IEmployeePdfGenerator>();
    var emailService = sp.GetRequiredService<IEmailService>();

    return new EmployeeService(repository, excelImporter, pdfGenerator, emailService);
});

builder.Services.AddScoped<IDepartmentService, DepartmentService>();
builder.Services.AddScoped<IPositionService, PositionService>();
builder.Services.AddScoped<IEducationalLevelService, EducationalLevelService>();

// 🔹 Registro de AuthService
builder.Services.AddScoped<IAuthService>(sp =>
{
    var userManager = sp.GetRequiredService<UserManager<IdentityUser>>();
    var signInManager = sp.GetRequiredService<SignInManager<IdentityUser>>();
    var emailService = sp.GetRequiredService<IEmailService>();

    var jwtKey = builder.Configuration["Jwt:Key"]!;
    var jwtIssuer = builder.Configuration["Jwt:Issuer"]!;
    var jwtAudience = builder.Configuration["Jwt:Audience"]!;

    return new AuthService(userManager, signInManager, emailService, jwtKey, jwtIssuer, jwtAudience);
});

// 🔹 Swagger con JWT
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "TalentPlus API",
        Version = "v1"
    });

    // 🔹 Configuración JWT para Swagger
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme. Example: 'Bearer {token}'",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement()
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                },
                Scheme = "oauth2",
                Name = "Bearer",
                In = ParameterLocation.Header
            },
            new List<string>()
        }
    });
});

// 🔹 Agregar controllers
builder.Services.AddControllers();

var app = builder.Build();

// 🔹 Middleware
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "TalentPlus API v1");
    c.RoutePrefix = string.Empty; // Swagger en la raíz
});

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.UseCors("AllowAll");

// 🔹 Mapear controllers
app.MapControllers();

// 🔹 Inicialización de roles y usuario admin
using (var scope = app.Services.CreateScope())
{
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();

    await InitializeRolesAndAdminAsync(roleManager, userManager);
}

// 🔹 Método helper async para roles y admin
async Task InitializeRolesAndAdminAsync(RoleManager<IdentityRole> roleManager, UserManager<IdentityUser> userManager)
{
    if (!await roleManager.RoleExistsAsync("Administrator"))
        await roleManager.CreateAsync(new IdentityRole("Administrator"));

    var adminEmail = "TalentPlus@gmail.com";
    var adminUser = await userManager.FindByEmailAsync(adminEmail);
    if (adminUser == null)
    {
        adminUser = new IdentityUser
        {
            UserName = adminEmail,
            Email = adminEmail,
            EmailConfirmed = true
        };
        await userManager.CreateAsync(adminUser, "Admin123!");
        await userManager.AddToRoleAsync(adminUser, "Administrator");
    }
}

app.Run();
