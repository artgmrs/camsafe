using CamSafe.Business.Businesses;
using CamSafe.Business.Interfaces;
using CamSafe.Repository.Interfaces;
using CamSafe.Repository.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Dependency Injection
builder.Services.AddScoped<ICameraBusiness, CameraBusiness>();
builder.Services.AddScoped<ICameraRepository, CameraRepository>();
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddScoped<ICustomerBusiness, CustomerBusiness>();
builder.Services.AddScoped<IReportRepository, ReportRepository>();
builder.Services.AddScoped<IReportBusiness, ReportBusiness>();

var app = builder.Build();

// Just to showcase the endpoints to facilitate testing
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
