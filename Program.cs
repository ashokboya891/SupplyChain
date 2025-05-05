using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using StackExchange.Redis;
using StocksApi.Services;
using SupplyChain.DatabaseContext;
using SupplyChain.IServiceContracts;
using SupplyChain.Mappers;
using SupplyChain.ServiceContracts;
using System.ComponentModel.Design;
using SupplyChain.ServiceContracts;
using SupplyChain.IReposioryContracts;
using SupplyChain.ReposioryContracts;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

// Add AutoMapper with all profiles in the assembly
builder.Services.AddAutoMapper(typeof(MappingProfile));

// Add services to the container.
//builder.Services.AddAutoMapper(typeof(Program)); // Or your startup class
//builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
// Add Redis caching
builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = "localhost:6379"; // Redis server URL
    options.InstanceName = "SampleInstance_"; // Optional prefix for keys
});

builder.Services.AddControllers();
builder.Services.AddDbContext<ApplicationDbContext>(opt =>
{
    opt.UseSqlServer(builder.Configuration.GetConnectionString("con"));
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IMappingService, MappingService>();

// Repositorybuilder.
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddScoped<ICustomerService, CustomerService>();
builder.Services.AddScoped<IJwtService, JwtService>();
builder.Services.AddTransient<ICartService, CartService>();
builder.Services.AddScoped<ICartRepository, CartRepository>();


// 🔹 Identity & Authentication
builder.Services.AddIdentity<ApplicationUser, ApplicationRole>(opt =>
{
    opt.Password.RequiredLength = 5;
    opt.Password.RequireNonAlphanumeric = false;

    opt.Password.RequireUppercase = false;
    opt.Password.RequireLowercase = true;
    opt.Password.RequireDigit = true;
})
.AddEntityFrameworkStores<ApplicationDbContext>()
.AddDefaultTokenProviders()
.AddUserStore<UserStore<ApplicationUser, ApplicationRole, ApplicationDbContext, Guid>>()
.AddRoleStore<RoleStore<ApplicationRole, ApplicationDbContext, Guid>>();

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateAudience = true,
        ValidAudience = configuration["Jwt:Audience"],
        ValidateIssuer = true,
        ValidIssuer = configuration["Jwt:Issuer"],
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(configuration["Jwt:Key"]))
    };
});

builder.Services.AddAuthorization(opt =>
{
    opt.AddPolicy("RequireAdminRole", policy => policy.RequireRole("Admin"));
    opt.AddPolicy("ModeratePhotoRole", policy => policy.RequireRole("Admin", "User"));

});

// AutoMapper
builder.Services.AddAutoMapper(typeof(MappingProfile));

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigins", policyBuilder =>
    {
        policyBuilder.WithOrigins(configuration.GetSection("AllowedOrigins").Get<string[]>())
                     .AllowAnyHeader()
                     .AllowAnyMethod();
    });
});


var app = builder.Build();
app.UseCors("AllowSpecificOrigins");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
