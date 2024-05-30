using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using Musbooking.Application.Commands.Equipment;
using Musbooking.Application.Requests.Order;
using Musbooking.Application.Requests.Validators;
using Musbooking.Host.Filters;
using Musbooking.Infrastructure.Contexts.Abstractions;
using Musbooking.Infrastructure.Contexts.Implementation;
using Musbooking.Infrastructure.Repositories.Abstractions;
using Musbooking.Infrastructure.Repositories.Implementation;
using EquipmentRequest = Musbooking.Application.Requests.Equipment.EquipmentRequest;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

#region Contexts

builder.Services.AddDbContext<IOrderReadContext, OrderReadContext>(options =>
    options.UseSqlite(connectionString));
builder.Services.AddDbContext<IOrderWriteContext, OrderWriteContext>(options => options.UseSqlite(connectionString));
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IEquipmentRepository, EquipmentRepository>();
builder.Services.AddScoped<IOrderReadRepository, OrderReadRepository>();
builder.Services.AddScoped<IOrderEquipmentRepository, OrderEquipmentRepository>();

#endregion

#region Utils

builder.Services.AddControllers(options => { options.Filters.Add<ExceptionFilter>(0); });


builder.Services.AddScoped<IValidator<EquipmentRequest>, CreateEquipmentValidator>();
builder.Services.AddScoped<IValidator<OrderRequest>, CreateOrderValidator>();
builder.Services.AddFluentValidationAutoValidation();

#endregion

#region CQRS

builder.Services.AddMediatR(x => x.RegisterServicesFromAssemblyContaining<AddEquipmentCommand>());

#endregion

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRouting();
app.MapControllers();

app.UseHttpsRedirection();

app.Run();