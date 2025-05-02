using Microsoft.EntityFrameworkCore;
using SupplyChain.DatabaseContext;
using SupplyChain.IServiceContracts;
using SupplyChain.Mappers;
using SupplyChain.ServiceContracts;

var builder = WebApplication.CreateBuilder(args);
// Add AutoMapper with all profiles in the assembly
builder.Services.AddAutoMapper(typeof(MappingProfile));

// Add services to the container.
//builder.Services.AddAutoMapper(typeof(Program)); // Or your startup class
//builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
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

// Services
builder.Services.AddScoped<ICustomerService, CustomerService>();

// AutoMapper
builder.Services.AddAutoMapper(typeof(MappingProfile));

var app = builder.Build();

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
